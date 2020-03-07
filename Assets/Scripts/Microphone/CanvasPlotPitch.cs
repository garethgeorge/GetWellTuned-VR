using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pitch;

public class CanvasPlotPitch : MonoBehaviour
{
  // https://forum.unity.com/threads/how-to-draw-lines-on-a-canvas-in-real-game-time.553669/

  private Texture2D drawTexture;
  private int width;
  private int height;
  private int index;
  public RawImage rawImage;
  void Start()
  {
    width = 64;
    height = 12;
    drawTexture = new Texture2D(width, height);
    fillRect(drawTexture, 0, 0, width, height, Color.green);
    rawImage.texture = drawTexture;
  }

  void fillRect(Texture2D texture, int x, int y, int w, int h, Color color)
  {
    var array = texture.GetPixels(x, y, w, h);
    for (int dx = 0; dx < w; ++dx)
    {
      for (int dy = 0; dy < h; ++dy)
      {
        array[(y + dy) * texture.width + x + dx] = color;
      }
    }
    texture.SetPixels(x, y, w, h, array);
  }

  void drawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
  {
    var dy = y1 - y0;
    var dx = x1 - x0;

    int stepy;
    int stepx;
    if (dy < 0) { dy = -dy; stepy = -1; }
    else { stepy = 1; }
    if (dx < 0) { dx = -dx; stepx = -1; }
    else { stepx = 1; }
    dy <<= 1;
    dx <<= 1;

    tex.SetPixel(x0, y0, col);
    if (dx > dy)
    {
      var fraction = dy - (dx >> 1);
      while (x0 != x1)
      {
        if (fraction >= 0)
        {
          y0 += stepy;
          fraction -= dx;
        }
        x0 += stepx;
        fraction += dy;
        tex.SetPixel(x0, y0, col);
      }
    }
    else
    {
      var fraction = dx - (dy >> 1);
      while (y0 != y1)
      {
        if (fraction >= 0)
        {
          x0 += stepx;
          fraction -= dy;
        }
        y0 += stepy;
        fraction += dx;
        tex.SetPixel(x0, y0, col);
      }
    }
  }

  void Update()
  {
    // clear the canvas
    fillRect(drawTexture, 0, 0, drawTexture.width, drawTexture.height, Color.black);

    int refPitchesLen = MicrophoneManager.recentRefPitches.Length;
    int refPithesCurIdx = MicrophoneManager.recentRefPitchesIdx;
    int xStepSize = drawTexture.width / refPitchesLen;
    int yStepSize = drawTexture.height / 12;

    // plot the target pitch
    for (int i = 0; i < refPitchesLen; ++i)
    {
      // we now figure out what the pitch is at that index ...
      int adjustedIdx = (refPithesCurIdx - i) % refPitchesLen;
      int pitchAtIndex = MicrophoneManager.recentRefPitches[adjustedIdx];

      fillRect(drawTexture, xStepSize * i, (pitchAtIndex % 12) * yStepSize, xStepSize, yStepSize, Color.grey);
    }

    // plot the user's pitch
    for (int i = 0; i < refPitchesLen; ++i)
    {
      // we now figure out what the pitch is at that index ...
      int adjustedIdx = (refPithesCurIdx - i) % refPitchesLen;
      int pitchAtIndex = MicrophoneManager.recentUserPitches[adjustedIdx];

      fillRect(drawTexture, xStepSize * i, (pitchAtIndex % 12) * yStepSize, xStepSize, yStepSize, Color.white);
    }

    drawTexture.Apply();

  }

}

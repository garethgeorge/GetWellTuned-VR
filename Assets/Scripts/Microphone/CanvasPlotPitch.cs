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
    width = 128;
    height = 48;
    drawTexture = new Texture2D(width, height);
    fillRect(drawTexture, 0, 0, width, height, Color.green);
    rawImage.texture = drawTexture;
  }

  void fillRect(Texture2D texture, int x, int y, int w, int h, Color color)
  {
    var array = texture.GetPixels(x, y, w, h);
    for (int idx = 0; idx < array.Length; ++idx)
    {
      array[idx] = color;
    }
    texture.SetPixels(x, y, w, h, array);
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

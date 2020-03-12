using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTileController : MonoBehaviour
{
  private GameObject discoTile;
  private static Color[] colorOptions;

  private Color myColor;
  private Color myColorDim;

  private Material myMat;
  private Material myMatDim;
  private MeshRenderer myRenderer;

  public Light light;

  private int intersectionCount = 0;

  void Start()
  {
    if (colorOptions == null)
    {
      colorOptions = new Color[6]{
        new Color(1f, 0f, 0f),
        new Color(0f, 1f, 0f),
        new Color(0f, 0f, 1f),
        new Color(0f, 1f, 1f),
        new Color(1f, 1f, 0f),
        new Color(1f, 0f, 1f),
      };
    }

    myMat = new Material(Shader.Find("Glow"));
    myMatDim = new Material(Shader.Find("Standard"));

    myRenderer = (MeshRenderer)GetComponentInParent<MeshRenderer>();
    myRenderer.material = myMatDim;

    PickNewColor();
    TurnOff();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag != "location")
      return;
    if (intersectionCount == 0 && myRenderer)
    {
      TurnOn();
      // if (MicrophoneManager.isMatchingDecay < 40)
      // {
      //   MicrophoneManager.isMatchingDecay = 40;
      // }
      DiscoFloorController.movementScore += 100;
    }
    intersectionCount++;
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag != "location")
      return;
    intersectionCount--;
    if (intersectionCount == 0 && myRenderer)
      TurnOff();
  }

  void PickNewColor()
  {
    myColor = colorOptions[Random.Range(0, colorOptions.Length)];
    myColorDim = Color.Lerp(myColor, Color.black, 0.7f);
    myMat.color = myColor;
    myMatDim.color = myColorDim;
  }

  void TurnOn()
  {
    light.enabled = true;
    light.color = myColor;
    myRenderer.material = myMat;
  }

  void TurnOff()
  {
    light.enabled = false;
    myRenderer.material = myMatDim;
  }
}

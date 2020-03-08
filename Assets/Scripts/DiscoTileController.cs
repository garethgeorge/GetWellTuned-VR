using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTileController : MonoBehaviour
{
  private GameObject discoTile;
  private static Color[] colorOptions;

  private Color myColor;
  private Color myColorDim;

  private int enteredCount = 0;
  private Material myMat;
  private Material myMatDim;
  private MeshRenderer myRenderer;

  public Light light;

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
    TurnOn();
  }

  IEnumerator OnTriggerExit(Collider other)
  {
    int lastEnteredCount = ++enteredCount;
    yield return new WaitForSeconds(0.1f);
    if (lastEnteredCount == enteredCount)
    {
      TurnOff();
    }
    yield return null;
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

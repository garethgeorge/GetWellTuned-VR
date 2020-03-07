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

  void Start()
  {
    if (colorOptions == null)
    {
      colorOptions = new Color[7]{
        new Color(1f, 0f, 0f),
        new Color(0f, 1f, 0f),
        new Color(0f, 0f, 1f),
        new Color(0f, 1f, 1f),
        new Color(1f, 1f, 0f),
        new Color(1f, 0f, 1f),
        new Color(1f, 0.5f, 1f),
      };
    }

    myColor = colorOptions[Random.Range(0, colorOptions.Length)];
    myColorDim = Color.Lerp(myColor, Color.black, 0.7f);

    myMat = new Material(Shader.Find("Glow"));
    myMatDim = new Material(Shader.Find("Standard"));
    myMat.color = myColor;
    myMatDim.color = myColorDim;

    myRenderer = (MeshRenderer)GetComponentInParent<MeshRenderer>();
    myRenderer.material = myMatDim;
  }

  void OnTriggerEnter(Collider other)
  {
    myRenderer.material = myMat;
  }

  IEnumerator OnTriggerExit(Collider other)
  {
    yield return new WaitForSeconds(1);
    myRenderer.material = myMatDim;
    yield return null;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
  public GameObject gameObject;
  public GameObject Pompeii;
  Renderer rend;
  void Start()
  {
    rend = gameObject.GetComponent<Renderer>();

  }

  public void colorChangeGreen()
  {
    // rend.material.color = Color.green;
    Pompeii.SetActive(!Pompeii.activeSelf);
  }

  public void colorChangeBlue()
  {
    rend.material.color = Color.blue;
  }

  public void colorChangeRed()
  {
    rend.material.color = Color.red;
  }
}

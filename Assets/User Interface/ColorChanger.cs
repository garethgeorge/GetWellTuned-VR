using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
  public GameObject gameObject;
  public GameObject Pompeii;
  public GameObject ActiveSong;
  Renderer rend;
 // UnityEngine.Video.VideoPlayer screen;
  void Start()
  {
    rend = gameObject.GetComponent<Renderer>();
  //  screen = Pompeii.GetComponent<UnityEngine.Video.VideoPlayer>();


  }

  public void colorChangeGreen()
  {
    // rend.material.color = Color.green;
    Pompeii.SetActive(true);
    ActiveSong.SetActive(true);
  //  screen.SetActive(true);
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

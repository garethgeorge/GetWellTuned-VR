using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectUI : MonoBehaviour
{
  public GameObject Pompeii;
  public GameObject ActiveSong;
  void Start()
  {

  }

  public void playPompeii()
  {
    Pompeii.SetActive(true);
    ActiveSong.SetActive(true);
  }

}

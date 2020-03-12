using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DisplayFinalScore : MonoBehaviour
{
  public GameObject finalScoreCanvas;
  public VideoPlayer videoPlayer;
  public VideoPlayer videoPlayer2;

  void Start()
  {
    videoPlayer.loopPointReached += EndReached;
    videoPlayer2.loopPointReached += EndReached;
  }

  // Update is called once per frame
  void Update()
  {

  }

  void EndReached(UnityEngine.Video.VideoPlayer vp)
  {
    //finalScoreCanvas.SetActive(false);
    finalScoreCanvas.SetActive(true);
    Debug.Log("done");
  }
}

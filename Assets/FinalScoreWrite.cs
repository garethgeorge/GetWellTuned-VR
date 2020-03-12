using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pitch;

public class FinalScoreWrite : MonoBehaviour
{
  Text finalScoreText;
  void Start()
  {
    finalScoreText = gameObject.GetComponent<Text>();
  }


  void Update()
  {
    finalScoreText.text = "Congratulations! Your final score is: " + Mathf.Round(MicrophoneManager.score * 1000).ToString() + " Movement score: " + Mathf.Round(DiscoFloorController.movementScore).ToString();
  }
}

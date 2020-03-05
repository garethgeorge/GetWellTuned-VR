using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFinalScore : MonoBehaviour
{
    public GameObject finalScoreCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeFinalScore()
    {
        finalScoreCanvas.SetActive(false);
    }
}

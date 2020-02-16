using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.MathHelpers;

public class MicrophoneManager : MonoBehaviour
{
  float[] spec = new float[1024];
  AudioSource audio;
  LineRenderer line;
  // Start is called before the first frame update
  void Start()
  {
    audio = GetComponent<AudioSource>();
    audio.clip = Microphone.Start(null, true, 1, 22050);
    audio.loop = true;
    while (!(Microphone.GetPosition(null) > 0)) { }
    Debug.Log("start playing... position is " + Microphone.GetPosition(null));
    audio.Play();

    line = GetComponent<LineRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    // https://answers.unity.com/questions/974565/how-to-do-a-fft-in-unity.html?childToView=1317336#answer-1317336
    // Unity's FFT function
    audio.GetSpectrumData(spec, 0, FFTWindow.Rectangular);
    line.positionCount = spec.Length;
    for (int i = 0; i < spec.Length; i++)
    {
      if (spec[i] > 0.0000001) {
        line.SetPosition(i, new Vector3((float)(i * 0.1), Mathf.Log(spec[i])));
      }
    }
  }
}

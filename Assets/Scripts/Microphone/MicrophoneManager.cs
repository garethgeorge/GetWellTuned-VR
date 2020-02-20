using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MicrophoneManager : MonoBehaviour
{
  public int bufferSize = 2048;
  private float[] buffer;
  public AudioSource audio;
  public LineRenderer lineRenderer;
  // Start is called before the first frame update
  private FastYin yin;
  public Text message;

  private float lastPitch = -1;
  void Start()
  {
    audio = GetComponent<AudioSource>();
    audio.clip = Microphone.Start(null, true, 1, 22050);
    audio.loop = true;
    audio.Play();

    // lineRenderer = GetComponent<LineRenderer>();

    // list connected microphones 
    Debug.Log("listing connected microphone devices");
    foreach (var device in Microphone.devices)
    {
      Debug.Log("Microphone Device Name: " + device);
    }
  }

  // Update is called once per frame
  void Update()
  {
    Debug.Log("start playing... position is " + Microphone.GetPosition(null));
    // https://answers.unity.com/questions/974565/how-to-do-a-fft-in-unity.html?childToView=1317336#answer-1317336
    // Unity's FFT function


    /* // PRETTY FFT VISUALIZATION STUFF
    audio.GetSpectrumData(spec, 0, FFTWindow.Rectangular);
    lineRenderer.positionCount = spec.Length;

    for (int i = 0; i < spec.Length; i++)
    {
      if (spec[i] > 0.0000001)
      {
        lineRenderer.SetPosition(i, new Vector3((float)(i * 0.1), Mathf.Log(spec[i])));
        // Debug.DrawLine(new Vector3(i, 0), new Vector3(i, Mathf.Log(spec[i]) * 10), Color.cyan);
      }
    }
    */

    // https://github.com/tbriley/PitchDetector/blob/1c30ff6f1127cab690e82d14e5f0dfa84ad0b85e/Assets/Plugins/PitchDetector/Systems/FastYinSystem.cs
    yin = new FastYin(22050, bufferSize, 0.15);
    buffer = new float[bufferSize];
    audio.GetOutputData(buffer, 0);
    Debug.Log("hello world!");

    PitchDetectionResult res = yin.getPitch(buffer);
    float curPitch = res.getPitch();

    if (curPitch != -1)
    {
      lastPitch = curPitch;
      message.text = "The pitch is: " + lastPitch.ToString();
    }
  }
}

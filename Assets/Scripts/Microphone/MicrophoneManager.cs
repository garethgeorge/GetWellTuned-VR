using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pitch;


public class MicrophoneManager : MonoBehaviour
{
  public int bufferSize = 1024;
  private float[] buffer;
  public AudioSource audio;
  public LineRenderer lineRenderer;
  private PitchTracker pitchTracker;
  public Text message;
  public PitchTracker.PitchRecord lastPitchResult;

  void Start()
  {
    // connect the microphone to our audio source
    audio = GetComponent<AudioSource>();
    audio.clip = Microphone.Start(null, true, 1, 22050);
    audio.loop = true;
    audio.Play();

    // list connected microphones 
    Debug.Log("listing connected microphone devices");
    foreach (var device in Microphone.devices)
    {
      Debug.Log("Microphone Device Name: " + device);
    }

    buffer = new float[bufferSize];
    pitchTracker = new PitchTracker();
    pitchTracker.SampleRate = 22050;
  }

  // Update is called once per frame
  void Update()
  {
    if (!Mathf.Approximately(audio.clip.frequency, (float)pitchTracker.SampleRate))
    {
      Debug.Log("upating sample rate to " + audio.clip.frequency);
      pitchTracker.SampleRate = audio.clip.frequency;
    }
    audio.GetOutputData(buffer, 0);
    pitchTracker.ProcessBuffer(buffer);

    lastPitchResult = pitchTracker.CurrentPitchRecord;
    float curPitch = pitchTracker.CurrentPitchRecord.Pitch;

    if (curPitch != -1)
    {
      message.text = "The pitch is: " + curPitch.ToString() + "\nNote: " + pitchTracker.CurrentPitchRecord.MidiNote;
    }
  }
}

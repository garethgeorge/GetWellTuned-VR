using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pitch;

public class MicrophoneManager : MonoBehaviour
{
  private AudioSource audioSourceMicin;
  public AudioSource audioSourceReference;

  private PitchTracker audioInTracker;
  private PitchTracker audioReferenceTracker;
  private PitchTracker.PitchRecord audioRefPitch;
  private PitchTracker.PitchRecord audioInPitch;

  public int bufferSize = 1024;
  private float[] buffer;

  public Text message;

  private int recentRefPitchesIdx = 0;
  public int[] recentRefPitches;

  void Start()
  {
    audioSourceMicin = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
    audioSourceMicin.clip = Microphone.Start(null, true, 1, 22050);
    audioSourceMicin.loop = true;
    audioSourceMicin.Play();

    audioInTracker = new PitchTracker();
    audioInTracker.SampleRate = 22050;

    audioSourceReference = GetComponent<AudioSource>();
    audioSourceReference.loop = false;
    // audioSourceReference.time = 3; // 3 is nearly perfect sync...
    audioSourceReference.time = (float)3;
    audioSourceReference.Play();

    audioReferenceTracker = new PitchTracker();
    audioReferenceTracker.SampleRate = 22050;

    // list connected microphones 
    Debug.Log("listing connected microphone devices");
    foreach (var device in Microphone.devices)
    {
      Debug.Log("Microphone Device Name: " + device);
    }

    buffer = new float[bufferSize];
    recentRefPitches = new int[60];
  }

  // Update is called once per frame
  void Update()
  {
    audioSourceMicin.GetOutputData(buffer, 0);
    audioInTracker.ProcessBuffer(buffer);

    audioSourceReference.GetOutputData(buffer, 0);
    audioReferenceTracker.ProcessBuffer(buffer);

    PitchTracker.PitchRecord curPitch = audioInTracker.CurrentPitchRecord;
    PitchTracker.PitchRecord curPitchRef = audioReferenceTracker.CurrentPitchRecord;
    if (curPitch.MidiNote != 0)
      audioInPitch = curPitch;
    if (curPitchRef.MidiNote != 0)
      audioRefPitch = curPitchRef;
    recentRefPitches[(recentRefPitchesIdx++) % recentRefPitches.Length] = audioRefPitch.MidiNote;

    string text = "Current note is: " + audioInPitch.MidiNote + "\nExpected: " + audioRefPitch.MidiNote;

    foreach (int value in recentRefPitches)
    {
      if (value != 0 && value == audioInPitch.MidiNote)
      {
        text = "THIS IS MATCHING!!! WOOHOO";
      }
    }

    message.text = text;
  }
}

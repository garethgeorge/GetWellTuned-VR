using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pitch;

public class MicrophoneManager : MonoBehaviour
{
  public AudioSource audioSourceMicin;
  public AudioSource audioSourceReference;

  private PitchTracker audioInTracker;
  private PitchTracker audioReferenceTracker;
  private int audioRefPitchLastUpdate = 0;
  private PitchTracker.PitchRecord audioRefPitch;
  private int audioInPitchLastUpdate = 0;
  private PitchTracker.PitchRecord audioInPitch;

  public int bufferSize = 1024;
  private float[] buffer;

  public Text message;

  private int recentRefPitchesIdx = 1000; // dumb fix with some index out of bounds errors 
  public int[] recentRefPitches;

  public static bool isMatching = false;
  public static float score = 0;


  void Start()
  {
    audioSourceMicin.clip = Microphone.Start(null, true, 1, 22050);
    audioSourceMicin.loop = true;
    audioSourceMicin.Play();

    audioInTracker = new PitchTracker();
    audioInTracker.SampleRate = 22050;

    audioSourceReference.loop = false;
    // audioSourceReference.time = (float)2.5;
    audioSourceReference.time = (float)3.0;
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
    score = 0;
  }

  bool FindMatch(int offset, int windowLength, int sampleNote, int forgiveness = 1)
  {
    if (sampleNote == 0)
      return false;
    for (int i = -offset - windowLength; i < -offset; ++i)
    {
      int value = recentRefPitches[(recentRefPitchesIdx + i) % recentRefPitches.Length];
      if (System.Math.Abs(value - sampleNote) <= forgiveness)
        return true;
    }
    return false;
  }

  // Update is called once per frame
  void Update()
  {
    audioSourceMicin.GetOutputData(buffer, 0);
    audioInTracker.ProcessBuffer(buffer);

    audioSourceReference.GetOutputData(buffer, 0);
    audioReferenceTracker.ProcessBuffer(buffer);

    PitchTracker.PitchRecord curPitch = audioInTracker.CurrentPitchRecord;
    audioInPitchLastUpdate++;
    PitchTracker.PitchRecord curPitchRef = audioReferenceTracker.CurrentPitchRecord;
    audioRefPitchLastUpdate++;
    if (curPitch.MidiNote != 0 || audioInPitchLastUpdate > 60)
    {
      audioInPitch = curPitch;
      audioInPitchLastUpdate = 0;
    }
    if (curPitchRef.MidiNote != 0 || audioRefPitchLastUpdate > 60)
    {
      audioRefPitch = curPitchRef;
      audioRefPitchLastUpdate = 0;
    }
    recentRefPitches[(recentRefPitchesIdx++) % recentRefPitches.Length] = audioRefPitch.MidiNote;

    string text = "Current pitch: " + audioInPitch.MidiNote + "\nTarget: " + audioRefPitch.MidiNote;

    if (this.FindMatch(0, recentRefPitches.Length, audioInPitch.MidiNote))
    {
      isMatching = true;
      text = "MATCHING! GREAT JOB!!!";
      score += Time.deltaTime;
    }
    else
      isMatching = false;

    text += "\nScore: " + Mathf.Round(score * 1000).ToString();

    message.text = text;
    }
}

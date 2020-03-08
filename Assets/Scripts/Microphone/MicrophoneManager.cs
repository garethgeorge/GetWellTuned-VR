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

  public static int recentRefPitchesIdx = 1000; // dumb fix with some index out of bounds errors 
  public static int[] recentRefPitches;
  public static int[] recentUserPitches; // actually uses the same index as ref pitches

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
    recentRefPitches = new int[120];
    recentUserPitches = new int[120];
    score = 0;
  }

  bool FindMatch(int offset, int windowLength, int sampleNote, int forgiveness = 0)
  {
    if (sampleNote == 0)
      return false;
    for (int i = -offset - windowLength; i < -offset; ++i)
    {
      int value = recentRefPitches[(recentRefPitchesIdx + i) % recentRefPitches.Length] % 12;
      if (value != 0 && value == sampleNote % 12)
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
    if (curPitch.MidiNote != 0 || audioInPitchLastUpdate > 15)
    {
      audioInPitch = curPitch;
      audioInPitchLastUpdate = 0;
    }
    if (curPitchRef.MidiNote != 0 || audioRefPitchLastUpdate > 15)
    {
      audioRefPitch = curPitchRef;
      audioRefPitchLastUpdate = 0;
    }
    // store the user's midi note and the reference midi note in the history array 
    recentUserPitches[recentRefPitchesIdx % recentRefPitches.Length] = audioInPitch.MidiNote;
    recentRefPitches[(recentRefPitchesIdx++) % recentRefPitches.Length] = audioRefPitch.MidiNote;

    string text = "Current pitch: " + audioInPitch.MidiNote + "\nTarget: " + audioRefPitch.MidiNote;

    if (this.FindMatch(recentRefPitches.Length / 4, recentRefPitches.Length * 3 / 4, audioInPitch.MidiNote))
    {
      isMatching = true;
      score += Time.deltaTime;
      message.color = Color.green;
    }
    else
    {
      isMatching = false;
      message.color = Color.white;
    }

    text += "\nScore: " + Mathf.Round(score * 1000).ToString();

    message.text = text;
  }
}

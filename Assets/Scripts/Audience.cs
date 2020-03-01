using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
  public MicrophoneManager microphoneManager;

  // private string[] names = { "idle", "applause", "applause2", "celebration", "celebration2", "celebration3" };
  private string[] animationNames = { "idle", "applause", "applause2", "idle", "applause", "applause2", "celebration" };

  // Use this for initialization
  void Start()
  {
    Animation[] AudienceMembers = gameObject.GetComponentsInChildren<Animation>();
    foreach (Animation anim in AudienceMembers)
    {
      StartCoroutine(setAnimation(anim));
    }
  }

  IEnumerator setAnimation(Animation anim)
  {
    string thisAnimation = "idle";
    if (MicrophoneManager.isMatching)
    {
      thisAnimation = animationNames[Random.Range(0, animationNames.Length)];
    }

    float timePeriod = Random.Range(0f, 4f);

    anim.wrapMode = WrapMode.Loop;
    anim.GetComponent<Animation>().CrossFade(thisAnimation);
    anim[thisAnimation].time = timePeriod;

    yield return new WaitForSeconds(timePeriod);
    StartCoroutine(setAnimation(anim));
    yield return null;
  }
}
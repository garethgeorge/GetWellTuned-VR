﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
  public MicrophoneManager microphoneManager;

  // private string[] names = { "idle", "applause", "applause2", "celebration", "celebration2", "celebration3" };
  private string[] animationNames = { "idle", "applause", "applause2", "idle", "applause", "applause2", "celebration" };

  public OVRCameraRig cameraRig;

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
    if (
      MicrophoneManager.isMatching ||
      (cameraRig && cameraRig.leftHandAnchor.transform.position.y > cameraRig.centerEyeAnchor.transform.position.y)
    )
    {
      thisAnimation = animationNames[Random.Range(0, animationNames.Length)];
    }

    // float timePeriod = Random.Range(0f, 4f);

    anim.wrapMode = WrapMode.Loop;
    anim.GetComponent<Animation>().CrossFade(thisAnimation);
    anim[thisAnimation].speed = Random.Range(0.5f, 2f);

    yield return new WaitForSeconds(anim[thisAnimation].length * anim[thisAnimation].speed);
    StartCoroutine(setAnimation(anim));
    yield return null;
  }
}
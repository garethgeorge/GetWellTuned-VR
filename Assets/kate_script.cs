using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kate_script : MonoBehaviour
{
    // Animator anim;
    int jumpHash = Animator.StringToHash("Clapping");
    int runStateHash = Animator.StringToHash("Base Layer.Run");

    private string[] animationNames = { "Clapping", "Idle" };


    // Start is called before the first frame update
    void Start()
    {
        Animation anim = gameObject.GetComponent<Animation>();

        StartCoroutine(setAnimation(anim));

    }

    // Update is called once per frame
    IEnumerator setAnimation(Animation anim)
    {

        string thisAnimation = "idle";
        // if (
        //   MicrophoneManager.isMatching ||
        //   (cameraRig && cameraRig.leftHandAnchor.transform.position.y > cameraRig.centerEyeAnchor.transform.position.y)
        // )
        // {
        //     thisAnimation = animationNames[Random.Range(0, animationNames.Length)];
        // }

        thisAnimation = animationNames[Random.Range(0, animationNames.Length)];
        float timePeriod = Random.Range(0f, 4f);

        anim.wrapMode = WrapMode.Loop;
        anim.GetComponent<Animation>().CrossFade(thisAnimation);
        anim[thisAnimation].speed = Random.Range(0.5f, 2f);

        yield return new WaitForSeconds(anim[thisAnimation].length * anim[thisAnimation].speed);
        StartCoroutine(setAnimation(anim));
        yield return null;
    }
}

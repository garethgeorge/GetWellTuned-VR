using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject Pompeii;
    public GameObject ActiveSong;
    Renderer rend;

    public Text debugText;

    public GameObject Audience;
    public int crowd_size;
    public List<Animation> audienceActive;
    public bool audienceActivated = false;
    // UnityEngine.Video.VideoPlayer screen;
    private int rand_num;

    public MicrophoneManager microphoneManager;

    // private string[] names = { "idle", "applause", "applause2", "celebration", "celebration2", "celebration3" };
    private string[] animationNames = { "idle", "applause", "applause2", "idle", "applause", "applause2", "celebration" };

    public OVRCameraRig cameraRig;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        //  screen = Pompeii.GetComponent<UnityEngine.Video.VideoPlayer>();


    }

    void Update()
    {
        debugText.text = "start";
    }
    public void colorChangeGreen()
    {
        // rend.material.color = Color.green;
        Pompeii.SetActive(true);
        ActiveSong.SetActive(true);
        //  screen.SetActive(true);
    }

    public void colorChangeBlue()
    {
        rend.material.color = Color.blue;
    }

    public void colorChangeRed()
    {
        rend.material.color = Color.red;
    }

    public void audienceActivate0()
    {
        audienceActivated = false;
        Audience.SetActive(false);
        crowd_size = 0;


    }

    public void audienceActivate10()
    {
        audienceActivated = true;
        crowd_size = 10;

        debugText.text = "\n 10 is clicked";


        Animation[] AudienceMembers = Audience.GetComponentsInChildren<Animation>();

        for (int i = 0; i < crowd_size; i++)
        {
            rand_num = Random.Range(0, AudienceMembers.Length);
            debugText.text = "\ngetting random Audience No." + rand_num;
            Audience.transform.GetChild(rand_num).gameObject.SetActive(true);
            // audienceActive.Add(AudienceMembers[]);
            audienceActive.Add(AudienceMembers[rand_num]);

        }

        foreach (Animation anim in audienceActive)
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

        float timePeriod = Random.Range(0f, 4f);

        anim.wrapMode = WrapMode.Loop;
        anim.GetComponent<Animation>().CrossFade(thisAnimation);
        anim[thisAnimation].time = timePeriod;

        yield return new WaitForSeconds(timePeriod);
        StartCoroutine(setAnimation(anim));
        yield return null;
    }
}

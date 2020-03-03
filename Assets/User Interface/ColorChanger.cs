using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject Pompeii;
    public GameObject ActiveSong;
    Renderer rend;

    public GameObject Audience;
    public int crowd_size;
    public List<Animation> audienceActive;
    public bool audienceActivated = false;
    // UnityEngine.Video.VideoPlayer screen;
    private int rand_num;
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        //  screen = Pompeii.GetComponent<UnityEngine.Video.VideoPlayer>();


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

        Animation[] AudienceMembers = Audience.GetComponentsInChildren<Animation>();

        for (int i = 0; i < crowd_size; i++)
        {
            rand_num = Random.Range(0, AudienceMembers.Length);
            Audience.transform.GetChild(rand_num).gameObject.SetActive(true);
            // audienceActive.Add(AudienceMembers[]);
            audienceActive.Add(AudienceMembers[rand_num]);

        }





    }
}

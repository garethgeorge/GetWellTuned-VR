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
    // UnityEngine.Video.VideoPlayer screen;
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

        Audience.SetActive(true);
        crowd_size = 0;


    }

    public void audienceActivate10()
    {

        Audience.SetActive(true);
        crowd_size = 10;

        Animation[] AudienceMembers = Audience.GetComponentsInChildren<Animation>();

        // depends on which button clicked
        for (int i = 0; i < crowd_size; i++)
        {
            audienceActive.Add(AudienceMembers[Random.Range(0, AudienceMembers.Length)]);
        }
    }
}

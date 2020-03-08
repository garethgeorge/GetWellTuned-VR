using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectUI : MonoBehaviour
{
    public GameObject Pompeii;
    public GameObject ActiveSong;
    public GameObject Crowd;
    private int crowdSize;
    void Start()
    {

    }

    public void playPompeii()
    {
        Pompeii.SetActive(true);
        ActiveSong.SetActive(true);
    }

    public void activateCrowd_0()
    {
        foreach (Transform child in Crowd.transform)
        {
            child.gameObject.SetActive(false);
        }

    }
    public void activateCrowd_3()
    {
        crowdSize = 3;

        foreach (Transform child in Crowd.transform)
        {
            child.gameObject.SetActive(false);
        }


        for (int i = 0; i < crowdSize; i++)
        {

            Crowd.transform.GetChild(i).gameObject.SetActive(true);
        }

    }

    public void activateCrowd_6()
    {
        crowdSize = 6;

        foreach (Transform child in Crowd.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in Crowd.transform)
        {
            child.gameObject.SetActive(true);
        }
        // for (int i = 0; i < crowdSize; i++)
        // {
        //     Crowd.transform.GetChild(i).gameObject.SetActive(true);
        // }

    }





}

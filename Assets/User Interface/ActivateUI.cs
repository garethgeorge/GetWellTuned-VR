using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUI : MonoBehaviour
{
    public GameObject arrow;
    public GameObject SongSelectUI;
    public GameObject RestartUI;
    public GameObject SongActive;
    void Start()
    {
        arrow.SetActive(true);
        SongSelectUI.SetActive(false);
        RestartUI.SetActive(false);
    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("location"))
        {
            arrow.SetActive(false);
            if (SongActive.activeSelf == true)
            {
                RestartUI.SetActive(true);
            }
            else
            {
                SongSelectUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("location"))
        {
            arrow.SetActive(true);
            SongSelectUI.SetActive(false);
            RestartUI.SetActive(false);
        }
    }

}

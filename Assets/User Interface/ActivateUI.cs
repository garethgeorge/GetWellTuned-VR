using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUI : MonoBehaviour
{
    public GameObject arrow;
    public GameObject canvas;
    void Start()
    {
        arrow.SetActive(true);
        canvas.SetActive(false);
    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("location"))
        {
            arrow.SetActive(false);
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("location"))
        {
            arrow.SetActive(true);
            canvas.SetActive(false);
        }
    }

}

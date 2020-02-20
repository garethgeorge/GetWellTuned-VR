using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMovement : MonoBehaviour
{
  bool movement = true;

  void Update()
  {
    transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    {

      if (movement)
      {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        Debug.Log(Time.deltaTime);
        if (transform.position.y > 3f)
        {
          movement = false;
        }

      }
      if (!movement)
      {
        // Debug.Log("down!");
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        if (transform.position.y < 2f)
        {
          movement = true;
        }
      }

    }
  }

}
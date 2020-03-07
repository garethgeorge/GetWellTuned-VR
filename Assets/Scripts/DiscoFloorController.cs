using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloorController : MonoBehaviour
{
  public GameObject template;

  // Start is called before the first frame update
  void Start()
  {
    // discoFloor.
    MeshRenderer renderer = (MeshRenderer)gameObject.GetComponent<MeshRenderer>();
    // renderer.bounds

    Instantiate(template, renderer.bounds.min, new Quaternion());
  }

  // Update is called once per frame
  void Update()
  {

  }
}

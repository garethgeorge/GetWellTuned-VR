using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject flames;
  private OVRGrabbable grabbable;

  void Start()
  {
    grabbable = transform.GetComponent<OVRGrabbable>();
  }

  // Update is called once per frame
  void Update()
  {
    if (grabbable.isGrabbed && (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
    {
      flames.SetActive(true);
    }
    else
      flames.SetActive(false);
  }
}

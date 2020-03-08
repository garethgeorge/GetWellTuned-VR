using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloorController : MonoBehaviour
{
  public Object prefab;

  // Start is called before the first frame update
  void Start()
  {
    BoxCollider collider = (BoxCollider)gameObject.GetComponent<BoxCollider>();

    for (float x = -5f; x <= 5f; x += .5f)
    {
      for (float z = -1f; z <= 1f; z += .5f)
      {
        Instantiate(prefab, transform.position + new Vector3(x, 0f, z), Quaternion.identity, transform);
      }
    }
  }
}

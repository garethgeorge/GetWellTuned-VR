using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawn : MonoBehaviour
{
    public GameObject target;
    public GameObject location;
    float time = 0f;
    void Start()
    {
        InvokeRepeating("SpawnObject", 2, 1);

        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if (time > 20f)
        {
            CancelInvoke();
        }
    }

    public void SpawnObject()
    {
        GameObject block = Instantiate(target, new Vector3(-2,3,0), Quaternion.identity);
        Destroy(block, 5);

    }
}

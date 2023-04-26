using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject drone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dronePos = drone.transform.position;
        Vector3 cameraPos = gameObject.transform.position;
        dronePos.z -= 8;
        dronePos.y += 5;
        Vector3 deltaPos = dronePos - cameraPos;
        gameObject.transform.Translate(new Vector3(deltaPos.x, deltaPos.y, deltaPos.z));
        //gameObject.transform.position.Set(dronePos.x + 5, dronePos.y, dronePos.z);
    }
}

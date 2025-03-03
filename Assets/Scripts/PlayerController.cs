using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;
    void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float steer = Input.GetAxis("Horizontal");
        float accel = Input.GetAxis("Vertical");
        float brake = Input.GetAxis("Jump");
        drivingScript.Drive(accel, brake, steer);
        drivingScript.EngineSound();
    }
}

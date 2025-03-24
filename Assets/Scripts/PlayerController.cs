using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;

    CheckpointController checkpointController;
    float lastTimeMoving = 0;

    void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
        checkpointController = 
            drivingScript.rb.GetComponent<CheckpointController>();
    }

    // Update is called once per frame
    void Update()
    {
        float steer = Input.GetAxis("Horizontal");
        float accel = Input.GetAxis("Vertical");
        float brake = Input.GetAxis("Jump");

        if (drivingScript.rb.velocity.magnitude > 1
            || RaceController.racing == false)
            lastTimeMoving = Time.time;

        if(Time.time > lastTimeMoving + 5 || 
            drivingScript.rb.gameObject.transform.position.y < -5)
        {
            drivingScript.rb.transform.position =
                checkpointController.lastPoint.transform.position
                + Vector3.up * 2;

            drivingScript.rb.transform.rotation =
                checkpointController.lastPoint.transform.rotation;

            drivingScript.rb.gameObject.layer = 8;
            Invoke(nameof(ResetLayer), 3);

        }


        if (RaceController.racing == false) accel = 0;

        drivingScript.Drive(accel, brake, steer);
        drivingScript.EngineSound();
    }

    void ResetLayer()
    {
        drivingScript.rb.gameObject.layer = 0;
    }
}

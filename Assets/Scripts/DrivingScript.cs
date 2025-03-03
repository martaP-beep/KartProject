using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelScript[] wheels;
    public float torque = 200;
    public float maxBrakeTorque = 500;
    public float maxSteerAngle = 30; 
    public float maxSpeed = 150; 
    public Rigidbody rb;
    public float currentSpeed;

    public GameObject backLight;

    public AudioSource engineSound;
    public int currentGear;
    public int numGears;
    float rpm;
    public float currentGearPerc;




    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        if(brake != 0)
        {
            backLight.SetActive(true);
        }
        else
        {
            backLight.SetActive(false);
        }

        float thrustTorque = 0;

        currentSpeed = rb.velocity.magnitude * 10;
        if (currentSpeed < maxSpeed)
        {
            thrustTorque = accel * torque;
        }
            foreach (WheelScript wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = thrustTorque;
            if (wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer;
            }
            else
            { 
                wheel.wheelCollider.brakeTorque = brake; 
            }
                Quaternion quat;
                Vector3 position;
                wheel.wheelCollider.GetWorldPose(out position, out quat);
                wheel.wheel.transform.position = position;
                wheel.wheel.transform.rotation = quat;
            }
        
    }

    public void EngineSound()
    {
        float gearPerc = 1 / (float)numGears;

        float targetGear = Mathf.InverseLerp(gearPerc * currentGear,
            gearPerc * (currentGear+1), Mathf.Abs(currentSpeed/maxSpeed));

        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGear,
            Time.deltaTime * 5);

        rpm = Mathf.Lerp(currentGear / (float)numGears, 1, currentGearPerc);

        float upperGearMax = (1 / (float)numGears) * (currentGear + 1);
        float downGearMax = (1 / (float)numGears) * (currentGear);

        float speedPerc = Mathf.Abs(currentSpeed/maxSpeed);

        if (currentGear > 0 && speedPerc < downGearMax) 
            currentGear--;

        if (currentGear < (numGears - 1) && speedPerc > upperGearMax) 
            currentGear++;


        float pitch = Mathf.Lerp(1, 6, rpm);
        engineSound.pitch = Mathf.Min(6, pitch) * 0.1f;
    }
}

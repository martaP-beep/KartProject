using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public CheckpointController[] carsController;

    void CountDown()
    {
        if(timer != 0)
        {
            Debug.Log("Rozpoczêcie gry za: " + timer);
            timer--;
        }
        else
        {
            Debug.Log("Start");
            racing = true;
            CancelInvoke(nameof(CountDown));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CountDown), 2, 1);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        carsController = new CheckpointController[cars.Length];

        for(int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckpointController>();
        }

    }

    private void LateUpdate()
    {
        int finishedLap = 0;
        foreach (var car in carsController) { 
        
            if(car.lap == totalLaps + 1)
            {
                finishedLap++;
            }

            if(finishedLap == carsController.Length && racing)
            {
                Debug.Log("Koniec wyœcigu");
                racing = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

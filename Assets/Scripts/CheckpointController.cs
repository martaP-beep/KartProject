using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int lap = 0;
    public int checkpoint = -1;
    public int nextPoint;
    int pointCount;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] checkpoints =
            GameObject.FindGameObjectsWithTag("Checkpoint");

        pointCount = checkpoints.Length;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            int thisPoint = int.Parse(other.gameObject.name);

            if (thisPoint == nextPoint)
            {

                checkpoint = thisPoint;
                if (checkpoint == 0)
                {
                    lap++;
                    Debug.Log("Okr¹¿enie: " + lap);
                }
                nextPoint++;
                nextPoint = nextPoint % pointCount;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

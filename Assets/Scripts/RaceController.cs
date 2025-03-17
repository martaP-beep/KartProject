using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public CheckpointController[] carsController;

    public TMP_Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public GameObject endPanel;


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if(timer != 0)
        {
            audioSource.PlayOneShot(count);
            startText.text = timer.ToString();
            //Debug.Log("Rozpoczêcie gry za: " + timer);
            timer--;
        }
        else
        {
            audioSource.PlayOneShot(start);
            startText.text = "START!";
            //Debug.Log("Start");
            racing = true;
            CancelInvoke(nameof(CountDown));
            Invoke(nameof(HideStartText), 1);
        }
    }
    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

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
                endPanel.SetActive(true);
                //Debug.Log("Koniec wyœcigu");
                racing = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

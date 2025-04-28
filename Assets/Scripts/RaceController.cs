using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class RaceController : MonoBehaviourPunCallbacks
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


    public GameObject carPrefab;
    public int playerCount;
    public Transform[] spawnPos;

    public GameObject startRace;
    public GameObject waitText;

    public RawImage mirror;

    public void SetMirror (Camera backCamera)
    {
        mirror.texture = backCamera.targetTexture;
    }

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

    [PunRPC]
    public void StartGame()
    {
        startRace.SetActive(false);
        waitText.SetActive(false);

        InvokeRepeating(nameof(CountDown), 2, 1);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        carsController = new CheckpointController[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckpointController>();
        }
    }

    public void BeginGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null); ;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        startText.gameObject.SetActive(false);

        int randomStartPosition = Random.Range(0, spawnPos.Length);

        Vector3 startPos = spawnPos[randomStartPosition].position;
        Quaternion startRot = spawnPos[randomStartPosition].rotation;

        GameObject playerCar = null;

        if (PhotonNetwork.IsConnected)
        {

            startPos = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
            startRot = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;

            object[] data = new object[4];
            data[0] = PlayerPrefs.GetString("PlayerName");
            data[1] = PlayerPrefs.GetInt("Red");
            data[2] = PlayerPrefs.GetInt("Green");
            data[3] = PlayerPrefs.GetInt("Blue");

            if(OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(
                    carPrefab.name, startPos, startRot, 0, data);

                playerCar.GetComponent<CarApperance>().SetLocalPlayer();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                startRace.SetActive(true);
            }
            else
            {
                waitText.SetActive(true);
            }

        }
        playerCar.GetComponent<DrivingScript>().enabled = true;
        playerCar.GetComponent<PlayerController>().enabled = true;
        

        


       

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;

    bool isConnecting;
    byte maxPlayerPerRoom = 3;
    string gameVersion = "1";
    public Text networkText;

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void StartTrial()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting) {
            networkText.text += " OnConnectedToMaster \n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += " OnJoinRandomFailed \n";
        PhotonNetwork.CreateRoom(null, new RoomOptions { 
            MaxPlayers = maxPlayerPerRoom });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += " OnDisconnected: " + cause +" \n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        networkText.text += " OnJoinedRoom:"+ 
            PhotonNetwork.CurrentRoom.PlayerCount +" players\n";

        PhotonNetwork.LoadLevel("TestTrack");

    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting=true;
        PhotonNetwork.NickName = playerName.text;

        if (PhotonNetwork.IsConnected)
        {
            networkText.text += "Join room \n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting \n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

    }

}

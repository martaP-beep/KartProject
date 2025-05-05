using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarApperance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public Renderer carRendrer;
    public TMP_Text nameText;

    public int playerNumber;

    public Camera backCamera;

    int carReg;
    bool regSet = false;
    public CheckpointController checkpointController;


    // Start is called before the first frame update
    

    public void SetLocalPlayer()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        carColor = ColorCar.IntToColor(
            PlayerPrefs.GetInt("Red"),
            PlayerPrefs.GetInt("Green"),
            PlayerPrefs.GetInt("Blue")
            );
        SetNameAndColor(playerName, carColor);
        FindObjectOfType<CameraController>().
            SetCameraProperties(this.gameObject);

        RenderTexture texture = new RenderTexture(1024, 1024, 0);
        backCamera.targetTexture = texture;
        FindObjectOfType<RaceController>().SetMirror(backCamera);
    }

    public void SetNameAndColor(string playerName, Color carColor)
    {
        this.playerName = playerName;
        nameText.text = playerName;
        carRendrer.material.color = carColor;
        nameText.color = carColor;
    }

    private void LateUpdate()
    {
        if (regSet == false)
        {
            carReg = Leaderboard.RegisterCar(playerName);
            regSet = true;
            return;
        }
        Leaderboard.SetPosition(carReg, checkpointController.lap,
            checkpointController.checkpoint);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

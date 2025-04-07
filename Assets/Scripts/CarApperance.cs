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

    // Start is called before the first frame update
    void Start()
    {
        if(playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            carColor = ColorCar.IntToColor(
                PlayerPrefs.GetInt("Red"),
                PlayerPrefs.GetInt("Green"),
                PlayerPrefs.GetInt("Blue")
                );
        }
        else
        {
            playerName = "Random " + playerNumber;
            carColor = new Color(
                Random.Range(0,255),
                Random.Range(0,255),
                Random.Range(0,255)
                );
        }

        nameText.text = playerName;
        carRendrer.material.color = carColor;
        nameText.color = carColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

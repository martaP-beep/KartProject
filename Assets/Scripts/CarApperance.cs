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


    // Start is called before the first frame update
    void Start()
    {
        nameText.text = playerName;
        carRendrer.material.color = carColor;
        nameText.color = carColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

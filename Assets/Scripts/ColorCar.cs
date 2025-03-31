using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCar : MonoBehaviour
{
    public Renderer renderer;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Text redText;
    public Text greenText;
    public Text blueText;

    public Color color;
    
    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;

        return new Color(r, g, b);
    }

    void SetCarColor(int red, int green, int blue)
    {
        Color color = IntToColor(red, green, blue);
        renderer.material.color = color;

        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);
    }

    // Start is called before the first frame update
    void Start()
    {
        color = IntToColor(
            PlayerPrefs.GetInt("Red"),
            PlayerPrefs.GetInt("Green"),
            PlayerPrefs.GetInt("Blue")
            );

        renderer.material.color = color;

        redSlider.value = PlayerPrefs.GetInt("Red");
        greenSlider.value = PlayerPrefs.GetInt("Green");
        blueSlider.value = PlayerPrefs.GetInt("Blue");
    }

    // Update is called once per frame
    void Update()
    {
        SetCarColor((int)redSlider.value,
            (int)greenSlider.value, (int)blueSlider.value);

        redText.text = redSlider.value.ToString();
        greenText.text = greenSlider.value.ToString();
        blueText.text = blueSlider.value.ToString();
    }
}

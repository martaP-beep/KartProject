using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardPanel : MonoBehaviour
{
    public List<TMP_Text> placesNumbers;

    // Start is called before the first frame update
    void Start()
    {
        Leaderboard.Reset();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        List<string> places = Leaderboard.GetPlaces();

        for(int i = 0; i < placesNumbers.Count; i++)
        {
            if(i < places.Count)
            {
                placesNumbers[i].text = places[i];
            }
            else
            {
                placesNumbers[i].text = "";
            }
        }
    }
}

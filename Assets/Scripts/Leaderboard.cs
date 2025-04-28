using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct Car
{
    public string name;
    public int position;

    public Car(string name, int position)
    {
        this.name = name;
        this.position = position;
    }
}


public class Leaderboard 
{
    static int carsRegistered = -1;
    static Dictionary<int, Car> board = new Dictionary<int, Car>();


    public static int RegisterCar(string name)
    {
        carsRegistered++;
        board.Add(carsRegistered, new Car(name, 0));
        return carsRegistered;
    }

    public static void Reset()
    {
        board.Clear();
        carsRegistered = -1;
    }

    public static void SetPosition(int id, int lap, int checkpoint)
    {
        int position = lap * 100 + checkpoint;
        board[id] = new Car(board[id].name, position);
    }

    public static List<string> GetPlaces()
    {
        List<string > places = new List<string>();
        foreach (var pos in board.OrderByDescending(key => key.Value.position))
        {
            places.Add(pos.Value.name);
        }
        return places;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

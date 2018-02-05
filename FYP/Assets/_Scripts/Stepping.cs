using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepping : MonoBehaviour {

    public List<Direction> Steps = new List<Direction>();
    private int numberOfStepsOne = 3;
    private int numberOfStepsTw0 = 4; 
    private int numberOfStepsThree = 6;


    // Use this for initialization
    void Start () {
        Steps.Add(new Direction() { DirectionName = "Left", DirectionId = 0 });
        Steps.Add(new Direction() { DirectionName = "Right", DirectionId = 1});
        Steps.Add(new Direction() { DirectionName = "Up", DirectionId = 2 });
        Steps.Add(new Direction() { DirectionName = "Down", DirectionId = 3 });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DifficultyOne()
    {
        int randomRandy = UnityEngine.Random.Range(0, 3);
    }

    void DifficultyTwo()
    {

    }

    void DifficultyThree()
    {

    }
}

public class Direction 
{
    public int DirectionId { get; set; }
    public string DirectionName { get; set; }

    //image on screen
    //keycode needed
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput : MonoBehaviour {

    public DFlowNetwork network;

    public float nManual = 0;
    public float nLeaning = 0;
    public float nOneLeg = 0;
    public float nStepping = 0;
    public float nJumping = 0;
    public float nJumpingOneleg = 0;
    public float nSpeed = 0;
    public float nObstackles = 0;
    public float nBoardMovements = 0;
    public float nJumpDifficulty = 0;
    public float nNumberofSteps = 0;
    public float nOneLegSpeed = 0;
    public float nEarthquakeShake = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nManual = network.getOutput(3);
        nLeaning = network.getOutput(4);
        nOneLeg = network.getOutput(5);
        nStepping = network.getOutput(6);
        nJumping = network.getOutput(7);
        nJumpingOneleg = network.getOutput(8);
        nSpeed = network.getOutput(9);
        nObstackles = network.getOutput(10);
        nBoardMovements = network.getOutput(11);
        nJumpDifficulty = network.getOutput(12);
        nNumberofSteps = network.getOutput(13);
        nOneLegSpeed = network.getOutput(14);
        nEarthquakeShake = network.getOutput(15);
    }

}

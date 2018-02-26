using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput : MonoBehaviour {

    public DFlowNetwork network;

    public float nManual;
    public float nLeaning;
    public float nOneLeg;
    public float nStepping;
    public float nJumping;
    public float nJumpingOneleg;
    public float nSpeed;
    public float nObstackles;
    public float nBoardMovements;

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
    }

}

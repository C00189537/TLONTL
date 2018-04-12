using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput : MonoBehaviour
{

    public DFlowNetwork network;
    public WorldController theWorld;

    private float nDifficulty = 0;
    private float nManual = 0;
    public float nLeaning = 0;
    public float nOneLeg = 0;
    public float nStepping = 0;
    public float nJumping = 0;
    public float nJumpingOneleg = 0;
    private float nSpeed = 0;
    public float nObstackles = 0;
    public float nBoardMovements = 0;
    public float nJumpDifficulty = 0;
    public float nNumberofSteps = 0;
    public float nOneLegDifficulty = 1;
    public float nEarthquakeShake = 0;
    public float nMomZ = 0;

    public float diffChange;
    public float speedChange;

    public float NManual
    {
        get
        {
            return nManual;
        }

        set
        {
            nManual = value;

        }
    }

    public float NDifficulty
    {
        get
        {
            return nDifficulty;
        }

        set
        {
            nDifficulty = value;
            diffChange = nDifficulty;
            Difficultycontroller.GetInstance().BeginDifficulty((int)NDifficulty);
        }
    }

    public float NSpeed
    {
        get
        {
            return nSpeed;
        }

        set
        {
            nSpeed = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        theWorld = gameObject.GetComponent<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {

        NManual = network.getOutput(3);
        nLeaning = network.getOutput(4);
        nOneLeg = network.getOutput(5);
        nStepping = network.getOutput(6);
        nJumping = network.getOutput(7);
        nJumpingOneleg = network.getOutput(8);
        NSpeed = network.getOutput(9);
        nObstackles = network.getOutput(10);
        nBoardMovements = network.getOutput(11);
        nJumpDifficulty = network.getOutput(12);
        nNumberofSteps = network.getOutput(13);
        nOneLegDifficulty = network.getOutput(14);
        nEarthquakeShake = network.getOutput(15);

        if (!(diffChange == network.getOutput(16)))
        {
            NDifficulty = network.getOutput(16);
        }

        nMomZ = network.getOutput(17);
    }



}

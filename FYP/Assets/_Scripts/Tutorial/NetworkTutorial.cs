using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTutorial : MonoBehaviour {

    public DFlowNetwork network;
    public StepTutorial step; 

    public float nLeaning = 0;
    public float nOneLeg = 0;
    public float nStepping = 0;
    public float nJumping = 0;
    public float nJumpingOneleg = 0;

    // Use this for initialization
    void Start () {
       // step = gameObject.GetComponent<StepTutorial>(); 
	}
	
	// Update is called once per frame
	void Update () {

        nLeaning = network.getOutput(4);
        nOneLeg = network.getOutput(5);
        nStepping = network.getOutput(6);
        nJumping = network.getOutput(7);
        nJumpingOneleg = network.getOutput(8);


        if (nLeaning == 1)
        {
            this.tag = "Lean";
        } else if (nOneLeg == 1)
        {
            this.tag = "OneLeg";
        } else if (nStepping == 1)
        {
            this.tag = "Step";
            if (step.Steps.Count <= 0 )
            {
                step.GenerateButtons(3);
            }

        } else if (nJumping == 1)
        {
            this.tag = "Jump";
        }
        else if (nJumpingOneleg == 1)
        {
            this.tag = "Jump2";
        }

    }
}

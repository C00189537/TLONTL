using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTutorial : MonoBehaviour {

    public DFlowNetwork network;
    public Camera cam;
    public GameObject leaning;
    public GameObject oneLeg;
    public GameObject stepping;
    public GameObject jumping;
    public GameObject jumpingTwo; 
    public float nLeaning = 0;
    public float nOneLeg = 0;
    public float nStepping = 0;
    public float nJumping = 0;
    public float nJumpingOneleg = 0;
    public float nMomZ = 0;



    // Use this for initialization
    void Start () {
        // step = gameObject.GetComponent<StepTutorial>(); 
        GameObject camOb = GameObject.FindWithTag("MainCamera");
        if (camOb != null)
        {
            cam = camOb.GetComponent<Camera>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        nLeaning = network.getOutput(4);
        nOneLeg = network.getOutput(5);
        nStepping = network.getOutput(6);
        nJumping = network.getOutput(7);
        nJumpingOneleg = network.getOutput(8);
        nMomZ = network.getOutput(17);

        if (nLeaning == 1)
        {
            leaning.transform.position = new Vector3(0, 0, 0);
            oneLeg.transform.position = new Vector3(40, 0, 0);
            stepping.transform.position = new Vector3(80, 0, 0);
            jumping.transform.position = new Vector3(120, 0, 0);
            jumpingTwo.transform.position = new Vector3(160, 0, 0);
            //this.transform.position = new Vector3(0, 1, 0);
            //cam.transform.position = new Vector3(0, 13, -30); 
        } else if (nOneLeg == 1)
        {
            leaning.transform.position = new Vector3(-40, 0, 0);
            oneLeg.transform.position = new Vector3(0, 0, 0);
            stepping.transform.position = new Vector3(40, 0, 0);
            jumping.transform.position = new Vector3(80, 0, 0);
            jumpingTwo.transform.position = new Vector3(120, 0, 0);
            //this.transform.position = new Vector3(40, 1, 0);
            //cam.transform.position = new Vector3(40, 13, -30);
        } else if (nStepping == 1)
        {
            leaning.transform.position = new Vector3(-80, 0, 0);
            oneLeg.transform.position = new Vector3(-40, 0, 0);
            stepping.transform.position = new Vector3(0, 0, 0);
            jumping.transform.position = new Vector3(40, 0, 0);
            jumpingTwo.transform.position = new Vector3(80, 0, 0);
            //this.transform.position = new Vector3(80, 1, 0);
            //cam.transform.position = new Vector3(80, 13, -30);
        } else if (nJumping == 1)
        {
            leaning.transform.position = new Vector3(-120, 0, 0);
            oneLeg.transform.position = new Vector3(-80, 0, 0);
            stepping.transform.position = new Vector3(40, 0, 0);
            jumping.transform.position = new Vector3(0, 0, 0);
            jumpingTwo.transform.position = new Vector3(40, 0, 0);
            //this.transform.position = new Vector3(120, 1, 0);
            //cam.transform.position = new Vector3(120, 13, -30);
        }
        else if (nJumpingOneleg == 1)
        {
            leaning.transform.position = new Vector3(-160, 0, 0);
            oneLeg.transform.position = new Vector3(-120, 0, 0);
            stepping.transform.position = new Vector3(-80, 0, 0);
            jumping.transform.position = new Vector3(-40, 0, 0);
            jumpingTwo.transform.position = new Vector3(0, 0, 0);
            //this.transform.position = new Vector3(160, 1, 0);
            //cam.transform.position = new Vector3(160, 13, -30);
        }

    }
}

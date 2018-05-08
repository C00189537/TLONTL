using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Tutorialscript : MonoBehaviour {

    public NetworkInput network;
    public WorldController worldController; 

    public int instructionsTimer = 10;
    public int waitTimer = 3;

  //  bool timer = false; 

    public Image instructions;
    public Text timeText; 

	// Use this for initialization
	void Start () {
        worldController = GetComponent<WorldController>(); 
        network = GetComponent<NetworkInput>(); 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator CountDown()
    {
        while (instructionsTimer > 0)
        {
            instructionsTimer--;
            instructions.enabled = true;
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator Wait()
    {
        while (waitTimer > 0)
        {
            timeText.text = waitTimer.ToString();
            waitTimer--;
           // timer = true;
            yield return new WaitForSeconds(1);
        }
    }

}

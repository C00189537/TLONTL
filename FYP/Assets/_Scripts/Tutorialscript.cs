using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Tutorialscript : MonoBehaviour {
    
    public WorldController theWorld;
    public PlayerController3 player;
    public GameObject playerObject;

    public int instructionsTimer = 10;
    public int waitTimer = 3;
    public Image instructions= null;
    public Text timeText;
    public Text instructionText; 

    public bool timer = false;

    public Image leanIns;
    public Image oneLegIns;
    public Image stepIns;
    public Image jumpIns;
    public Image jumpTwoIns;

    // Use this for initialization
    void Start () {
        theWorld = GetComponent<WorldController>(); 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController3>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        leanIns.enabled = false;
        oneLegIns.enabled = false;
        stepIns.enabled = false;
        jumpIns.enabled = false;
        jumpTwoIns.enabled = false; 
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    public void SetInstructions(int val)
    {
        switch (val)
        {
            case 1:
                instructions = leanIns;
                break;
            case 2:
                instructions = oneLegIns;
                break;
            case 3:
                instructions = jumpIns;
                break;
            case 4:
                instructions = stepIns;
                break;
            case 5:
                instructions = jumpTwoIns;
                break;
            default:
                break; 
        }
    }
    public IEnumerator WaitforInstructions()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(CountDown());
        yield break; 
    }

    public IEnumerator CountDown()
    {
        
        while (instructionsTimer > 0 && theWorld.tutorial)
        { 
            timer = true; 
            theWorld.speed = 0;
            instructionText.text = instructionsTimer.ToString();
            instructionsTimer--;
            instructions.enabled = true;
            
            yield return new WaitForSeconds(1);
        }

        if (instructionsTimer <= 0 || !theWorld.tutorial)
        {
            timer = false; 
            instructions.enabled = false;
            instructionText.text = ""; 
            StartCoroutine(Wait());
            yield break; 
        }
    }
    
    public IEnumerator Wait()
    {
        while (waitTimer > 0 && theWorld.tutorial)
        {
            timer = true; 
            theWorld.speed = 0;
            timeText.text = waitTimer.ToString();
            waitTimer--;
            
            yield return new WaitForSeconds(1);
        }

        if (waitTimer <= 0 || !theWorld.tutorial)
        {
            timer = false; 
            timeText.text = "";
            theWorld.speed = 0.1f;
            SetValues();
            yield break; 
        }
    }

    public void SetValues()
    {
        instructionsTimer = 10;
        waitTimer = 3;
    }


}

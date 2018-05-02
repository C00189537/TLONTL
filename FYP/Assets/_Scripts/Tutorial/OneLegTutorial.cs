using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class OneLegTutorial : MonoBehaviour {

    PlayerTutorial player;

    public Image instructions;

    public Image OtherInstructionsOne;
    public Image OtherInstructionsTwo;
    public Image OtherInstructionsThree;
    public Image OtherInstructionsFour;

    public Text timeText;
    public Image checkMark; 

    public int instructionsTimer = 10;
    public int waitTimer = 5;

    public bool timer = false;
    public bool gameStart = false;
    public bool gameEnd = false;

    // Use this for initialization
    void OnEnable () {

        player = GameObject.FindGameObjectWithTag("PlayerTut").GetComponent<PlayerTutorial>();
        StartCoroutine(CountDown());
        instructions.enabled = true;
        checkMark.enabled = false;


        OtherInstructionsOne.enabled = false;
        OtherInstructionsTwo.enabled = false;
        OtherInstructionsThree.enabled = false;
        OtherInstructionsFour.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        
        
        if (instructionsTimer <= 0 && !timer)
        {
            StopCoroutine(CountDown());
            instructions.enabled = false;
            StartCoroutine(Wait());
        }

        if (waitTimer <= 0 && !gameStart)
        {
            gameStart = true;
            timeText.text = "";
        }

        if (player.i >= 186 && player.j >= 186 && gameStart)
        {
            gameStart = false;
            gameEnd = true; 
        }
         
        if (gameEnd)
        {
            player.transform.position = new Vector3(0, 1, -8);
            StartCoroutine(End());
        }
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
            timer = true;
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator End()
    {
        checkMark.enabled = true;
        yield return new WaitForSeconds(4);
        TutorialWorld.LeanPlatform = false;
        TutorialWorld.OneLegPlatform = false;
        TutorialWorld.StepPlatform = true;
        TutorialWorld.JumpPlatform = false;
        TutorialWorld.JumpTwoPlatform = false;
    }
}

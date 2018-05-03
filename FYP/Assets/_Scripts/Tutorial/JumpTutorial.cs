using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTutorial : MonoBehaviour
{

    public PlayerTutorial player;
    public TutorialWorld tutWorld;
    public Image instructions;
    public Image OtherInstructionsOne;
    public Image OtherInstructionsTwo;
    public Image OtherInstructionsThree;
    public Image OtherInstructionsFour;

    public Image Go;
    public bool restPause = true;
    public bool coroutineStart = false;

    public Text timeText;
    public int instructionsTimer = 10;
    public int waitTimer = 5;


    public bool timer = false;
    public bool gameStart = false;
    public bool gameEnd = false;

    public Image checkMark;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("PlayerTut").GetComponent<PlayerTutorial>();
        tutWorld = GameObject.FindGameObjectWithTag("TutorialWorld").GetComponent<TutorialWorld>();

        StartCoroutine(CountDown());

        instructions.enabled = true;
        OtherInstructionsOne.enabled = false;
        OtherInstructionsTwo.enabled = false;
        OtherInstructionsThree.enabled = false;
        OtherInstructionsFour.enabled = false;

        checkMark.enabled = false;
        Go.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

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

        if (gameStart)
        {

            if (player.JumpScore >= 10 && gameStart)
            {
                gameStart = false;
                gameEnd = true;
            }

            if (!restPause)
            {
                player.transform.position += new Vector3(0, 0, 0.1f);
                StopCoroutine(Pause());
                coroutineStart = false;
            }


            if (player.transform.position.z > 28)
            {
                restPause = true;
                player.transform.position = new Vector3(0, 1, -8);

            }

            if (player.transform.position.z < -7 && restPause && !coroutineStart && !gameEnd)
            {
                coroutineStart = true;
                StartCoroutine(Pause());
            }
        }

        if (gameEnd)
        {
            StopCoroutine(Pause());
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
        StopCoroutine(Pause());
        Go.enabled = false; 
        checkMark.enabled = true;
        yield return new WaitForSeconds(4);
        TutorialWorld.LeanPlatform = false;
        TutorialWorld.OneLegPlatform = false;
        TutorialWorld.StepPlatform = false;
        TutorialWorld.JumpPlatform = false;
        TutorialWorld.JumpTwoPlatform = true;
    }

    public IEnumerator Pause()
    {
        Go.enabled = true;
        yield return new WaitForSeconds(0.3f);
        Go.enabled = false;
        yield return new WaitForSeconds(0.3f);
        Go.enabled = true;
        yield return new WaitForSeconds(0.3f);
        Go.enabled = false;
        restPause = false;
    }
}

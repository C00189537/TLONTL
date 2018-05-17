using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class JumpTwoTutorial : MonoBehaviour {

    public PlayerTutorial player;
    public TutorialWorld tutWorld;
    public Image instructions;
    public Image OtherInstructionsOne;
    public Image OtherInstructionsTwo;
    public Image OtherInstructionsThree;
    public Image OtherInstructionsFour;

    public GameObject LeftPanel;
    public GameObject RightPanel; 

    public Text timeText;
    public int instructionsTimer = 10;
    public int waitTimer = 5;

    public int side = 0; 

    public bool timer = false;
    public bool gameStart = false;
    public bool gameEnd = false;

    public bool LeftOn = true;
    public bool RightOn = true;

    public bool coroutineDone = true;
    public bool odd = true; 
    public bool firstPanel = true; 

    public bool receiveScore;
    public bool Stepleft = false;
    public bool Stepright = false; 

    public Image checkMark;

    System.Random rand = new System.Random();


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

        LeftPanel = gameObject.transform.Find("LeftPanel").gameObject;
        RightPanel = gameObject.transform.Find("RightPanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        LeftPanel.SetActive(LeftOn);
        RightPanel.SetActive(RightOn);

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
            if (player.transform.position.y < 1.5)
            {
                player.ResetPlayer(); 
            }
           
            if (firstPanel)
            {
                side = rand.Next(1, 3);
                if (side == 1)
                {
                    RightOn = false;
                    LeftOn = true; 
                } else
                {
                    RightOn = true; 
                    LeftOn = false; 
                }

                firstPanel = false; 
            }

            if (coroutineDone)
            {
                coroutineDone = false; 
                StartCoroutine(Interval());
                
            }
        }

        if (gameEnd)
        {
            player.transform.position = new Vector3(0, 1, -8);  
            StartCoroutine(End());
        }
    }

    public IEnumerator Interval()
    {
       

        if (side == 1)
        {
            if (odd)
            {
                yield return new WaitForSeconds(1);
                RightOn = !RightOn;
                yield return new WaitForSeconds(1);
                LeftOn = !LeftOn;
                coroutineDone = true;
                odd = false; 
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(1);
                LeftOn = !LeftOn;
                yield return new WaitForSeconds(1);
                RightOn = !RightOn;
                coroutineDone = true;
                odd = true; 
                yield break;
            }
            
        }
        else
        {
            if (odd)
            {
                yield return new WaitForSeconds(1);
                LeftOn = !LeftOn;
                yield return new WaitForSeconds(1);
                RightOn = !RightOn;
                coroutineDone = true;
                odd = false; 
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(1);
                RightOn = !RightOn;
                yield return new WaitForSeconds(1);
                LeftOn = !LeftOn;
                coroutineDone = true;
                odd = true;
                yield break;
            }
        }


        //int waitInterval = rand.Next(1, 3);
        
        //RightOn = !RightOn;
        //LeftOn = !LeftOn;
        //yield return new WaitForSeconds(waitInterval);
        
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
        TutorialWorld.LeanPlatform = true;
        TutorialWorld.OneLegPlatform = false;
        TutorialWorld.StepPlatform = false;
        TutorialWorld.JumpPlatform = false;
        TutorialWorld.JumpTwoPlatform = false;
    }
}

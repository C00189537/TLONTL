using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanTutorial : MonoBehaviour
{

    public PlayerTutorial player;
    public TutorialWorld tutWorld; 
    public Image instructions;
    public Image OtherInstructionsOne; 


    public GameObject collectPrefab;
    public int nrOfCollect = 0;

    public Text timeText;
    public int instructionsTimer = 10;
    public int waitTimer = 5;
   

    public bool timer = false;
    public bool gameStart = false;
    public bool gameEnd = false;

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

        checkMark.enabled = false; 
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

        if (gameStart && nrOfCollect <= 3 && !gameEnd)
        {
            nrOfCollect++;
            int xPos = rand.Next(-13, 13);
            Instantiate(collectPrefab, new Vector3(xPos, 1, -8), Quaternion.identity);
        }           

        if (player.Leanscore >= 25)
        {
            gameEnd = true;
            gameStart = false;
        }

        if (gameEnd)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Coin");
            foreach( GameObject coin in objects){
                Destroy(coin);
            }

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
        TutorialWorld.OneLegPlatform = true; 
    }
}

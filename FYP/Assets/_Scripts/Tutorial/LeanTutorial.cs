using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanTutorial : MonoBehaviour
{

    PlayerTutorial player;
    public Image instructions;
    public Text timeText;
    public GameObject collectPrefab; 

    public int instructionsTimer = 10;
    public int waitTimer = 5;
    public int nrOfCollect = 0; 

    public bool timer = false;
    public bool gameStart = false;

    System.Random rand = new System.Random();

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTut").GetComponent<PlayerTutorial>();
        StartCoroutine(CountDown());
        instructions.enabled = true; 
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

        if (gameStart && nrOfCollect <= 3)
        {
            int xPos = rand.Next(-13, 13);
            Instantiate(collectPrefab, new Vector3(xPos, 1, -8), Quaternion.identity);
            nrOfCollect++; 
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
}

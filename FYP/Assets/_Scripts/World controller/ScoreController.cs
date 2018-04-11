using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreController : MonoBehaviour {

    private static ScoreController instance;
    public WorldController theWorld;
    public PlayerController3 player; 

    public Text scoreText;

    public double score = 0;
    public int scoreInt = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public static ScoreController GetInstance()
    {
        return instance;
    }
    
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
    }
	
	void Update () {
        UpdateScore();
	}

    public void AddScore(int value)
    {
        score = score + value;
        FloatingTextController.CreateFLoatingText(value.ToString(), player.transform, 0);
    }

    //Insert a negative value between the brackets
    public void SubtractScore(int value)
    {
        score = score + value;
        FloatingTextController.CreateFLoatingText(value.ToString(), player.transform, 1);
    }

    void setScoreText()
    {
        scoreInt = (int)score;
        scoreText.text = scoreInt.ToString();
    }

    void UpdateScore()
    {
        score += theWorld.speed * Time.deltaTime * 5;
        if (score < 0)
        {
            score = 0;
        }
        setScoreText();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    private static ScoreController instance;
    public WorldController theWorld;
    public PlayerController3 player;

    public Text scoreText;

    public double score = 0;
    public int scoreInt = 0;

    public float duration = 2f;
    float time;
    Color currentColor;

    public int currentSize = 45;
    public int newSize = 70;
    public int standardSize = 45;

    //Score values
    public int Collectable = 10;
    public int Obstacle = -10;
    public int FallingOff = -10;
    public int NotFallingOff = 25;
    public int Step = 10;
    public int StepsLeft = -10;
    public int PerfectJump = 15;

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

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
        currentColor = Color.white;
        scoreText.fontSize = standardSize; 
    }

    void Update()
    {
        if (currentColor == Color.green || currentColor == Color.red)
        {
            LerpColor();
        }

        if (currentColor.r == 0 && currentColor.g == 0 && currentColor.b == 0)
        {
            currentColor = Color.white;
        }

        UpdateScore();

        if (currentSize > standardSize)
        {
            LerpSize();
        }
    }

    public void LerpColor()
    {
        Color color = Color.Lerp(currentColor, Color.white, time);
        time += Time.deltaTime / duration;
        scoreText.color = color;
    }

    public void LerpSize()
    {
        currentSize--;
        scoreText.fontSize = currentSize;
    }

    public void AddScore(int value)
    {
        int variable = 1;
        if (theWorld.input.NManual == 0)
        {
            variable = Difficultycontroller.GetInstance().difficulty;
        }
        if (theWorld.input.NManual == 1)
        {
            variable = (int)theWorld.input.nJumpDifficulty;
        }
        score = score + (value*variable);

        scoreText.color = Color.green;
        currentColor = Color.green;

        currentSize = newSize;

        time = 0;
        FloatingTextController.CreateFLoatingText(value.ToString(), player.transform, 0);
    }

    //Insert a negative value between the brackets
    public void SubtractScore(int value)
    {
        score = score + value;

        scoreText.color = Color.red;
        currentColor = Color.red;

        currentSize = newSize;

        time = 0;
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

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
    }

    void Update()
    {
        if (currentColor == Color.green || currentColor == Color.red)
        {
            Color color = Color.Lerp(currentColor, Color.white, time);
            time += Time.deltaTime / duration;
            scoreText.color = color;
        }

        if (currentColor.r == 0 && currentColor.g == 0 && currentColor.b == 0)
        {
            currentColor = Color.white;
        }

        UpdateScore();
    }

    public void AddScore(int value)
    {
        score = score + value;
        scoreText.color = Color.green;
        currentColor = Color.green;
        time = 0;
        FloatingTextController.CreateFLoatingText(value.ToString(), player.transform, 0);
    }

    //Insert a negative value between the brackets
    public void SubtractScore(int value)
    {
        score = score + value;
        scoreText.color = Color.red;
        currentColor = Color.red;
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

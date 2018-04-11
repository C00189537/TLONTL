using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficultycontroller : MonoBehaviour
{
    private static Difficultycontroller instance;

    public WorldController theWorld;

    public int difficulty = 0;
    public int difficultyScore = 0;
    public int platformScore = 0;

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

    public static Difficultycontroller GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
    }

    public void BeginDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                difficultyScore = 2;
                break;
            case 2:
                difficultyScore = 7;
                break;
            case 3:
                difficultyScore = 20;
                break;
            case 4:
                difficultyScore = 35;
                break;
            case 5:
                difficultyScore = 55;
                break;
            default:
                break;
        }
    }

    public void UpdateDifficultyScore()
    {
        //Contain within 0-70
        if (difficultyScore < 0)
        {
            difficultyScore = 0;
        }
        else if (difficultyScore >= 70)
        {
            difficultyScore = 70;
        }
        //Difficulty set based on performance
        if (difficultyScore >= 0 && difficultyScore < 5)
        {
            difficulty = 1;
            theWorld.oneLegDif = 1;
        }
        else if (difficultyScore >= 5 && difficultyScore < 15)
        {
            difficulty = 2;
            theWorld.oneLegDif = 2;
        }
        else if (difficultyScore >= 15 && difficultyScore < 30)
        {
            difficulty = 3;
            theWorld.oneLegDif = 3;
        }
        else if (difficultyScore >= 30 && difficultyScore < 50)
        {
            difficulty = 4;
            theWorld.oneLegDif = 4;
        }
        else if (difficultyScore >= 50 && difficultyScore < 70)
        {
            difficulty = 5;
            theWorld.oneLegDif = 5;
        }
    }

    public void setPlatformScore()
    {
        switch (difficulty)
        {
            case 1:
                platformScore = 1;
                break;
            case 2:
                platformScore = 2;
                break;
            case 3:
                platformScore = 3;
                break;
            case 4:
                platformScore = 4;
                break;
            case 5:
                platformScore = 5;
                break;
            default:
                break;
        }
    }

    public void CalculateDifficultyScore()
    {
        if (platformScore == 0)
        {
            switch (difficulty)
            {
                case 1:
                    difficultyScore--;
                    break;
                case 2:
                    difficultyScore -= 2;
                    break;
                case 3:
                    difficultyScore -= 3;
                    break;
                case 4:
                    difficultyScore -= 4;
                    break;
                default:
                    break;
            }
        }
        else
        {
            difficultyScore += platformScore;
        }
    }

}

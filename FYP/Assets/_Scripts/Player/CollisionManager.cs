using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    WorldController gameController;
    Stepping stepController;
    PlayerController3 playerController;
    Camera cam;
    BoardMovement movement;


    private int platformScore;

    void Start()
    {
        
        //access the world controller for updating speed + score
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<WorldController>();
        }
        else if (gameController == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }

        movement = gameControllerObject.GetComponent<BoardMovement>();

        //Get player script
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (gameControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController3>();
        }
        GameObject stepControllerObject = GameObject.FindWithTag("GameController");
        if (stepControllerObject != null)
        {
            stepController = stepControllerObject.GetComponent<Stepping>();
        }
        else if (playerController == null)
        {
            Debug.Log("cannot find 'PlayerController' script");
        }
  
        GameObject camOb = GameObject.FindWithTag("MainCamera");
        if (camOb != null)
        {
            cam = camOb.GetComponent<Camera>();
        }
        else if (camOb == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Player vs Obstacle
        if (other.gameObject.tag == "Obstacle")
        {
            movement.Boardvibration();
            Debug.Log("explode?");
            Destroy(other.gameObject);
            //gameController.UpdateSpeed(-0.025f);
            cam.GetComponent<CameraShake>().SetTimer(1.0f);

            if (platformScore > 0)
            {
                platformScore--;
            }
            //palpitation TBD
            

        }
        else if (other.gameObject.tag == "Basic")
        {
            playerController.basic = true;
            movement.ResetBoard();
        }
        else if (other.gameObject.tag == "Pit")
        {
            gameObject.GetComponent<PlayerController3>().ResetPlayer();
            if (platformScore > 0)
            {
                platformScore--;
            }
            playerController.pit = true;
            playerController.basic = false;
        }
        else if (other.gameObject.tag == "Coin")
        {
            gameController.score += 20;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Killer")
        {
            playerController.basic = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Lean")
        {
            platformScore = 3;
            playerController.pit = false;
            playerController.basic = false;
            switch (gameController.difficulty)
            {
                case 1:
                    platformScore = 0;
                    break;
                case 2:
                    platformScore = 1;
                    break;
                case 3:
                    platformScore = 2;
                    break;
                case 4:
                    platformScore = 3;
                    Debug.Log("its me");
                    break;
                default:
                    break;
            }
            movement.BoardMovements();
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            //gameController.oneLegSpeed = true;
            playerController.pit = false;
            playerController.basic = false;
            platformScore = 1;
            movement.BoardMovements();
        }
        else if (other.gameObject.tag == "Step")
        {
            playerController.pit = false;
            playerController.basic = false;
            gameController.SteppingStones();
            switch (gameController.difficulty)
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
                default:
                    break;
            }

        }
        else if (other.gameObject.tag == "Jump")
        {
            playerController.pit = false;
            playerController.basic = false;
            switch (gameController.difficulty)
            {
                case 1:
                    platformScore = 1;
                    break;
                case 2:
                    platformScore = 1;
                    break;
                case 3:
                    platformScore = 2;
                    break;
                case 4:
                    platformScore = 2;
                    break;
                default:
                    break;
            }
        }
        else if (other.gameObject.tag == "Jump2")
        {
            playerController.pit = false;
            playerController.basic = false;
            switch (gameController.difficulty)
            {
                case 1:
                    platformScore = 2;
                    break;
                case 2:
                    platformScore = 2;
                    break;
                case 3:
                    platformScore = 3;
                    break;
                case 4:
                    platformScore = 3;
                    break;
                default:
                    break;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Lean")
        {
            playerController.lean = true;
            playerController.basic = false; 
        }
        else if (other.gameObject.tag == "Basic")
        {
            playerController.basic = true;
            movement.ResetBoard();
        }
        else if (other.gameObject.tag == "Pit")
        {
            playerController.pit = true; 
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            playerController.oneLeg = true;
            playerController.basic = false;
        }
        else if (other.gameObject.tag == "Step")
        {
            playerController.step = true;
            playerController.basic = false;
        }
        else if (other.gameObject.tag == "Jump")
        {
            playerController.jump = true;
            playerController.basic = false;
        }
        else if (other.gameObject.tag == "Jump2")
        {
            playerController.jump2 = true;
            playerController.basic = false;
        }

    }
    //Leaving each platform
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lean")
        {
            playerController.lean = false;
           // playerController.basic = true;
            //Difficulty check
            if (platformScore == 0)
            {
                switch (gameController.difficulty)
                {
                    case 2:
                        gameController.difficultyScore--;
                        break;
                    case 3:
                        gameController.difficultyScore -= 2;
                        break;
                    case 4:
                        gameController.difficultyScore -= 3;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                gameController.difficultyScore += platformScore;
            }
        }
        else if (other.gameObject.tag == "Basic")
        {
            playerController.basic = false;
        }
        else if (other.gameObject.tag == "Pit")
        {
            playerController.pit = false;
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            playerController.oneLeg = false;
           // playerController.basic = true;
            //Difficulty check
            if (platformScore == 0)
            {
                gameController.difficultyScore--;
            }
            else
            {
                gameController.difficultyScore += platformScore;
            }
        }
        else if (other.gameObject.tag == "Step")
        {
            playerController.step = false; 
            //playerController.basic = true;
            platformScore -= stepController.getSteps();
            //Difficulty check
            if (platformScore == 0)
            {
                switch (gameController.difficulty)
                {
                    case 1:
                        gameController.difficultyScore--;
                        break;
                    case 2:
                        gameController.difficultyScore -= 2;
                        break;
                    case 3:
                        gameController.difficultyScore -= 3;
                        break;
                    case 4:
                        gameController.difficultyScore -= 4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                gameController.difficultyScore += platformScore;
            }
        }
        else if (other.gameObject.tag == "Jump")
        {
            playerController.jump = false;
            //Difficulty check
            if (platformScore == 0)
            {
                switch (gameController.difficulty)
                {
                    case 1:
                        gameController.difficultyScore--;
                        break;
                    case 2:
                        gameController.difficultyScore -= 2;
                        break;
                    case 3:
                        gameController.difficultyScore -= 3;
                        break;
                    case 4:
                        gameController.difficultyScore -= 4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                gameController.difficultyScore += platformScore;
            }
        }
        else if (other.gameObject.tag == "Jump2")
        {
            playerController.jump2 = false;
            //Difficulty check
            if (platformScore == 0)
            {
                switch (gameController.difficulty)
                {
                    case 1:
                        gameController.difficultyScore -= 2;
                        break;
                    case 2:
                        gameController.difficultyScore -= 2;
                        break;
                    case 3:
                        gameController.difficultyScore -= 3;
                        break;
                    case 4:
                        gameController.difficultyScore -= 3;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                gameController.difficultyScore += platformScore;
            }
        }
    }

}
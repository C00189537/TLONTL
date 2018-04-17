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

    int hitObstacles = 0;

    void Start()
    {

        //access the world controller for updating speed
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
            AudioManager.GetInstance().audiosource.PlayOneShot(AudioManager.GetInstance().HitObstacle, 0.5f);
            gameController.CamShake();
            Destroy(other.gameObject);
            StartCoroutine(movement.ObstacleHit());
            hitObstacles++;

            if (Difficultycontroller.GetInstance().platformScore > 0)
            {
                Difficultycontroller.GetInstance().platformScore--;
            }
        }
        else if (other.gameObject.tag == "Basic")
        {
            playerController.basic = true;
            playerController.fallOff = 0;
            hitObstacles = 0;
        }
        else if (other.gameObject.tag == "End")
        {
            if (playerController.fallOff <= 0 && hitObstacles <= 0)
            {
                ScoreController.GetInstance().AddScore(ScoreController.GetInstance().NotFallingOff);
            }
        }
        else if (other.gameObject.tag == "Pit")
        {
            gameObject.GetComponent<PlayerController3>().ResetPlayer();

            if (Difficultycontroller.GetInstance().platformScore > 0)
            {
                Difficultycontroller.GetInstance().platformScore--;
            }
            playerController.pit = true;
            playerController.basic = false;

        }
        else if (other.gameObject.tag == "Coin")
        {
            AudioManager.GetInstance().audiosource.PlayOneShot(AudioManager.GetInstance().Collectable, 0.5f);
            ScoreController.GetInstance().AddScore(ScoreController.GetInstance().Collectable);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Killer")
        {
            playerController.basic = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Lean")
        {
            playerController.pit = false;
            playerController.basic = false;
            Difficultycontroller.GetInstance().setPlatformScore();
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            playerController.pit = false;
            playerController.basic = false;

            if (playerController.fallOff == 0)
            {
                Difficultycontroller.GetInstance().platformScore = 1;
            }

        }
        else if (other.gameObject.tag == "Step")
        {
            playerController.pit = false;
            playerController.basic = false;
            gameController.SteppingStones();
            Difficultycontroller.GetInstance().setPlatformScore();

        }
        else if (other.gameObject.tag == "Jump")
        {
            playerController.pit = false;
            playerController.basic = false;
            Difficultycontroller.GetInstance().setPlatformScore();
        }
        else if (other.gameObject.tag == "Jump2")
        {
            playerController.pit = false;
            playerController.basic = false;

            if (playerController.fallOff == 0)
            {
                Difficultycontroller.GetInstance().platformScore = 1;
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
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lean")
        {
            playerController.lean = false;
            Difficultycontroller.GetInstance().CalculateDifficultyScore();
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

        }
        else if (other.gameObject.tag == "End")
        {
            Difficultycontroller.GetInstance().CalculateDifficultyScore();

        }
        else if (other.gameObject.tag == "Step")
        {
            playerController.step = false;
            
            Difficultycontroller.GetInstance().platformScore -= stepController.getSteps();
            Difficultycontroller.GetInstance().CalculateDifficultyScore();
        }
        else if (other.gameObject.tag == "Jump")
        {
            playerController.jump = false;
            Difficultycontroller.GetInstance().CalculateDifficultyScore();
        }
        else if (other.gameObject.tag == "Jump2")
        {
            playerController.jump2 = false;
            Difficultycontroller.GetInstance().CalculateDifficultyScore();
        }
    }

}
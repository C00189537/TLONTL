using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    WorldController gameController;
    Stepping stepController;
    PlayerController3 playerController;
    Camera cam;
   // Stepping stepping; 

    private int platformScore;

    void Start()
    {
     //   stepping = GameObject.Find("WorldController").GetComponent<Stepping>();
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
        Debug.Log("Hit");
        //Player vs Obstacle
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("explode?");
            Destroy(other.gameObject);
            gameController.UpdateSpeed(-0.025f);
            cam.GetComponent<CameraShake>().SetTimer(1.0f);
            Debug.Log("bifbwbf");

            if (platformScore > 0)
            {
                platformScore--;
            }
            //palpitation TBD

        }
        else if (other.gameObject.tag == "Pit")
        {
            gameObject.GetComponent<PlayerController2>().ResetPlayer();
            if (platformScore > 0)
            {
                platformScore--;
            }
            Debug.Log("Fallen");
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
            Debug.Log("Start Leaning");
            platformScore = 3;
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            Debug.Log("Start OneLeg");
            gameController.oneLegSpeed = true;
           platformScore = 3;
        }
        else if (other.gameObject.tag == "Step")
        {
            Debug.Log(" Start Steping");
            gameController.SteppingStones();
            switch (gameController.difficulty)
            {
                case 0:
                    platformScore = 1;
                    break;
                case 1:
                    platformScore = 2;
                    break;
                case 2:
                    platformScore = 3;
                    break;
                case 3:
                    platformScore = 4;
                    break;
                default:
                    break;
            }

        }
        else if (other.gameObject.tag == "Jump")
        {
            Debug.Log("Start Jumping");
            platformScore = 3;
        }
        else if (other.gameObject.tag == "Jump2")
        {
            Debug.Log("Start Jumping 2");
            platformScore = 3;
        }
    }
    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Lean")
        {
            Debug.Log("Lean");
            playerController.lean = true;
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            Debug.Log("OneLeg");
            playerController.oneLeg = true;
        }
        else if (other.gameObject.tag == "Step")
        {
            //Debug.Log("Step");
            playerController.step = true;
        }
        else if (other.gameObject.tag == "Jump")
        {
            Debug.Log("Jump");
            playerController.jump = true;
        }
        else if (other.gameObject.tag == "Jump2")
        {
            Debug.Log("Jump2");
            playerController.jump2 = true;
        }

    }
    //Leaving each platform
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.gameObject.tag == "Lean")
        {
            Debug.Log("Lean");
            playerController.lean = false;
            playerController.basic = true;
            Debug.Log(platformScore);
            gameController.UpdateSpeed(0.015f * platformScore);

        }
        else if (other.gameObject.tag == "OneLeg")
        {
            Debug.Log("OneLeg");
            playerController.oneLeg = false;
            playerController.basic = true;
            Debug.Log(platformScore);
            gameController.UpdateSpeed(0.015f * platformScore);
            gameController.oneLegSpeed = false;
        }
        else if (other.gameObject.tag == "Step")
        {
            Debug.Log("Step");
            playerController.step = false;
            //stepping.EmptyQueue();
            playerController.basic = true;
            platformScore -= stepController.getSteps();
            Debug.Log(platformScore);
            gameController.UpdateSpeed(0.015f * platformScore);
        }
        else if (other.gameObject.tag == "Jump")
        {
            Debug.Log("Jump");
            playerController.jump = false;
            playerController.basic = true;
            gameController.UpdateSpeed(0.015f * platformScore);
        }
        else if (other.gameObject.tag == "Jump2")
        {
            Debug.Log("Jump2");
            playerController.jump2 = false;
            playerController.basic = true;
            gameController.UpdateSpeed(0.015f * platformScore);
        }
    }

}
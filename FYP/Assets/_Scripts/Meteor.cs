using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    System.Random randy = new System.Random();
    Camera cam;


    public float timeStamp;
    public float waitTime;

    public float shakeTimeStamp;
    public float shakeWaitTime = 4; 

    public BoardMovement board;
    public WorldController theWorld; 


    // Use this for initialization
    void Start()
    {
        setTimer();
        GameObject camOb = GameObject.FindWithTag("MainCamera");
        if (camOb != null)
        {
            cam = camOb.GetComponent<Camera>();
        }
        theWorld = GameObject.FindGameObjectWithTag("GameController").GetComponent<WorldController>(); 

        board = GameObject.FindGameObjectWithTag("GameController").GetComponent<BoardMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!theWorld.tutorial)
        {
            if (gameObject.transform.position.y <= 1.0f && shakeTimeStamp < Time.time)
            {
                board.BoardMovements();
                cam.GetComponent<CameraShake>().SetShakeAmount(0.1f);
                cam.GetComponent<CameraShake>().SetTimer(1.0f);
                shakeTimeStamp = Time.time + shakeWaitTime;
            }

            if (timeStamp < Time.time)
            {
                gameObject.transform.position += Vector3.down * 0.5f;
            }

            if (gameObject.transform.position.y <= -3)
            {
                gameObject.transform.position = new Vector3(0, 22, 24);
                setTimer();
            }
        }

      
    }

    public void setTimer()
    {
        waitTime = randy.Next(15, 25);
        timeStamp = waitTime + Time.time;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

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

    public Image warningImage;
    public bool coroutineWarning = false; 
    public bool warning = false; 


    // Use this for initialization
    void Start()
    {
        warningImage.enabled = false; 
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

            if (timeStamp < Time.time && !coroutineWarning)
            {
                coroutineWarning = true;
                StartCoroutine(warningsign());
            }

            if (timeStamp < Time.time && warning)
            {
                gameObject.transform.position += Vector3.down * 0.5f;
            }
            
            if (gameObject.transform.position.y <= -3f)
            {
                setTimer();
                gameObject.transform.position = new Vector3(0, 22, 24);
                
            }
        }

      
    }

    public void setTimer()
    {
        coroutineWarning = false; 
        warning = false;
        waitTime = randy.Next(10, 20);
        timeStamp = waitTime + Time.time;
    }

    public IEnumerator warningsign()
    {
        
        warningImage.enabled = true;
        yield return new WaitForSeconds(0.7f);
        warningImage.enabled = false;
        yield return new WaitForSeconds(0.7f);
        warningImage.enabled = true;
        yield return new WaitForSeconds(0.7f);
        warningImage.enabled = false;
        warning = true; 
        yield break; 
    }

}

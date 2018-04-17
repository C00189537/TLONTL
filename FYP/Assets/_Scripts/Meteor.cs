using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    System.Random randy = new System.Random();
   

    public float timeStamp;
    public float waitTime; 

    // Use this for initialization
    void Start () {

        // StartCoroutine(Earthquake());
    }

    // Update is called once per frame
    void Update () {

        if (timeStamp < Time.time)
        {
            gameObject.transform.position += Vector3.down * 0.5f;
        }

        if (gameObject.transform.position.y <= -50)
        {
            gameObject.transform.position = new Vector3(0, 22, 24);
            setTimer();
        }
	}

    public void setTimer()
    {
        waitTime = randy.Next(15, 25);
        timeStamp = waitTime + Time.time; 
    }

}

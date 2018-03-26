using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour {

    WorldController theWorld;
    public float posY;
    public Vector3 arrowPos; 
	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            theWorld = gameControllerObject.GetComponent<WorldController>();
        }
        else if (theWorld == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }
    }
	
	// Update is called once per frame
	void Update () {
        posY= gameObject.transform.localPosition.y - theWorld.speed * 5;
        arrowPos = new Vector3(gameObject.transform.localPosition.x, posY, gameObject.transform.localPosition.z);
        gameObject.transform.localPosition = arrowPos;
	}
}

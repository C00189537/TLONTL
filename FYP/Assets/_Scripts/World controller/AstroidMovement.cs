using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMovement : MonoBehaviour {

    WorldController theWorld;
    public float posZ;
    public float killpoint = -10;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        posZ = gameObject.transform.localPosition.z - theWorld.speed/4;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, posZ); 

        if (gameObject.transform.position.z < killpoint)
        {
            Destroy(this.gameObject);
        }
    }
}

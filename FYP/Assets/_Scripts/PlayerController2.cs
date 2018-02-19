using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    WorldController gameController;
    
    public float xVal;

    private bool right, left;
    public float translateSpeed;
    public float testSpeed;
    public bool basic, lean, oneLeg, step, jump, jump2;

    public DynSTABLE platform;
    public Vector3 pos;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<WorldController>();
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {

       // pos = transform.localPosition;

        pos.x = 10 * platform.cop.x;

      //  new Vector3(platform.cop.x * 10, 1, 0);

        gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, 0 );

       
		//gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        /*Inputs();
        if (lean || basic)
        {
            Lean();
        }
        else if(oneLeg)
        {
            OneLeg();
        }
        else if (step)
        {
            Step();
        }
        else if(jump)
        {
            Jump();
        }
        else if (jump2)
        {
            Jump2();
        }
        //OneLeg();
        */
	}
    void Lean()
    {
        //Get From Dynstable Script
        //gameObject.transform.Translate( speedFromDynstable / scalerToFitGame, 0, 0);
        if (left)
        {
            gameObject.transform.Translate(-testSpeed, 0, 0);
        }
        else if (right)
        {
            gameObject.transform.Translate(testSpeed, 0, 0);
        }
    }
    void OneLeg()
    {
        if (right)
        {
            gameObject.transform.Translate((xVal - gameObject.transform.position.x) / translateSpeed, 0, 0);

        }
        else if (left)
        {
            gameObject.transform.Translate(-((xVal + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
        else if (!right)
        {
            gameObject.transform.Translate(-((0 + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
        else if (!left)
        {
            gameObject.transform.Translate(-((0 + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
    }
    void Step()
    {

    }
    void Jump()
    {

    }
    void Jump2()
    {

    }
    void Inputs()
    {
        if (Input.GetKeyDown("right"))
        {
            right = true;
            left = false;
        }
        else if (Input.GetKeyDown("left"))
        {
            right = false;
            left = true;
        }
        if (Input.GetKeyUp("right"))
        {
            right = false;
        }
        else if (Input.GetKeyUp("left"))
        {
            left = false;
        }
    }
    public void ResetPlayer()
	{
        gameObject.transform.position = new Vector3(0, 3, 0);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

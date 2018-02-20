using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour {

    public DynSTABLE platform; 
    //WorldController gameController;
    public Stepping stepping; 
    
    public float xVal;

    private bool right, left;
    public float translateSpeed;
    public float testSpeed;
    public float jumpSpeed;
    public bool basic, lean, oneLeg, step, jump, jump2;

	// Use this for initialization
	void Start () {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        /*  if (gameControllerObject != null)
         {
             gameController = gameControllerObject.GetComponent<WorldController>();
         }
         */
        stepping = gameControllerObject.GetComponent<Stepping>();
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0 );
		gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        Inputs();
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


	}
    void Lean()
    {
        
            gameObject.transform.Translate(platform.cop.x * testSpeed, 0, 0);
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
        if (platform.cop.x > 0.5f)// && (platform.cop.z < -0.1 || platform.cop.z > 0.1))
        {
            stepping.DestroyRight();
        }

        if (platform.cop.z > 0.5f)
        {
            stepping.DestroyUp();
        }

        if (platform.cop.x < -0.5f)// && (platform.cop.z < -0.1 || platform.cop.z > 0.1))
        {
            stepping.DestroyLeft();
        }

        if (platform.cop.z < -0.5)
        {
            stepping.DestroyDown();
        }
    }

    void Jump()
    {
        if (jump)
            gameObject.transform.Translate(0, jumpSpeed, 0);
    }
    void Jump2()
    {
        if (jump)
            gameObject.transform.Translate(0, jumpSpeed, 0);
    }
    void Inputs()
    {
       if (platform.cop.x < 0)
        {
            left = true;
            right = false; 
        }

       if (platform.cop.x > 0)
        {
            left = false;
            right = true; 
        }

       if (platform.cop.force > 0)
        {
            jump = false;
        }

       if (platform.cop.force <= 0)
        {
            jump = true; 
        }

    }
    public void ResetPlayer()
	{
        gameObject.transform.position = new Vector3(0, 3, 0);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

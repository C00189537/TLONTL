using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour {

    public DynSTABLE platform; 
    public Stepping stepping;
    public WorldController theWorld; 

    public float xVal;

    private bool leanLeft, LeanRight, jumping2;
    public float translateSpeed;
    public float LeanSpeed;
    public float jumpSpeed;
    public float jumpFallSpeed; 
    public float jump2Speed; 
    public float switchJumpSpeed;
    public float OneLegValue;
    public float StepValue;
    public float JumpValue;
    public float JumpTwoValue;
    public bool basic, lean, oneLeg, step, jump, jump2, pit;
    public bool touchGround = true;

    public Rigidbody rb; 

    public float force;
    public float xpos;
    public float zpos; 
	// Use this for initialization
	void Start () {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
       stepping = gameControllerObject.GetComponent<Stepping>();
       theWorld = gameControllerObject.GetComponent<WorldController>();
        rb = GetComponent<Rigidbody>();
        
    }


    private void FixedUpdate()
    {
        if (platform.cop.force >= JumpValue && touchGround == false)
        {
            rb.AddForce(Vector3.down * jumpFallSpeed, ForceMode.Impulse);
        }

        if (gameObject.transform.position.y < 1.01f)
        {
            touchGround = true;
        }

        if (jump)
        {
            Jump();

        }
        else if (jump2)
        {
            Jump2();
            
        }
    }
    // Update is called once per frame
    void Update () {
        force = platform.cop.force;
        xpos = platform.cop.x;
        zpos = platform.cop.z;
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
         
        gameObject.transform.Rotate(new Vector3(0, 0, 3000 * Time.deltaTime * theWorld.speed), Space.Self);
    }
    void Lean()
    {
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
    }
    void OneLeg()
    {
        if (LeanRight)
        {
            gameObject.transform.Translate((xVal - gameObject.transform.position.x) / translateSpeed, 0, 0);

        }
        else if (leanLeft)
        {
            gameObject.transform.Translate(-((xVal + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
        else if (!LeanRight)
        {
            gameObject.transform.Translate(-((0 + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
        else if (!leanLeft)
        {
            gameObject.transform.Translate(-((0 + gameObject.transform.position.x) / translateSpeed), 0, 0);
        }
    }
    void Step()
    {
        if (platform.cop.x > StepValue)
        {
            stepping.DestroyRight();
        }

        if (platform.cop.z <= -StepValue)
        {
            stepping.DestroyUp();
        }

        if (platform.cop.x < -StepValue)
        {
            stepping.DestroyLeft();
        }
        
    }

    void Jump()
    {
        if (platform.cop.force < JumpValue && touchGround == true)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            touchGround = false;
        }
        
        if (gameObject.transform.position.x < 0 || gameObject.transform.position.x > 0)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, 0);
        }
    }
    void Jump2()
    {
        float step = switchJumpSpeed * Time.deltaTime;
        if (platform.cop.force < JumpValue && touchGround == true)
        {
            rb.AddForce(Vector3.up * jump2Speed, ForceMode.Impulse);
            touchGround = false;
        }

        if (platform.cop.x < -StepValue && !jumping2)
        {
           gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(-3, gameObject.transform.position.y, 0), step); 
             
        }

        if (platform.cop.x > -StepValue && platform.cop.x < StepValue && !jumping2)
        {
            gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(0, gameObject.transform.position.y, 0), step);
        }

        if (platform.cop.x > StepValue && !jumping2)
        {
            gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(3, gameObject.transform.position.y, 0), step);
        }

    }
    void Inputs()
    {

       if (platform.cop.x > OneLegValue)
        {
            LeanRight = true;
            leanLeft = false;
        }
       if(platform.cop.x <= OneLegValue)
        {
            LeanRight = false;
        }

        if (platform.cop.x < -OneLegValue)
        {
            LeanRight = false;
            leanLeft = true;
        }

        if (platform.cop.x >= -OneLegValue)
        {
            leanLeft = false;
        }

        if (platform.cop.force > JumpValue)
        {
            jumping2 = false;
        }
       

       if (platform.cop.force < JumpValue)
        {
            jumping2 = true; 
        }

    }
    public void ResetPlayer()
	{
        gameObject.transform.position = new Vector3(0, 3, 0);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour {

    public DynSTABLE platform; 
    public Stepping stepping;
    public WorldController theWorld; 

    public float xMax;

    private bool leanLeft, LeanRight, jumping2, jumping2L, jumping2R;
    public float translateSpeed;
    public float LeanSpeed;
    public float jumpSpeed;
    public float jumpFallSpeed; 
    public float jump2Speed; 
    public float switchJumpSpeed;
    public float OneLegValue;
    public float StepValue;
    public float OneLegJumpValue;
    public float JumpValue;
    public float JumpTwoValue;
    public bool basic, lean, oneLeg, step, jump, jump2, pit;
    public bool touchGround = true, flying = false;

	private const int MAXJUMP = 1;

    public Rigidbody rb;
    public NetworkInput input; 

    public float force;
    public float xpos;
    public float zpos;

    public float rotation; 
	// Use this for initialization
	void Start () {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        input = gameControllerObject.GetComponent<NetworkInput>();
       stepping = gameControllerObject.GetComponent<Stepping>();
       theWorld = gameControllerObject.GetComponent<WorldController>();
        rb = GetComponent<Rigidbody>();
        rotation = 10; 
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

        if (jump2)
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
        //gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        rotation+= 10;
        gameObject.transform.rotation = Quaternion.Euler((theWorld.speed * rotation) * 3.6f, 0, 0);
        Inputs();

        if (basic)
        {
            Lean();
            if (gameObject.transform.position.x >= xMax)
            {
                gameObject.transform.position = new Vector3(3, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.x <= -xMax)
            {
                gameObject.transform.position = new Vector3(-3, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        if (lean)
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
		else if (jump)
		{
			Jump();

		}

	}
    void Lean()
    {
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
    }
    void OneLeg()
    {
        if (LeanRight)
        {
            gameObject.transform.Translate((xMax - gameObject.transform.position.x) / translateSpeed, 0, 0);

        }
        else if (leanLeft)
        {
            gameObject.transform.Translate(-((xMax + gameObject.transform.position.x) / translateSpeed), 0, 0);
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
        if (platform.cop.x > StepValue)// && platform.cop.z < 0.1f && platform.cop.z > -0.1f)
        {
            stepping.DestroyRight();
        }

        if (platform.cop.z <= -StepValue)// && platform.cop.x < 0.1f && platform.cop.x > -0.1f)
        {
            stepping.DestroyUp();
        }

        if (platform.cop.x < -StepValue)// && platform.cop.z < 0.1f && platform.cop.z > -0.1f)
        {
            stepping.DestroyLeft();
        }
        
    }

    void Jump()
    {
        if (platform.cop.force < JumpValue && touchGround == true)
        {
			//rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
			rb.velocity = rb.velocity + new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
			//transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime, Space.World);
			touchGround = false;
        }
        
        /*
        if (gameObject.transform.position.x < 0 || gameObject.transform.position.x > 0)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, 0);
        }
        */
    }
    void Jump2()
    {
        float step = switchJumpSpeed * Time.deltaTime;
        //if (platform.cop.force < JumpTwoValue && touchGround == true)
        //{
        //    rb.AddForce(Vector3.up * jump2Speed, ForceMode.Impulse);
        //    touchGround = false;
        //}

        if (touchGround)
        {

            if ( input.nMomZ < -100) //platform.cop.x < -OneLegJumpValue &&
            {
                //gameObject.transform.Translate(-((xMax + gameObject.transform.position.x) / translateSpeed), 0, 0);
                //if (Physics.Raycast(gameObject.transform.position, Vector3.down, 3) && transform.position.y > 1)
                //{
                //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
                //}
                //else
                //{
                //    rb.AddForce(Vector3.down * 0.01f, ForceMode.Impulse);
                //}

                flying = false;
                jumping2L = true;
                jumping2R = false;
                if (platform.cop.force < JumpValue)
                {
                    jumping2L = false;
                    jumping2R = true;
                    flying = true;
                }
               

            }
            if (input.nMomZ > -100 && input.nMomZ < 100)//platform.cop.x > -OneLegJumpValue && platform.cop.x < OneLegJumpValue)
            {
     
               
            }
            if (input.nMomZ > 100) //platform.cop.x > OneLegJumpValue && 
            {
                //gameObject.transform.Translate((xMax - gameObject.transform.position.x) / translateSpeed, 0, 0);
                //if (Physics.Raycast(gameObject.transform.position, Vector3.down, 3) && transform.position.y > 1)
                //{
                //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
                //}
                //else
                //{
                //    rb.AddForce(Vector3.down * 0.01f, ForceMode.Impulse);
                //}
                flying = false;
                jumping2L = false;
                jumping2R = true;
                if (platform.cop.force < JumpValue)
                {
                    jumping2L = true;
                    jumping2R = false;
                    flying = true;
                }
            }

            //Movement from left to right
            if (jumping2L)
            {
                gameObject.transform.Translate(-((xMax + gameObject.transform.position.x) / translateSpeed), 0, 0);
            }
            else if (jumping2R)
            {
                gameObject.transform.Translate((xMax - gameObject.transform.position.x) / translateSpeed, 0, 0);
            }
            if (flying)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
                //gameObject.transform.Translate(0, ((xMax + gameObject.transform.position.y) / translateSpeed), 0);
            }

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

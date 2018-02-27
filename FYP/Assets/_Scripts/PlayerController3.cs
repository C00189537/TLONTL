﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour {

    public DynSTABLE platform; 
    public Stepping stepping; 
    
    public float xVal;

    private bool right, left, leanLeft, LeanRight;
    public float translateSpeed;
    public float LeanSpeed;
    public float jumpSpeed;
    public float LeanValue;
    public float OneLegValue;
    public float StepValue;
    public float JumpValue;
    public float JumpTwoValue;
    public bool basic, lean, oneLeg, step, jump, jump2;

    public Rigidbody rb; 

    public float force;
    public float xpos;
    public float zpos; 
	// Use this for initialization
	void Start () {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
       stepping = gameControllerObject.GetComponent<Stepping>();

        rb = GetComponent<Rigidbody>();
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
        else if(jump)
        {
            Jump();
        }
      //  else if (jump2)
        //{
        //    Jump2();
       //i }
       


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
        
        if (jump)
        {
            gameObject.transform.Translate(0, jumpSpeed, 0);
        }
           
    }
    void Jump2()
    {
        if (jump)
        {
            gameObject.transform.Translate(0, jumpSpeed, 0);
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
        }

    }
    void Inputs()
    {
       if (platform.cop.x < LeanValue)
        {
            left = true;
            right = false; 
        }

       if (platform.cop.x > LeanValue)
        {
            left = false;
            right = true; 
        }

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
            jump = false;
        }
       

       if (platform.cop.force < JumpValue)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour {

    public DynSTABLE platform; 
    public Stepping stepping;
    public WorldController theWorld; 

    public float xMax;
    public int fallOff; 

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
    public float pressure;

    public Rigidbody rb;
    public NetworkInput input;
    public float tilt;

    public float force;
    public float xpos;
    public float zpos;

    public float rotation;

    float FallingCooldown = 2;
    float TimeStampFallinig;
    float JumpingCooldown = 2;
    float TimeStampJumping; 

   
    // Use this for initialization
    void Start () {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        input = gameControllerObject.GetComponent<NetworkInput>();
       stepping = gameControllerObject.GetComponent<Stepping>();
       theWorld = gameControllerObject.GetComponent<WorldController>();
        rb = GetComponent<Rigidbody>();
        rotation = 10;
        TimeStampFallinig = Time.time + FallingCooldown;
        TimeStampJumping = Time.time + JumpingCooldown;
    }


    private void FixedUpdate()
    {
        if (platform.cop.force >= JumpValue && touchGround == false)
        {
			rb.AddForce(Vector3.down * jumpFallSpeed, ForceMode.Impulse);
		}

        if (platform.cop.force >= 300)
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

        if (gameObject.transform.position.y < -0.2 && TimeStampFallinig <= Time.time)
        {
            AudioManager.GetInstance().audiosource.PlayOneShot(AudioManager.GetInstance().falling, 0.5f);

            TimeStampFallinig = Time.time + FallingCooldown;
            ScoreController.GetInstance().SubtractScore(-5);
            fallOff++; 

        }

        if (gameObject.transform.position.y > 1.1f && TimeStampJumping <= Time.time)
        {
            AudioManager.GetInstance().audiosource.PlayOneShot(AudioManager.GetInstance().Jumping, 0.5f);
            TimeStampJumping = Time.time + JumpingCooldown;
        }


        force = platform.cop.force;
        xpos = platform.cop.x;
        zpos = platform.cop.z;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0 );
        Tilted();
      
        Inputs();

        if (basic)
        {
            Lean();
            if (gameObject.transform.position.x >= xMax)
            {
                gameObject.transform.position = new Vector3(xMax, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.x <= -xMax)
            {
                gameObject.transform.position = new Vector3(-xMax, gameObject.transform.position.y, gameObject.transform.position.z);
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
			rb.velocity = rb.velocity + new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
			touchGround = false;
        }
        

    }
    void Jump2()
    {
        float step = switchJumpSpeed * Time.deltaTime;

        if (touchGround)
        {

            if ( input.nMomZ < -pressure) 
            {

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
            if (input.nMomZ > -pressure && input.nMomZ < pressure)
            {
     
               
            }
            if (input.nMomZ > pressure)
            {

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
    private void Tilted()
    {
        if (input.nMomZ < -pressure)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, tilt * Mathf.Deg2Rad, 1);
        }
        else if (input.nMomZ > pressure)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, -tilt * Mathf.Deg2Rad, 1);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }
}

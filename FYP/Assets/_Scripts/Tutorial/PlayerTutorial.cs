using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour
{

    public DynSTABLE platform;
    public StepTutorial stepping;
    public NetworkTutorial input;
    public float OneLegValue;
    public float StepValue;
    public float xVal;
    private bool leanLeft, LeanRight, jumping2, jumping2L, jumping2R;
    public float translateSpeed;
    public float LeanSpeed;
    public Rigidbody rb;
    public bool touchGround = true, flying = false;
    public bool lean, oneLeg, step, jump, jump2;
    public float jumpFallSpeed;
    public float JumpValue;
    public float jumpSpeed;
    public float xMax;
    bool leftDone = false, rightDOne = false;

    //UI elements
    public Text Scoretext;//leaning
    public Image greenLeft;
    public Image redLeft;
    public Image redRight;
    public Image greenRight;
    float i = 0;
    float j = 0;

    public Text steppingText;
    public Text jumpingText;
    public Text jumpingTwoText; 

    public float switchJumpSpeed;

    private const int MAXJUMP = 1;
    public float pressure;

    public int Leanscore; //for leaning
    public int steppingScore;
    public int JumpScore;
    public int JumpTwoScore;

    // Use this for initialization
    void Start()
    {
        stepping = GetComponent<StepTutorial>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<NetworkTutorial>();

        Leanscore = 0;

    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.y > 1.3f && touchGround == true)
        {
            rb.AddForce(Vector3.down * jumpFallSpeed, ForceMode.Acceleration);
        }

        if (platform.cop.force >= JumpValue)
        {
            touchGround = true;
        }

        if (jump2)
        {
            Jump2();

        }
    }
    // Update is called once per frame
    void Update()
    {
        Inputs();

        if (lean)
        {
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, -8);
        }

        else if (oneLeg)
        {
            if (LeanRight)
            {
                gameObject.transform.Translate((xVal - gameObject.transform.position.x) / translateSpeed, 0, 0);
                if (i < 186)
                {
                    i += 0.2f;
                    greenRight.rectTransform.sizeDelta = new Vector2(100, i);
                }

            }
            else if (leanLeft)
            {
                gameObject.transform.Translate(-((xVal + gameObject.transform.position.x) / translateSpeed), 0, 0);
                if (j < 186)
                {
                    j += 0.2f;
                    greenLeft.rectTransform.sizeDelta = new Vector2(100, j);
                }


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

        else if (step)
        {
            Step();
        }
        else if (jump)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (platform.cop.force < JumpValue && touchGround == true)
        {
            rb.velocity = rb.velocity + new Vector3(0, jumpSpeed, rb.velocity.z);
            touchGround = false;
        }

    }

    void Jump2()
    {
        float step = switchJumpSpeed * Time.deltaTime;

        if (touchGround)
        {

            if (input.nMomZ < -pressure)
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

    void Step()
    {
        
        if (platform.cop.x > StepValue && platform.cop.z >= -0.15f && platform.cop.z <= 0.15f)
        {
            stepping.DestroyRight();
           
        }

        if (platform.cop.z <= -StepValue && platform.cop.x >= -0.15f && platform.cop.x <= 0.15f)
        {
            stepping.DestroyUp();
           
        }

        if (platform.cop.x < -StepValue && platform.cop.z >= -0.15f && platform.cop.z <= 0.15f)
        {
            stepping.DestroyLeft();
            
        }

    }

    public void Inputs()
    {

        if (platform.cop.x > OneLegValue)
        {
            LeanRight = true;
            leanLeft = false;
        }
        if (platform.cop.x <= OneLegValue)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Leanscore++;
            Destroy(other.gameObject);
            Scoretext.text = "Score: " + Leanscore.ToString();
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Lean")
        {
            Scoretext.enabled = true;
            redLeft.enabled = false;
            redRight.enabled = false;
            greenLeft.enabled = false;
            greenRight.enabled = false;
            steppingText.enabled = false;
            jumpingText.enabled = false;
            jumpingTwoText.enabled = false; 
            lean = true;
            oneLeg = false;
            step = false;
            jump = false;
            jump2 = false;
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            Scoretext.enabled = false;
            redLeft.enabled = true;
            redRight.enabled = true;
            greenLeft.enabled = true;
            greenRight.enabled = true;
            steppingText.enabled = false;
            jumpingText.enabled = false;
            jumpingTwoText.enabled = false;
            lean = false;
            oneLeg = true;
            step = false;
            jump = false;
            jump2 = false;
        }
        else if (other.gameObject.tag == "Step")
        {
            Scoretext.enabled = false;
            redLeft.enabled = false;
            redRight.enabled = false;
            greenLeft.enabled = false;
            greenRight.enabled = false;
            steppingText.enabled = true;
            jumpingText.enabled = false;
            jumpingTwoText.enabled = false;
            lean = false;
            oneLeg = false;
            step = true;
            jump = false;
            jump2 = false;

            if (stepping.getSteps() <= 0)
            {
                stepping.GenerateButtons(3);
            }
            steppingText.text = steppingScore.ToString() + "/10"; 
        }
        else if (other.gameObject.tag == "Jump")
        {
            Scoretext.enabled = false;
            redLeft.enabled = false;
            redRight.enabled = false;
            greenLeft.enabled = false;
            greenRight.enabled = false;
            steppingText.enabled = false;
            jumpingText.enabled = true;
            jumpingTwoText.enabled = false;
            lean = false;
            oneLeg = false;
            step = false;
            jump = true;
            jump2 = false;
            jumpingText.text = JumpScore.ToString() + "/10"; 

        }
        else if (other.gameObject.tag == "Jump2")
        {
            Scoretext.enabled = false;
            redLeft.enabled = false;
            redRight.enabled = false;
            greenLeft.enabled = false;
            greenRight.enabled = false;
            steppingText.enabled = false;
            jumpingText.enabled = false;
            jumpingTwoText.enabled = true;
            lean = false;
            oneLeg = false;
            step = false;
            jump = false;
            jump2 = true;
            jumpingTwoText.text = JumpTwoScore.ToString() + "/10";
        }

    }

    public void ResetPlayer()
    {
        gameObject.transform.position = new Vector3(0, 3, 0);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

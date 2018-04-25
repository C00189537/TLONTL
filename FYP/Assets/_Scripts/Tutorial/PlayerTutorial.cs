using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{

    public DynSTABLE platform;
    public StepTutorial stepping; 
    public float OneLegValue;
    public float StepValue; 
    public float xVal;
    private bool leanLeft, LeanRight;
    public float translateSpeed;
    public float LeanSpeed;

    public bool lean, oneLeg, step, jump, jump2;
    // Use this for initialization
    void Start()
    {
        stepping = GetComponent<StepTutorial>(); 
    }

    // Update is called once per frame
    void Update()
    {

        Inputs();
        if (lean)
        {
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
        }
        else if (oneLeg)
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
        else if (step)
        {
            Step();
        }
        else if (jump)
        {

        }
        else if (jump2)
        {

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
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Lean")
        {
            lean = true;
            oneLeg = false;
            step = false;
            jump = false;
            jump2 = false;
        }
        else if (other.gameObject.tag == "OneLeg")
        {
            lean = false;
            oneLeg = true;
            step = false;
            jump = false;
            jump2 = false;
        }
        else if (other.gameObject.tag == "Step")
        {
            lean = false;
            oneLeg = false;
            step = true;
            jump = false;
            jump2 = false;
            if (stepping.getSteps() <= 0)
            {
                stepping.GenerateButtons(3);
            }
            
        }
        else if (other.gameObject.tag == "Jump")
        {
            lean = false;
            oneLeg = false;
            step = false;
            jump = true;
            jump2 = false;
        }
        else if (other.gameObject.tag == "Jump2")
        {
            lean = false;
            oneLeg = false;
            step = false;
            jump = false;
            jump2 = true;
        }

    }
}

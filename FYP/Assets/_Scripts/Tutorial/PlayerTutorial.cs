using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour {

    public DynSTABLE platform;

    public float xVal;
    private bool leanLeft, LeanRight;
    public float translateSpeed;
    public float LeanSpeed;

    public bool lean, oneLeg, step, jump, jump2; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lean)
        {
            gameObject.transform.Translate(platform.cop.x * LeanSpeed, 0, 0);
        } else if (oneLeg)
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
        } else if (jump)
        {

        } else if (jump2)
        {

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

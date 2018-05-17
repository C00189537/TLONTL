using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWorld : MonoBehaviour
{
    public static bool LeanPlatform, OneLegPlatform, StepPlatform, JumpPlatform, JumpTwoPlatform;

    public LeanTutorial leanTut;
    public OneLegTutorial oneLegTut;
    public SteppingTutorial stepTut;
    public JumpTutorial jumpTut;
    public JumpTwoTutorial jumpTwoTut; 

    public GameObject leaning;
    public GameObject oneLeg;
    public GameObject stepping;
    public GameObject jumping;
    public GameObject jumpingTwo;

    public void Start()
    {
        LeanPlatform = false;
        OneLegPlatform = true;
        StepPlatform = false;
        JumpPlatform = false;
        JumpTwoPlatform = false;
         
        leanTut = leaning.GetComponent<LeanTutorial>();
        oneLegTut = oneLeg.GetComponent<OneLegTutorial>();
        stepTut = stepping.GetComponent<SteppingTutorial>();
        jumpTut = jumping.GetComponent<JumpTutorial>();
        jumpTwoTut = jumpingTwo.GetComponent<JumpTwoTutorial>(); 
        
    }

    public void Update()
    {
        if (LeanPlatform)
        {
            leanTut.enabled = true;
            oneLegTut.enabled = false;
            stepTut.enabled = false;
            jumpTut.enabled = false;
            jumpTwoTut.enabled = false; 

            leaning.transform.position = new Vector3(0, 0, 0);
            oneLeg.transform.position = new Vector3(40, 0, 0);
            stepping.transform.position = new Vector3(80, 0, 0);
            jumping.transform.position = new Vector3(120, 0, 0);
            jumpingTwo.transform.position = new Vector3(160, 0, 0);
        }
        else if (OneLegPlatform)
        {
            leanTut.enabled = false;
            oneLegTut.enabled = true;
            stepTut.enabled = false;
            jumpTut.enabled = false;
            jumpTwoTut.enabled = false;

            leaning.transform.position = new Vector3(-40, 0, 0);
            oneLeg.transform.position = new Vector3(0, 0, 0);
            stepping.transform.position = new Vector3(40, 0, 0);
            jumping.transform.position = new Vector3(80, 0, 0);
            jumpingTwo.transform.position = new Vector3(120, 0, 0);
        }
        else if (StepPlatform)
        {
            leanTut.enabled = false;
            oneLegTut.enabled = false;
            stepTut.enabled = true;
            jumpTut.enabled = false;
            jumpTwoTut.enabled = false;

            leaning.transform.position = new Vector3(-80, 0, 0);
            oneLeg.transform.position = new Vector3(-40, 0, 0);
            stepping.transform.position = new Vector3(0, 0, 0);
            jumping.transform.position = new Vector3(40, 0, 0);
            jumpingTwo.transform.position = new Vector3(80, 0, 0);
        }
        else if (JumpPlatform)
        {
            leanTut.enabled = false;
            oneLegTut.enabled = false;
            stepTut.enabled = false;
            jumpTut.enabled = true;
            jumpTwoTut.enabled = false;

            leaning.transform.position = new Vector3(-120, 0, 0);
            oneLeg.transform.position = new Vector3(-80, 0, 0);
            stepping.transform.position = new Vector3(-40, 0, 0);
            jumping.transform.position = new Vector3(0, 0, 0);
            jumpingTwo.transform.position = new Vector3(40, 0, 0);
        }
        else if (JumpTwoPlatform)
        {
            leanTut.enabled = false;
            oneLegTut.enabled = false;
            stepTut.enabled = false;
            jumpTut.enabled = false;
            jumpTwoTut.enabled = true;

            leaning.transform.position = new Vector3(-160, 0, 0);
            oneLeg.transform.position = new Vector3(-120, 0, 0);
            stepping.transform.position = new Vector3(-80, 0, 0);
            jumping.transform.position = new Vector3(-40, 0, 0);
            jumpingTwo.transform.position = new Vector3(0, 0, 0);
        }
    }

  
}

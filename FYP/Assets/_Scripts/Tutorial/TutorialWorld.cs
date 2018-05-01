using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWorld : MonoBehaviour
{

    public PlayerTutorial player;

    public int waitTimer = 3;
    public int instructionsTimer = 10;
    public Image theInstructions; 
    public Image Lean;
    public Image OneLeg;
    public Image Step;
    public Image Jump;
    public Image Jump2;

    bool showInst = false;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("PlayerTut").GetComponent<PlayerTutorial>();
        Lean.enabled = false;
        OneLeg.enabled = false;
        Step.enabled = false;
        Jump.enabled = false;
        Jump2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionsTimer <= 0)
        {
            showInst = false;
            StopCoroutine(CountDown());
         
            Lean.enabled = false;
            OneLeg.enabled = false;
            Step.enabled = false;
            Jump.enabled = false;
            Jump2.enabled = false;
        }

        if (player.lean)
        {
           
            while (instructionsTimer >= 0)
            {
                StartCoroutine(CountDown());
              

            }

        }
        if (player.oneLeg)
        {
            instructionsTimer = 3;
            // StartCoroutine(CountDown());
            while (instructionsTimer > 0)
            {
                Lean.enabled = false;
                OneLeg.enabled = true;
                Step.enabled = false;
                Jump.enabled = false;
                Jump2.enabled = false;
            }

        }
        if (player.step)
        {
            instructionsTimer = 3;
            /// StartCoroutine(CountDown());
            while (instructionsTimer > 0)
            {
                Lean.enabled = false;
                OneLeg.enabled = false;
                Step.enabled = true;
                Jump.enabled = false;
                Jump2.enabled = false;
            }

        }
        if (player.jump)
        {
            instructionsTimer = 3;
            //  StartCoroutine(CountDown());
            while (instructionsTimer > 0)
            {
                Lean.enabled = false;
                OneLeg.enabled = false;
                Step.enabled = false;
                Jump.enabled = true;
                Jump2.enabled = false;
            }

        }
        if (player.jump2)
        {
            instructionsTimer = 3;
            //  StartCoroutine(CountDown());
            while (instructionsTimer > 0)
            {
                Lean.enabled = false;
                OneLeg.enabled = false;
                Step.enabled = false;
                Jump.enabled = false;
                Jump2.enabled = true;
            }

        }
    }

    public IEnumerator CountDown()
    {
        // while (instructionsTimer > 0)
        // {
        Debug.Log("it workssss");
        instructionsTimer--;
        showInst = true; 
        yield return new WaitForSeconds(1);
        //}


    }

    public IEnumerator Wait()
    {
        waitTimer--;
        yield return new WaitForSeconds(1);


    }
}

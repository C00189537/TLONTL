﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StepTutorial : MonoBehaviour
{


    public GameObject LeftButton;
    public GameObject UpButton;
    public GameObject RightButton;
    public GameObject Selector;

    GameObject[] greenUp;
    GameObject[] greenLeft;
    GameObject[] greenRight;

    GameObject mySelector = null;
    public Canvas Canvas;


    //For positioning
    public int resetYValue = -100;
    public int Xvalue = 0;
    public int Yvalue = 0;
    public int Zvalue = 0;
    public int ButtonSpacing = -65;

    int previousArrow = 4;

    System.Random randy = new System.Random();

    PlayerTutorial playerController;

    public int DestroyedSteps; 

    //Creating a queue to load the buttons in. This will help with destroying them in the order they are displayed
    public Queue<GameObject> Steps = new Queue<GameObject>();

    private void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("PlayerTut");
        playerController = playerControllerObject.GetComponent<PlayerTutorial>();
        DestroyedSteps = 0; 
    }


    void Update()
    {
        
        //Should destroy all the objects in the queue when player leaves stepping track
        if (Steps.Count <= 0)
        {
            greenUp = GameObject.FindGameObjectsWithTag("Up");
            greenLeft = GameObject.FindGameObjectsWithTag("Left");
            greenRight = GameObject.FindGameObjectsWithTag("Right");

            foreach (GameObject up in greenUp)
            {
                Destroy(up);
            }

            foreach (GameObject left in greenLeft)
            {
                Destroy(left);
            }

            foreach (GameObject right in greenRight)
            {
                Destroy(right);
            }
        }

        if (playerController.step == false)
        {

            foreach (GameObject step in Steps)
            {
                Destroy(step);
            }
            Steps.Clear();
            ResetXPosition();
        }

        if (Steps.Count > 0)
        {
            var firstArrow = Steps.Peek();
            firstArrow.GetComponent<RawImage>().color = Color.white;

            if (mySelector == null)
            {
                mySelector = Instantiate(Selector);
            }
        }
        if (!(mySelector == null))
        {
            SelectArrow();
        }
    }

    // parameter should eventually be received from world controller
    public void GenerateButtons(int numberOfButtons)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            int rand = randy.Next(0, 3);

            while (previousArrow == rand)
            {
                rand = randy.Next(0, 3);
            }

            switch (rand)
            {
                case (0):
                    var Left = Instantiate(LeftButton);
                    previousArrow = 0;
                    SetPosition(Left);
                    break;
                case (1):
                    var Up = Instantiate(UpButton);
                    previousArrow = 1;
                    SetPosition(Up);
                    break;
                case (2):
                    var Right = Instantiate(RightButton);
                    previousArrow = 2;
                    SetPosition(Right);
                    break;
                default:
                    break;
            }
        }
    }

    void SetPosition(GameObject arrow)
    {
        arrow.transform.SetParent(Canvas.transform, false);
        arrow.transform.localPosition = new Vector3(Xvalue, Yvalue, Zvalue);
        Yvalue -= ButtonSpacing;
        Steps.Enqueue(arrow);
        ResetXPosition();
    }


    public void DestroyRight()
    {
        if (Steps.Count > 0)
        {
            GameObject firstArrow = Steps.Peek();

            if (firstArrow.CompareTag("Right"))
            {
                playerController.steppingScore++;
                Steps.Dequeue();
                firstArrow.GetComponent<RawImage>().color = Color.green;
                DestroyedSteps++; 
            }
            ResetXPosition();
        }

    }


    public void DestroyUp()
    {
        if (Steps.Count > 0)
        {
            GameObject firstArrow = Steps.Peek();

            if (firstArrow.CompareTag("Up"))
            {
                playerController.steppingScore++;
                Steps.Dequeue();
                firstArrow.GetComponent<RawImage>().color = Color.green;
                DestroyedSteps++;
            }
            ResetXPosition();
        }

    }

    public void DestroyLeft()
    {
        if (Steps.Count > 0)
        {
            GameObject firstArrow = Steps.Peek();

            if (firstArrow.CompareTag("Left"))
            {
                playerController.steppingScore++;
                Steps.Dequeue();
                firstArrow.GetComponent<RawImage>().color = Color.green;
                DestroyedSteps++;
            }
            ResetXPosition();
        }

    }

    public void ResetXPosition()
    {
        if (Steps.Count == 0)
        {
            Yvalue = resetYValue;
        }
    }

    public int getSteps()
    {
        return Steps.Count;
    }

    public void SelectArrow()
    {
        if (Steps.Count > 0)
        {
            GameObject firstArrow = Steps.Peek();
            mySelector.transform.SetParent(Canvas.transform, false);
            mySelector.transform.position = firstArrow.transform.position + new Vector3(0, 5f, 0);
        }
        else
        {
            Destroy(mySelector);
        }
    }
}

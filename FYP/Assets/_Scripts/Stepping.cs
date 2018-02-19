﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class Stepping : MonoBehaviour
{
    public GameObject LeftButton;
    public GameObject UpButton;
    public GameObject RightButton;
    public GameObject DownButton;
    public Canvas Canvas;

   
    //For positioning
    public int resetXValue = -134;
    public int Xvalue = -134;
    public int Yvalue = -102;
    public int Zvalue = 0;
    public int ButtonSpacing = -90;

    public int buttoneCount;
    
    System.Random randy = new System.Random();

    PlayerController2 playerController;

    //Creating a queue to load the buttons in. This will help with destroying them in the order they are displayed
    public Queue <GameObject>Steps = new Queue<GameObject>();

    private void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        playerController = playerControllerObject.GetComponent<PlayerController2>();
    }

   
    void Update()
    {
        //Should destroy all the objects in the queue when player leaves stepping track
        if (playerController.step == false)
        {
            
            foreach ( GameObject step in Steps){
               Destroy(step);
            }
            Steps.Clear();
            ResetXPosition();
        }
    }

    // parameter should eventually be received from world controller
    public void GenerateButtons(int numberOfButtons)
    {
        buttoneCount = numberOfButtons + 1;
        for (int i = 0; i < numberOfButtons; i++)
        {
            int rand = randy.Next(0, 3);
            
            switch (rand)
            {
                case (0):
                    var Left = Instantiate(LeftButton);
                    SetPosition(Left);
                    break;
                case (1):
                    var Up = Instantiate(UpButton);
                    SetPosition(Up);
                    break;
                case (2):
                    var Right = Instantiate(RightButton);
                    SetPosition(Right);
                    break;
                case (3):
                    var Down = Instantiate(DownButton);
                    SetPosition(Down);
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
            Xvalue -= ButtonSpacing;
            Steps.Enqueue(arrow);
           ResetXPosition();
    }

     
    public void DestroyRight()
    {
        if (Steps.Count > 0){
             GameObject firstArrow = Steps.Peek();

              if (firstArrow.CompareTag("Right"))
              {
                  Steps.Dequeue();
                  Destroy(firstArrow);
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
                Steps.Dequeue();
                Destroy(firstArrow);
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
                Steps.Dequeue();
                Destroy(firstArrow);
            }
            ResetXPosition();
        }
        
    }


    public void DestroyDown()
    {
        if (Steps.Count > 0)
        {
            GameObject firstArrow = Steps.Peek();

            if (firstArrow.CompareTag("Down"))
            {
                Steps.Dequeue();
                Destroy(firstArrow);
            }
            ResetXPosition();
        }
        
    }

    public void ResetXPosition()
    {
        if (Steps.Count == 0)
        {
            Xvalue = resetXValue;
        }
    }

    public int getSteps()
    {
        return Steps.Count - 1;
    }
}

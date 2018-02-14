using System;
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
    
    System.Random randy = new System.Random();

    //Creating a queue to load the buttons in. This will help with destroying them in the order they are displayed
    public static Queue <GameObject>Steps = new Queue<GameObject>();
    

   
    void Update()
    {
        /* //This was for testing purposes 
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenerateButtons(2);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateButtons(3);
        }
        */

        if (Steps.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                DestroyRight();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                DestroyUp();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                DestroyLeft();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                DestroyDown();
            }
        }
    }

    // parameter should eventually be received from world controller
    public void GenerateButtons(int numberOfButtons)
    {
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
            Debug.Log("Steps count: " + Steps.Count);
        ResetXPosition();
    }

     
    public void DestroyRight()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Right"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
        ResetXPosition();
    }
    

    public void DestroyUp()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Up"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
        ResetXPosition();
    }

    public void DestroyLeft()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Left"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
        ResetXPosition();
    }


    public void DestroyDown()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Down"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
        ResetXPosition();
    }

    public void ResetXPosition()
    {
        if (Steps.Count == 0)
        {
            Xvalue = resetXValue;
        }
    }
}

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
    public int Xvalue = -134;
    public int Yvalue = -102;
    public int Zvalue = 0;
    public int ButtonSpacing = -90; 
    
    System.Random randy = new System.Random();

    //Creating a queue to load the buttons in. This will help with destroying them in the order they are displayed
    public static Queue <GameObject>Steps = new Queue<GameObject>();
    
    // parameter is received from world controller
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenerateButtons(2);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateButtons(3);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyRight();
        }
        if (Input.GetKey(KeyCode.W))
        {
            DestroyUp();
        }

        if (Input.GetKey(KeyCode.A))
        {
            DestroyLeft();
        }

        if (Input.GetKey(KeyCode.S))
        {
            DestroyDown();
        }
        
    }

    void GenerateButtons(int numberOfButtons)
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
    }

     
    public void DestroyRight()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Right"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        } 

    }
    

    public void DestroyUp()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Up"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
    }

    public void DestroyLeft()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Left"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
    }


    public void DestroyDown()
    {
        var firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Down"))
        {
            Steps.Dequeue();
            Destroy(firstArrow);
        }
    }

}

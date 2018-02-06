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
    public GameObject Canvas;

    public int Xvalue = -137;
    public int Yvalue = -102;
    public int Zvalue = 0;
    public int ButtonSpacing = -90; 

    Vector3 position;
    public int NumberOfButtons = 3;
    System.Random randy = new System.Random(); 

    // Use this for initialization
    void Start()
    {
        position = new Vector3(Xvalue, Yvalue, Zvalue);
    }


    // Update is called once per frame
    void Update()
    {
        Instantiate(LeftButton, position, transform.rotation);
        LeftButton.transform.SetParent(Canvas.transform, false);
        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenerateButtons(2);
            Debug.Log("Button O is being registered");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateButtons(3);
        }
        */
    }

    void GenerateButtons(int numberOfButtons)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            int rand = randy.Next(0, 3);

            switch (rand)
            {
                case (0):
                    Instantiate(LeftButton, position, transform.rotation);
                    LeftButton.transform.SetParent(Canvas.transform, false);
                    break;
                case (1):
                    Instantiate(UpButton, position, transform.rotation);
                    UpButton.transform.SetParent(Canvas.transform, false);
                    break;
                case (2):
                    Instantiate(RightButton, position, transform.rotation);
                    RightButton.transform.SetParent(Canvas.transform, false);
                    break;
                case (3):
                    Instantiate(DownButton, position, transform.rotation);
                    DownButton.transform.SetParent(Canvas.transform, false);
                    break;
                default:
                    break;
            }

            Xvalue += ButtonSpacing;
        }
    }
}

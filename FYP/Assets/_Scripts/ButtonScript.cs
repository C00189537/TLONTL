using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {


//    public Stepping stepping;
   // GameObject firstArrow;

    // Use this for initialization
    void Start () {
    //    stepping = GetComponent<Stepping>();
	}
	
	// Update is called once per frame
	void Update () {
          if (Input.GetKey(KeyCode.D))
          {
          //  CheckFirstArrowInQueue();
        }

        /*if (Input.GetKey(KeyCode.W))
        {
            DestroyUp();
        }

        if (Input.GetKey(KeyCode.A))
        {
            DestroyLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            DestroyDown();
        }*/

        
    }

    void CheckStepsQueue()
    {
        if (Stepping.Steps.Count <= 0)
        {
            throw new System.Exception("Queue is empty");
        }
    }

    public void CheckFirstArrowInQueue()
    {

        CheckStepsQueue();
        GameObject firstArrow = Stepping.Steps.Peek();

        if (firstArrow.CompareTag("Right"))
        {
            
            Stepping.Steps.Dequeue();
            Destroy(firstArrow);
        }

           

    }
    /*

   public void DestroyRight()
    {
       
          var arrow = GameObject.FindWithTag("Right");
         
          if (Stepping.Steps.Count > 0)
            firstArrow = Stepping.Steps.Peek();
        
            if (firstArrow!= null && firstArrow.(arrow) )
            {
                Debug.Log("It works yo");
                Stepping.Steps.Dequeue();
                Destroy(firstArrow);
            }   
            
    }

   public void DestroyUp()
    {
            var arrow = GameObject.FindWithTag("Up");
            //stepping.Steps.Dequeue();
            Destroy(arrow);
    }

  public  void DestroyLeft()
    {
            var arrow = GameObject.FindWithTag("Left");
           /// stepping.Steps.Dequeue();
            Destroy(arrow);
    }


   public void DestroyDown()
    {
            var arrow = GameObject.FindWithTag("Down");
          //  stepping.Steps.Dequeue();
            Destroy(arrow);
    }
    */
}

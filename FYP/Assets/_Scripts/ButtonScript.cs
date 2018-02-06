using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        DestroyLeft();
        DestroyUp();
        DestroyRight();
        DestroyDown();
    }


   public void DestroyLeft()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            Destroy(GameObject.FindWithTag("Left"));
        }
    }

   public void DestroyUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Destroy(GameObject.FindWithTag("Up"));
        }
    }

  public  void DestroyRight()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Destroy(GameObject.FindWithTag("Right"));
        }
    }


   public void DestroyDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Destroy(GameObject.FindWithTag("Down"));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InstructionsTimer : MonoBehaviour {

    public int timer = 10;
    //public Image instructions; 
     
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour {

    public DynSTABLE platform; 
    public float jumpValue;
    public float jumpSpeed;
    public float force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        force = platform.cop.force;
        if ( platform.cop.force < jumpValue)
        {
            gameObject.transform.Translate(0, jumpSpeed, 0);
        }
		
	}
}

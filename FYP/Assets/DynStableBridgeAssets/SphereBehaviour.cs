using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour {


    public DynSTABLE platform;
    public Vector3 ballPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 scale = transform.localScale;
        //scale.y = 6 * platform.cop.force;

        Vector3 position = transform.localPosition;
      //  position.y = 3 * platform.cop.force;

        position.x = 10 * platform.cop.x;
        // position.z = -10 * platform.cop.z;

        //transform.localScale = scale;
        ballPosition = new Vector3(position.x, 1, 0);

    }
}

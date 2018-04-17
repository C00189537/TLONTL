using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotato : MonoBehaviour
{

    public Vector3 rotationVector;

    float originalY;
    float FloatStrength = 0.4f;

    // Use this for initialization
    void Start()
    {
        Vector3 initialY = new Vector3(0, 90f, 0);
        transform.Rotate(initialY);

        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, originalY +  0.3f+ ((float)Mathf.Sin(Time.time * 3) * FloatStrength), transform.position.z);
    }
}

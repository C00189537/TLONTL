using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotato : MonoBehaviour
{

    public Vector3 rotationVector;

    float originalY;
    float FloatStrength = 0.3f;

    // Use this for initialization
    void Start()
    {
        Vector3 initialY = new Vector3(0, 90, 0);
        transform.Rotate(initialY);

        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, originalY + 0.5f + ((float)Mathf.Sin(Time.time) * FloatStrength), transform.position.z);
    }
}

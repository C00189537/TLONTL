using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Indicator : MonoBehaviour {

    public SpriteRenderer image;
    public GameObject player; 
    float alpha;
    Color color; 
    float distance; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        image = GetComponentInChildren<SpriteRenderer>();
        color = GetComponentInChildren<SpriteRenderer>().color;
    }

    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        alpha = distance / 200.0f;

        color.a = alpha;
        image.color = color;

        if (distance < 45)
        {
            image.enabled = false; 
        }
    }


}

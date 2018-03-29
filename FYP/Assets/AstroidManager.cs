using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidManager : MonoBehaviour {

    public GameObject[] astroids = new GameObject[7];

    System.Random rand = new System.Random();

    public float interval;
    float time;
    public float spawnPoint = 140; 
    int number;
    int side;
    int position; 
    public int minLeftSide = -8;
    public int maxLeftSide = -5;
    public int minRightSide = 5; 
    public int maxRightSide = 8; 

    int height;
    public int minHeight = 7;
    public int maxHeight = 10; 

    // Use this for initialization
    void Start () {
        time = interval; 
	}
	
	// Update is called once per frame
	void Update () {

        interval -= Time.deltaTime; 
        
        if (interval <= 0)
        {
            

            position = rand.Next(0, 2); 

            if (position == 0)
            {
                number = rand.Next(0, astroids.Length);
                position = rand.Next(minLeftSide, maxLeftSide + 1);
                height = rand.Next(minHeight, maxHeight + 1);
                Instantiate(astroids[number], new Vector3(position, height, spawnPoint), transform.rotation);
                interval = time;
            }

            if (position == 1)
            {
                number = rand.Next(0, astroids.Length);
                position = rand.Next(minRightSide, maxRightSide + 1);
                height = rand.Next(minHeight, maxHeight + 1);
                Instantiate(astroids[number], new Vector3(position, height, spawnPoint), transform.rotation);
                interval = time;
            }
          
        }
	}
}

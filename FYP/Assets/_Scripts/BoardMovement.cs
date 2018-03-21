using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour {


    public NetworkOutput board;

    public WorldController world;
    public NetworkInput input;
    public float moveTime; 
    System.Random rand = new System.Random();
    // Use this for initialization

    void Start () {
        world = GetComponent<WorldController>();
        input = GetComponent<NetworkInput>();
        board = GetComponent<NetworkOutput>();
        board.SetPos(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //move the track
        /*
        int board = rand.Next(0, 10);
        if (board % 3 == 0)
        {
           // BoardMovements();
        }*/

       
    }

    public void BoardMovements()
    {
        int variable = world.difficulty;    

        if (input.NManual == 0)
        {
            variable = world.difficulty;
        }
        if (input.NManual == 1)
        {
            variable = (int)input.nBoardMovements;
        }

        switch (variable)
        {
            case 1:
                break;
            case 2:
                moveTime = 0.7f;
                StartCoroutine(Movement(moveTime));
                break;
            case 3:
                moveTime = 0.5f;
                StartCoroutine(Movement(moveTime));
                break;
            case 4:
                moveTime = 0.3f;
                StartCoroutine(Movement(moveTime));
                break;
            case 5:
                moveTime = 0.1f;
                StartCoroutine(Movement(moveTime));
                break;
            default:
                break;
        }
         
    }

    IEnumerator Movement(float seconds)
    {
        float i = GetRandomFloat(0.03f, 0.06f);

        float k = 0.0f;

        if ( i > 0)
        {
            while (!(k >= i))
            {
                k += 0.01f;
                Debug.Log(k);
                board.SetPos(k, k);
                yield return new WaitForSeconds(seconds);
            }
        }

        if (i < 0)
        {
            while (!(k <= i))
            {
                k -= 0.01f;
                board.SetPos(k, k);
                yield return new WaitForSeconds(seconds);
            }
        }
        
        //yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
        
    }

    public void ResetBoard()
    {
        board.SetPos(0, 0); 
    }

    /*
    IEnumerator MovementTwo()
    {
        float i = GetRandomFloat(0.03f, 0.06f);
        float j = GetRandomFloat(0.03f, 0.06f);
        board.SetPos(i, j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
    }

    IEnumerator MovementThree()
    {
        float i = GetRandomFloat(0.03f, 0.06f);
        float j = GetRandomFloat(0.03f, 0.06f);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
    }

    IEnumerator MovementFour()
    {
        float i = GetRandomFloat(0.03f, 0.06f);
        float j = GetRandomFloat(0.03f, 0.06f);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
        board.SetPos(0, 0);
    }
    */

    public float GetRandomFloat (float min, float max) { 
         int i = rand.Next(0, 2);
       
            if (i == 0)
            {
                i = -1; 
            } else {
                i = 1;
             }

        return (Random.Range(min, max) * i);
    }
}

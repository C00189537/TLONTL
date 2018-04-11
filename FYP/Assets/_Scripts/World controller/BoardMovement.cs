using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour {


    public NetworkOutput board;

    public WorldController world;
    public NetworkInput input;
    public float moveTime;
    public float increment, l, m; 
    System.Random rand = new System.Random();

    public float boardOffset;
    public bool done = false; 

    float k;

    void Start () {
        world = GetComponent<WorldController>();
        input = GetComponent<NetworkInput>();
        board = GetComponent<NetworkOutput>();
        board.SetPos(0, 0);
        k = 0.0f;
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
                increment = 0.002f;
                moveTime = 0.3f;
                boardOffset = 0.007f; 
                break;
            case 3:
                increment = 0.002f;
                moveTime = 0.3f;
                boardOffset = 0.009f;
                break;
            case 4:
                increment = 0.003f;
                moveTime = 0.2f;
                boardOffset = 0.011f;
                break;
            case 5:
                increment = 0.004f;
                moveTime = 0.1f;
                boardOffset = 0.015f; 
                break;
            default:
                break;
        }
        k = 0;
        l = PosNegValue(1);
        m = PosNegValue(1);
        StartCoroutine(Movement(boardOffset, increment, moveTime));
    }

    IEnumerator Movement(float offset, float inc, float time)
    {
       

        while (k < offset && done == false)
        {
            k += inc;
            yield return new WaitForSeconds(time);
            board.SetPos(l * k , m * k );

            if (k > offset)
            {
                done = true;
                break;
            }
        }

        while (done == true )
        {
            k -= 0.03f;
            board.SetPos(k, k);

            if (k <= 0)
            {
                done = false;
                board.SetPos(0, 0);
                k = 0;
                yield return new WaitForSeconds(5);
                break;
            }
        }

        
    }

    public void ResetBoard()
    {
        board.SetPos(0, 0); 

    }

    public void Boardvibration()
    {
        board.SetPos(0.001f, 0.001f);
        board.SetPos(-0.001f, -0.001f);
        board.SetPos(0, 0);
    }

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

    public float PosNegValue(float val)
    {
        int j = rand.Next(0, 2);

        if (j == 0)
        {
            return val; 
        }
        else
        {
            return -val;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour {


    public NetworkOutput board;

    public WorldController world;
    public NetworkInput input;
    System.Random rand = new System.Random();
    // Use this for initialization

    void Start () {
        world = GetComponent<WorldController>();
        input = GetComponent<NetworkInput>();
        board = GetComponent<NetworkOutput>();
	}
	
	// Update is called once per frame
	void Update () {
        //move the track
        int board = rand.Next(0, 10);
        if (board % 3 == 0)
        {
           // BoardMovements();
        }
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
                StartCoroutine(MovementOne());
                break;
            case 3:
                StartCoroutine(MovementTwo());
                break;
            case 4:
                StartCoroutine(MovementThree());
                break;
            default:
                break;
        }
         
    }

    IEnumerator MovementOne()
    {
        float i = GetRandomFloat(0, 0.03f);
        float j = GetRandomFloat(0, 0.03f);
        board.SetPos(i, j);
        yield return new WaitForSeconds(3);
        board.SetPos(0, 0);
       /* yield return new WaitForSeconds(3);
        i = GetRandomFloat(0, 0.03f);
        j = GetRandomFloat(0, 0.03f);
        board.SetPos(i, j);
        yield return new WaitForSeconds(3);
        board.SetPos(0, 0);*/
        
    }

    IEnumerator MovementTwo()
    {
        float i = GetRandomFloat(0.03f, 0.06f);
        float j = GetRandomFloat(0.03f, 0.06f);
        board.SetPos(i, j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
        /* yield return new WaitForSeconds(2);
         i = GetRandomFloat(0.03f, 0.06f);
         j = GetRandomFloat(0.03f, 0.06f);
         board.SetPos((float)i, (float)j);
         yield return new WaitForSeconds(2);
         board.SetPos(0, 0);
         yield return new WaitForSeconds(2);
         i = GetRandomDouble(0.03, 0.06);
         j = GetRandomDouble(0.03, 0.06);
         board.SetPos((float)i, (float)j);
         yield return new WaitForSeconds(2);
         board.SetPos(0, 0); */
    }

    IEnumerator MovementThree()
    {
        float i = GetRandomFloat(0.06f, 0.1f);
        float j = GetRandomFloat(0.06f, 0.1f);
        board.SetPos((float)i, (float)j);
        /* yield return new WaitForSeconds(1);
        i = GetRandomFloat(0.06f, 0.1f);
        j = GetRandomFloat(0.06f, 0.1f);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
       i = GetRandomDouble(0.06, 0.1);
        j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
        board.SetPos(0, 0);
        yield return new WaitForSeconds(1);
        i = GetRandomDouble(0.06, 0.1);
        j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j); */
        yield return new WaitForSeconds(1);
        board.SetPos(0, 0);
    }

    public double GetRandomDouble(double min, double max)
    {
        int i = rand.Next(0, 1);
       
        if (i == 0)
        {
            i = -1; 
        }
        
        return ( rand.NextDouble() * (max - min) + min) * i; 
    }

    public float GetRandomFloat (float min, float max) { 
         int i = rand.Next(0, 2);
       
            if (i == 0)
            {
            i = -1; 
            }

        return (Random.Range(min, max) * i);
    }
}

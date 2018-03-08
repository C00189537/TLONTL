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
            BoardMovements();
        }
    }

    public void BoardMovements()
    {
        int variable = world.difficulty;    

        if (input.nManual == 0)
        {
            variable = world.difficulty;
        }
        if (input.nManual == 1)
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
        double i = GetRandomDouble(0, 0.03);
        double j = GetRandomDouble(0, 0.03);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(3);
        board.SetPos(0, 0);
        yield return new WaitForSeconds(3);
        i = GetRandomDouble(0, 0.03);
        j = GetRandomDouble(0, 0.03);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(3);
        board.SetPos(0, 0);
        
    }

    IEnumerator MovementTwo()
    {
        double i = GetRandomDouble(0.03, 0.06);
        double j = GetRandomDouble(0.03, 0.06);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
        yield return new WaitForSeconds(2);
        i = GetRandomDouble(0.03, 0.06);
        j = GetRandomDouble(0.03, 0.06);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0);
       /* yield return new WaitForSeconds(2);
        i = GetRandomDouble(0.03, 0.06);
        j = GetRandomDouble(0.03, 0.06);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(2);
        board.SetPos(0, 0); */
    }

    IEnumerator MovementThree()
    {
        double i = GetRandomDouble(0.06, 0.1);
        double j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
        i = GetRandomDouble(0.06, 0.1);
        j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
      /*  i = GetRandomDouble(0.06, 0.1);
        j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1);
        board.SetPos(0, 0);
        yield return new WaitForSeconds(1);
        i = GetRandomDouble(0.06, 0.1);
        j = GetRandomDouble(0.06, 0.1);
        board.SetPos((float)i, (float)j);
        yield return new WaitForSeconds(1); */
        board.SetPos(0, 0);
    }

    public double GetRandomDouble(double min, double max)
    {
        int i = rand.Next(0, 1);
       
        if (i == 0)
        {
            i = -1; 
        }
        
        return (rand.NextDouble() * (max - min) + min) * i; 
    }
}

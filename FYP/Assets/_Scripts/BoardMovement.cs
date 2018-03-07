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
            case 0:
                break;
            case 1:
                StartCoroutine(MovementOne());
                break;
            case 2:
                StartCoroutine(MovementTwo());
                break;
            case 3:
                StartCoroutine(MovementThree());
                break;
            case 4:
                StartCoroutine(MovementFour());
                break;
            default:
                break;
        }
         
    }

    IEnumerator MovementOne()
    {
        //double i = rand.NextDouble();
        //board.SetPos((float)i, (float)i);
        board.SetPos(1, 1);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(1.5f, 1.5f);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(2, 2);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(2.5f, 2.5f);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
        yield return new WaitForSeconds(rand.Next(1, 3));
    }

    IEnumerator MovementTwo()
    {
        double i = rand.NextDouble();
        board.SetPos((float)i, (float)i);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double j = rand.NextDouble();
        board.SetPos((float)-j, (float)-j);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
    }

    IEnumerator MovementThree()
    {
        double i = rand.NextDouble();
        board.SetPos((float)i, (float)i);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double j = rand.NextDouble();
        board.SetPos((float)-j, (float)-j);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double k = rand.NextDouble();
        board.SetPos((float)-k, (float)k);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
    }

    IEnumerator MovementFour()
    {
        double i = rand.NextDouble();
        board.SetPos((float)i, (float)i);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double j = rand.NextDouble();
        board.SetPos((float)-j, (float)-j);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double k = rand.NextDouble();
        board.SetPos((float)-k, (float)k);
        yield return new WaitForSeconds(rand.Next(1, 3));
        double l = rand.NextDouble();
        board.SetPos((float)l, (float)-l);
        yield return new WaitForSeconds(rand.Next(1, 3));
        board.SetPos(0, 0);
    }

}

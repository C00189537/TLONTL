using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour
{


    public NetworkOutput board;

    public WorldController world;
    public NetworkInput input;
    public float moveTime;
    public float increment, l, m;
    System.Random rand = new System.Random();

    public float boardOffset;
    public bool done = false;

    float k;

    void Start()
    {
        world = GetComponent<WorldController>();
        input = GetComponent<NetworkInput>();
        board = GetComponent<NetworkOutput>();
        board.SetPos(0, 0);
        k = 0.0f;
    }

    public void BoardMovements()
    {
        int variable = Difficultycontroller.GetInstance().difficulty; ;

        if (input.NManual == 0)
        {
            variable = Difficultycontroller.GetInstance().difficulty;
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
        //StartCoroutine(Movements(moveTime));
    }

    IEnumerator Movement(float offset, float inc, float time)
    {


        while (k < offset && done == false)
        {
            k += inc;
            yield return new WaitForSeconds(time);
            board.SetPos(l * k, m * k);

            if (k > offset)
            {
                done = true;
                break;
            }
        }

        while (done == true)
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

    public IEnumerator Movements(float time)
    //start posotion 0.0  end position 0.04
    {
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 endPosition = new Vector3(0.03f, 0.03f, 0.03f);
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        board.SetPos((direction * (Time.deltaTime * (distance / time))).x * l, (direction * (Time.deltaTime * (distance / time))).y * m);
        Debug.Log((direction * (Time.deltaTime * (distance / time))).x);
        yield return new WaitForSeconds(1);

        Vector3 startPositionB = new Vector3(0.03f, 0.03f, 0.03f);
        Vector3 endPositionB = new Vector3(0, 0, 0);
        Vector3 directionB = endPosition - startPosition;
        float distanceB = direction.magnitude;

        board.SetPos((direction * (Time.deltaTime * (distance / time))).x * l, (direction * (Time.deltaTime * (distance / time))).y * m);

    }

    public IEnumerator ObstacleHit()
    {
        board.SetPos(0, -0.007f);
        yield return new WaitForSeconds(1);
        board.SetPos(0, 0);
    }

    public void Boardvibration()
    {
        board.SetPos(0.01f, 0.01f);
        board.SetPos(0, 0);
    }

    public float GetRandomFloat(float min, float max)
    {
        int i = rand.Next(0, 2);

        if (i == 0)
        {
            i = -1;
        }
        else
        {
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

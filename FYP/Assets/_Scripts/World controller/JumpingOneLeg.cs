using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingOneLeg : MonoBehaviour
{

    public WorldController theWorld;
    System.Random rand = new System.Random();

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
    }

    public void FormationOne(int val)
    {
        int side = rand.Next(1, 3);
        if (side == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad3").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad4").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad5").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0, 0);
        }
        else
        {
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad3").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad4").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad5").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0, 0);
        }

    }

    public void FormationTwo(int val)
    {
        int side = rand.Next(1, 3);
        if (side == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad3").Translate(0, 1, -6);
            theWorld.trackPiece[val].transform.Find("Pad4").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad5").Translate(0, 1, -6);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0, 0);
        }
        else {
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").Translate(0, 1, -6);
            theWorld.trackPiece[val].transform.Find("Pad3").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad4").Translate(0, 1, -6);
            theWorld.trackPiece[val].transform.Find("Pad5").Translate(0, 1, 0);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0, 0);
        }

    }

    public void FormationThree(int val)
    {
        int side = rand.Next(1, 3);
        //spawns the platforms on the same side
        if (side == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad3").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad5").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
        }
        else
        {
            theWorld.trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad4").gameObject.SetActive(false);
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
        }
    }

    public void FormationFour(int val)
    {

    }

    public void FormationFive(int val)
    {
        int front = 1;
        int back = 2;
        int endP = rand.Next(1, 3);

        //Spawns random pads
        if (front == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad3").gameObject.SetActive(false);
        }
        else
        {
            theWorld.trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad2").gameObject.SetActive(false);
        }
        if (back == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad5").gameObject.SetActive(false);
        }
        else
        {
            theWorld.trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad4").gameObject.SetActive(false);
        }
        if (endP == 1)
        {
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
        }
        else
        {
            theWorld.trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0.0f, 0);
            theWorld.trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
        }
    }
}

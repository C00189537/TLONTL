using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{


    System.Random rand = new System.Random();

    public WorldController theWorld;
    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
    }

    public void JumpOne(int val)
    {
        theWorld.trackPiece[val].transform.Find("WallMid").Translate(0, 2.5f, 0);
        theWorld.trackPiece[val].transform.Find("JumpZoneM").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
    }

    public void JumpTwo(int val)
    {
        theWorld.trackPiece[val].transform.Find("WallMid").Translate(0, 3.5f, 0);
        theWorld.trackPiece[val].transform.Find("JumpZoneM").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
    }

    public void JumpThree(int val)
    {
        theWorld.trackPiece[val].transform.Find("WallFront").Translate(0, 2.5f, -3);
        theWorld.trackPiece[val].transform.Find("WallBack").Translate(0, 2.5f, 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
    }

    public void JumpFour(int val)
    {
        theWorld.trackPiece[val].transform.Find("WallFront").Translate(0, 2.5f, -3);
        theWorld.trackPiece[val].transform.Find("WallBack").Translate(0, 3.5f, 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
    }

    public void JumpFive(int val)
    {
        theWorld.trackPiece[val].transform.Find("WallFront").Translate(0, 3.5f, -3);
        theWorld.trackPiece[val].transform.Find("WallBack").Translate(0, 3.5f, 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
        theWorld.trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * theWorld.speed) + 1);
    }
}

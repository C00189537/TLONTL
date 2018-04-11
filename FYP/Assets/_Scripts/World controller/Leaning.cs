using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaning : MonoBehaviour {

    System.Random rand = new System.Random();

    public WorldController theWorld;
    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        theWorld = gameControllerObject.GetComponent<WorldController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ObstalceOne(int val)
    {
        theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(rand.Next(-4, 5), 2, 0);
        theWorld. trackPiece[val].transform.Find("ScoreOb2").Translate(rand.Next(-4, 5), 2, 0);
    }

    public void ObstacleTwo(int val)
    {
        int set = rand.Next(1, 4);

        switch (set)
        {
            case 1:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(1, 2, -1);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(rand.Next(-4, 5), 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(rand.Next(-4, 5), 2, -7.5f);
                break;
            case 2:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate((rand.Next(-4, 5)), 2, -1);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(1, 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(rand.Next(-4, 5), 2, -7.5f);
                break;
            case 3:
                int side = rand.Next(-4, 5);
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(side, 2, -1);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(side, 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(-side, 2, -7.5f);
                break;
        }
    }

    public void ObstacleThree(int val)
    {
        int set = rand.Next(1, 4);

        switch (set)
        {
            case 1:
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(-3, 2, -12);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(0, 2, 0);
                break;
            case 2:
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(2, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(0, 2, -14);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(1, 2, -2);
                break;
            case 3:
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(3, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(0, 2, -9);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(-1, 2, 0);
                break;
        }
    }

    public void ObstacleFour(int val)
    {
        int set = rand.Next(1, 4);

        switch (set)
        {
            case 1:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(2, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(2, 2, 0);
                break;
            case 2:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(-3, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(3, 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(2, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(-1, 2, 0);
                break;
            case 3:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(3, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(-3.5f, 2, 0);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(-1, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(2, 2, 0);
                break;
        }
    }

    public void ObstacleFive(int val)
    {
        int set = rand.Next(1, 4);

        switch (set)
        {
            case 1:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(-2, 2, 9);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(0, 2, 9);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(2, 2, -9);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(0, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(2, 2, 8);
                break;
            case 2:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(3.5f, 2, 9);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(1, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(-3.5f, 2, -9);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(-1, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(-1, 2, 0);
                break;
            case 3:
                theWorld.trackPiece[val].transform.Find("ObstacleFront").Translate(-3.5f, 2, 9);
                theWorld.trackPiece[val].transform.Find("ObstacleMid").Translate(-1, 2, 0);
                theWorld.trackPiece[val].transform.Find("ObstacleBack").Translate(3.5f, 2, -9);
                theWorld.trackPiece[val].transform.Find("ScoreOb1").Translate(0, 2, 2);
                theWorld.trackPiece[val].transform.Find("ScoreOb2").Translate(0, 2, 0);
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour {

    public GameObject[] platformLayout = new GameObject[6];
    public int[] trackAvailable = new int[6];
    private int temp;
    Camera cam;


    private int[] trackers = new int[3];
    private int currentTracker = 0;
    private int nextTracker = 1;
    private int farTracker = 0;

    bool earthquake = true;

    //Destroys track off screen
    public float killPoint;

    //Sections of track
    private int TRACK_SIZE = 3;
    private GameObject[] trackPiece = new GameObject[3];
    private GameObject currentSection;
    private GameObject nextSection;
    private GameObject farSection;

    //Scrolling speed of the platforms
    public float speed;
    public float minSpeed;
    public float maxSpeed;

    private Vector3 scrollSpeed;
    private Vector3 oneLegScrollSpeed;
    public bool oneLegSpeed;

    //Where the next section will spawn
    public float spawnPoint;
    public float spawnPointFar;

    //Random number gen
    System.Random rand = new System.Random();
    public int minRand;
    public int maxRand;

    public Text scoreText;
    public Text timerText;
    public Text gameOverText;

    //Obstacles
    public GameObject[] blockade = new GameObject[3];

    public double score = 0;

    public double timeLeft = 30;
    public int difficulty;

    public bool gameOver;

    Stepping stepping;
    public NetworkInput input;
    public NetworkOutput board; 

    void Start()
    {
        stepping = GetComponent<Stepping>();

        //Initialise camera obj
        GameObject camOb = GameObject.FindWithTag("MainCamera");
        if (camOb != null)
        {
            cam = camOb.GetComponent<Camera>();
        }

        input = GetComponent<NetworkInput>();
        board = GetComponent<NetworkOutput>();

        //initialise the first platforms
        currentSection = Instantiate(platformLayout[currentTracker], new Vector3(0, 0, 0.0f), transform.rotation) as GameObject;
        nextSection = Instantiate(platformLayout[nextTracker], new Vector3(0, 0, spawnPoint), transform.rotation) as GameObject;
        farSection = Instantiate(platformLayout[farTracker], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;

        //Set up the track array
        trackPiece[0] = currentSection;
        trackPiece[1] = nextSection;
        trackPiece[2] = farSection;

        //Initialises the first set of obstacles
        ObstacleSpawn(nextTracker);

        //Set up the trackers array
        trackers[0] = currentTracker;
        trackers[1] = nextTracker;
        trackers[2] = farTracker;

        scrollSpeed = new Vector3(0.0f, 0.0f, speed);

        gameOver = false;
        oneLegSpeed = false;
        timerText.text = "Time: " + timeLeft;

        StartCoroutine(Earthquake());
        UpdateMaxSpeed();
        UpdateOneLegSpeed();
    }

    void Update()
    {

        for (int i = 0; i < TRACK_SIZE; i++)
        {
            //ScrollingWorld(trackPiece[i]);
            if (oneLegSpeed)
            {
                trackPiece[i].transform.position -= oneLegScrollSpeed;
            }
            else
            {
                trackPiece[i].transform.position -= scrollSpeed;
            }
           
        }
        if (input.nManual == 1)
        {
            UpdateAvailableTracks();
        }
        
        UpdateTrack();
        UpdateScore();
        timer();
        //move the track
        BoardMovements();
    }

    void UpdateAvailableTracks()
    {
        trackAvailable[1] = (int)input.nLeaning;
        trackAvailable[2] = (int)input.nOneLeg;
        trackAvailable[3] = (int)input.nJumping;
        trackAvailable[4] = (int)input.nStepping;
        trackAvailable[5] = (int)input.nJumpingOneleg;
    }

    void UpdateTrack()
    {
        //Creates a looping track
        for (int i = 0; i < TRACK_SIZE; i++)
        {
            if (trackPiece[i].transform.position.z <= killPoint)
            {
                Destroy(trackPiece[i]);
                //If the track piece is a base piece the next one will be a random exercise
                if (trackers[i] == 0)
                {
                    temp = rand.Next(minRand, maxRand + 1);

                    int count = 0; 
                    while (trackAvailable[temp] != 1)
                    {
                        
                        temp = rand.Next(minRand, maxRand + 1);
                        count++;

                         if (count >= 60){
                            temp = 1;
                            break;
                        }
                        
                    }
                    trackers[i] = temp;
                    trackPiece[i] = Instantiate(platformLayout[trackers[i]], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;

                        //Set up the track pieces
                        switch (trackers[i])
                        {
                            case 1:
                                ObstacleSpawn(i);
                                break;
                            case 2:
                                OneLegSpawn(i);
                                break;
                            case 3:
                                JumpSpawn(i);
                                break;
                            case 4:
                                break;
                            case 5:
                                DifJumpSpawn(i);
                                break;
                            default:
                                break;
                        }

                    
                    
                }
                //If the track piece is an exercise, the next one will be a base piece
                else if (trackers[i] > 0)
                {
                    trackers[i] = 0;
                    trackPiece[i] = Instantiate(platformLayout[trackers[i]], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;
                    CoinSpawn(i);
                }
                if (TRACK_SIZE > i + 1)
                {
                    trackPiece[i + 1].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (TRACK_SIZE > i + 2)
                {
                    trackPiece[i + 2].transform.position = new Vector3(0.0f, 0.0f, 30.0f);
                }
                
            }
        }
    }

    void CoinSpawn(int val)
    {
        int randy = rand.Next(-4, 5);
        trackPiece[val].transform.Find("ScoreOb1").Translate(randy, 2, 0);
        trackPiece[val].transform.Find("ScoreOb2").Translate(randy, 2, 0);
    }

    void ObstacleSpawn(int val)
    {
        int variable = difficulty; 

        if (input.nManual == 0.0f)
        {
            variable = difficulty;
        } 
        if (input.nManual == 1)
        {
            variable = (int)input.nObstackles;
        }

        switch (variable)
        {
            case 0:
                break;
            case 1:
                trackPiece[val].transform.Find("ObstacleMid").Translate(rand.Next(-4, 5), 2, 0);
                break;
            case 2:
                trackPiece[val].transform.Find("ObstacleFront").Translate(rand.Next(-4, 5), 2, 0);
                trackPiece[val].transform.Find("ObstacleBack").Translate(rand.Next(-4, 5), 2, 0);
                break;
            case 3:
                trackPiece[val].transform.Find("ObstacleFront").Translate(rand.Next(-4, 5), 2, 0);
                trackPiece[val].transform.Find("ObstacleMid").Translate(rand.Next(-4, 5), 2, 0);
                trackPiece[val].transform.Find("ObstacleBack").Translate(rand.Next(-4, 5), 2, 0);
                break;
            default:
                break;
        }

        trackPiece[val].transform.Find("ObstacleFront").localScale = trackPiece[val].transform.Find("ObstacleFront").localScale * difficulty * 0.75f;
        trackPiece[val].transform.Find("ObstacleMid").localScale = trackPiece[val].transform.Find("ObstacleMid").localScale * difficulty * 0.75f;
        trackPiece[val].transform.Find("ObstacleBack").localScale = trackPiece[val].transform.Find("ObstacleBack").localScale * difficulty * 0.75f;
    }
    void OneLegSpawn(int val)
    {

        //Randomly pick a side
        int side = rand.Next(1, 3);
        if (side == 1)
        {
            //Right
            trackPiece[val].transform.Find("OLMid").Translate(-3, 0, 0);
        }
        else if (side == 2)
        {
            //Left
            trackPiece[val].transform.Find("OLMid").Translate(3, 0, 0);
        }

    }
    void JumpSpawn(int val)
    {

        int variable = difficulty;

        if (input.nManual == 0)
        {
            variable = difficulty;
        }
        if (input.nManual == 1)
        {
            variable = (int)input.nJumpDifficulty;
        }

        switch (variable)
            {
                case 0:
                    trackPiece[val].transform.Find("WallMid").Translate(0, 2.5f, 0);
                    break;
                case 1:
                    trackPiece[val].transform.Find("WallMid").Translate(0, 3.5f, 0);
                    break;
                case 2:
                    trackPiece[val].transform.Find("WallFront").Translate(0, 2.5f, 0);
                    trackPiece[val].transform.Find("WallBack").Translate(0, 2.5f, 0);
                    break;
                case 3:
                    trackPiece[val].transform.Find("WallFront").Translate(0, 3.5f, 0);
                    trackPiece[val].transform.Find("WallBack").Translate(0, 3.5f, 0);
                    break;
                default:
                    break;
            }
    }

    void DifJumpSpawn(int val)
    {
        if (difficulty == 1)
        {
            int side = rand.Next(1,3);
            //spawns the platforms on the same side
            if (side == 1)
            {
                trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
            }
            else
            {
                trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
            }
        }
        else
        {
            int front = rand.Next(1, 3);
            int back = rand.Next(1, 3);
            //Spawns random pads
            if (front == 1)
            {
                trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
            }
            else
            {
                trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
            }
            if (back == 1)
            {
                trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
            }
            else
            {
                trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
            }
        }

    }
    public void UpdateSpeed(float v)
    {
        if (input.nManual == 0)
        {
            speed += v;
        }
        if (input.nManual == 1)
        {
            speed = input.nSpeed;
        }

        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
        else if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        scrollSpeed = new Vector3(0.0f, 0.0f, speed);
    }

    public void UpdateMaxSpeed()
    {
        switch (difficulty)
        {
            case 0:
                maxSpeed = 0.2f;
                break;
            case 1:
                maxSpeed = 0.3f;
                break;
            case 2:
                maxSpeed = 0.4f;
                break;
            case 3:
                maxSpeed = 0.5f;
                break;
            default:
                break;
        }
    }


        public void UpdateOneLegSpeed()
    {
        int variable = difficulty;

        if (input.nManual == 0)
        {
            variable = difficulty;
        }
        if (input.nManual == 1)
        {
            variable = (int)input.nOneLegSpeed;
        }

        switch (variable)
            {
                case 0:
                    oneLegScrollSpeed = new Vector3(0, 0, 0.2f);
                break;
                case 1:
                    oneLegScrollSpeed = new Vector3(0, 0, 0.2f);
                break;
                case 2:
                    oneLegScrollSpeed = new Vector3(0, 0, 0.15f);
                    break;
                case 3:
                    oneLegScrollSpeed = new Vector3(0, 0, 0.1f);
                    break;
                default:
                    break;
            }
        
    }

    void ScrollingWorld(GameObject g)
    {
        g.transform.position -= scrollSpeed;
    }

    public void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString("F");
    }
    private void SetTimerText()
    {
        timerText.text = "Time: " + timeLeft.ToString("F");
    }

    void timer()
    {
        timeLeft -= Time.deltaTime;
        SetTimerText();
        if (timeLeft < 0)
        {
            //Game over
            gameOver = true;
            timeLeft = 0.00;
            gameOverText.text = "Time is up!";
            SetTimerText();
        }
    }
    void UpdateScore()
    {
        if (!gameOver)
        {
            score += speed * Time.deltaTime;
            SetScoreText();
        } 
    }

    public void SteppingStones()
    {
        if (input.nManual == 0)
        {
            stepping.GenerateButtons(difficulty);
        }
        if (input.nManual == 1)
        {
            stepping.GenerateButtons((int)input.nNumberofSteps);
        }
    }

    public void BoardMovements()
    {
        int variable = difficulty;

        if (input.nManual == 0)
        {
            variable = difficulty;
        }
        if (input.nManual == 1)
        {
            variable = (int)input.nBoardMovements;
        }

        switch (variable)
        {
            case 0:
                board.SetPos(0.5f, 0.5f); 
                break;
            case 1:
                board.SetPos(1.0f, 1.0f);
                break;
            case 2:
                board.SetPos(1.5f, 1.5f);
                break;
            case 3:
                board.SetPos(2.0f, 2.0f);
                break;
            default:
                break;
        }

    }

    //Flashes 
    IEnumerator Earthquake()
    {
        while(earthquake)
        {
            int variable = difficulty;

            if (input.nManual == 0)
            {
                variable = difficulty;
            }
            if (input.nManual == 1)
            {
                variable = (int)input.nEarthquakeShake;
            }
            switch (variable)
            {
                case 0:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.05f);
                    break;
                case 1:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.1f);
                    break;
                case 2:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.2f);
                    break;
                case 3:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.3f);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(rand.Next(5, 10));
            Debug.Log("Shake");
            //Earthquake
            cam.GetComponent<CameraShake>().SetTimer(1.0f);
        }    
    }
}

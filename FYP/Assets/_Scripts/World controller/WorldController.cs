﻿using System;
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
    private int nextTracker = 0;
    private int farTracker = 0;
    private int oneLegTracker = 0;
    public int oneLegDif = 1;

    private int restPhase;
    public int restTracks;

    bool earthquake = true;

    //Destroys track off screen
    public float killPoint;

    //Sections of track
    private int TRACK_SIZE = 3 ;
    public GameObject[] trackPiece = new GameObject[3];
    private GameObject currentSection;
    private GameObject nextSection;
    private GameObject farSection;

    //Scrolling speed of the platforms
    public float speed;
    public float minSpeed;
    public float maxSpeed;

    private Vector3 scrollSpeed;

    //Where the next section will spawn
    public float spawnPoint;
    public float spawnPointFar;

    //Random number gen
    System.Random rand = new System.Random();
    public int minRand;
    public int maxRand;

    public Text scoreText;

    //Obstacles
    public GameObject[] blockade = new GameObject[3];

    public double score = 0;
    public int scoreInt = 0; 

    public double timeLeft = 30;
    public int difficulty;
    public int difficultyScore = 0;

    public bool gameOver;

    Stepping stepping;
    Leaning leaning; 
    public NetworkInput input;

    void Start()
    {
        FloatingTextController.Initialize();
        stepping = GetComponent<Stepping>();
        leaning = GetComponent<Leaning>(); 

        restPhase = 4; 

        //Initialise camera obj
        GameObject camOb = GameObject.FindWithTag("MainCamera");
        if (camOb != null)
        {
            cam = camOb.GetComponent<Camera>();
        }

        input = GetComponent<NetworkInput>();

        //initialise the first platforms
        currentSection = Instantiate(platformLayout[currentTracker], new Vector3(0, 0, 0.0f), transform.rotation) as GameObject;
        nextSection = Instantiate(platformLayout[nextTracker], new Vector3(0, 0, spawnPoint), transform.rotation) as GameObject;
        farSection = Instantiate(platformLayout[farTracker], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;

        //Set up the track array
        trackPiece[0] = currentSection;
        trackPiece[1] = nextSection;
        trackPiece[2] = farSection;

        //Initialises the first set of obstacles
        //ObstacleSpawn(nextTracker);

        //Set up the trackers array
        trackers[0] = currentTracker;
        trackers[1] = nextTracker;
        trackers[2] = farTracker;

        scrollSpeed = new Vector3(0.0f, 0.0f, speed);

        StartCoroutine(Earthquake());
      
    }

    public void BeginDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                difficultyScore = 2;
                break;
            case 2:
                difficultyScore = 7;
                break;
            case 3:
                difficultyScore = 20;
                break;
            case 4:
                difficultyScore = 35;
                break;
            case 5:
                difficultyScore = 55;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        for (int i = 0; i < TRACK_SIZE; i++)
        {
            trackPiece[i].transform.position -= scrollSpeed;   
        }

        
        UpdateAvailableTracks(); 
        UpdateTrack();
        UpdateScore();
        SpeedUpdate();

        if (input.NManual == 0)
        {
            UpdateDifficultyScore();
        }
      
        
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
                if (restPhase > restTracks)
                {
                    temp = rand.Next(minRand, maxRand + 1);
                    restPhase = 0; 
                    int count = 0; 
                    while (trackAvailable[temp] != 1)
                    {
                        
                        temp = rand.Next(minRand, maxRand + 1);
                        count++;

                         if (count >= 20){
                            temp = 0;
                            restPhase = restTracks; 
                            break;
                        }
                        
                    }
                    trackers[i] = temp;
                    //Longer one leg
                    if (temp == 2)
                    {
                        if (input.NManual == 1)
                        {
                            switch ((int)input.nOneLegDifficulty)
                            {
                                case 1:
                                    //1 length
                                    oneLegTracker = 0;
                                    break;
                                case 2:
                                    //2 length
                                    oneLegTracker = 1;
                                    break;
                                case 3:
                                    //3 length
                                    oneLegTracker = 2;
                                    break;
                                case 4:
                                    //4 length
                                    oneLegTracker = 3;
                                    break;
                                case 5:
                                    //5 length
                                    oneLegTracker = 4;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (oneLegDif)
                            {
                                case 1:
                                    //1 length
                                    oneLegTracker = 0;
                                    break;
                                case 2:
                                    //2 length
                                    oneLegTracker = 1;
                                    break;
                                case 3:
                                    //3 length
                                    oneLegTracker = 2;
                                    break;
                                case 4:
                                    //4 length
                                    oneLegTracker = 3;
                                    break;
                                case 5:
                                    //5 length
                                    oneLegTracker = 4;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    trackPiece[i] = Instantiate(platformLayout[trackers[i]], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;
         
                    //Set up the track pieces
                    switch (trackers[i])
                    {
                        case 1:
                            //CoinSpawn(i);
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
                else if (oneLegTracker > 0)
                {
                    oneLegTracker--;
                    trackPiece[i] = Instantiate(platformLayout[2], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;
                    OneLegSpawn(i);
                }
                //If the track piece is an exercise, the next one will be a base piece
                else if (restPhase <= restTracks)  //(trackers[i] > 0)
                {
                    restPhase++; 
                    trackers[i] = 0;
                    trackPiece[i] = Instantiate(platformLayout[trackers[i]], new Vector3(0, 0, spawnPointFar), transform.rotation) as GameObject;
                    
                }
                //Alligns the track
                if (TRACK_SIZE > i + 1)
                {
                    trackPiece[i + 1].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (TRACK_SIZE > i + 2)
                {
                    trackPiece[i + 2].transform.position = new Vector3(0.0f, 0.0f, spawnPoint);
                }
                
            }
        }
    }

    void ObstacleSpawn(int val)
    {
        int variable = difficulty; 

        if (input.NManual == 0.0f)
        {
            variable = difficulty;
        } 
        if (input.NManual == 1)
        {
            variable = (int)input.nObstackles;
        }

        switch (variable)
        {
            case 1:
                leaning.ObstalceOne(val);
                break;
            case 2:
                leaning.ObstacleTwo(val);
                break;
            case 3:
                leaning.ObstacleThree(val);
                break;
            case 4:
                leaning.ObstacleFour(val);
                break;
            case 5:
                leaning.ObstacleFive(val);
                break;
            default:
                break;
        }

        trackPiece[val].transform.Find("ObstacleFront").localScale = trackPiece[val].transform.Find("ObstacleFront").localScale * 2; //difficulty * 0.75f;
        trackPiece[val].transform.Find("ObstacleMid").localScale = trackPiece[val].transform.Find("ObstacleMid").localScale * 2;//difficulty * 0.75f;
        trackPiece[val].transform.Find("ObstacleBack").localScale = trackPiece[val].transform.Find("ObstacleBack").localScale * 2;//difficulty* 0.75f;
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

        if (input.NManual == 0)
        {
            variable = difficulty;
        }
        if (input.NManual == 1)
        {
            variable = (int)input.nJumpDifficulty;
        }

        switch (variable)
            {
                case 1:
                    trackPiece[val].transform.Find("WallMid").Translate(0, 2.5f, 0);
                    trackPiece[val].transform.Find("JumpZoneM").Translate(0, 0.6f, (9 * speed) + 1);
                break;
                case 2:
                    trackPiece[val].transform.Find("WallMid").Translate(0, 3.5f, 0);
                    trackPiece[val].transform.Find("JumpZoneM").Translate(0, 0.6f, (9 * speed) + 1);
                break;
                case 3:
                    trackPiece[val].transform.Find("WallFront").Translate(0, 2.5f, -3);
                    trackPiece[val].transform.Find("WallBack").Translate(0, 2.5f, 1);
                    trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * speed) + 1);
                    trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * speed) + 1);

                break;
                case 4:
                    trackPiece[val].transform.Find("WallFront").Translate(0, 2.5f, -3);
                    trackPiece[val].transform.Find("WallBack").Translate(0, 3.5f, 1);
                    trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * speed) + 1);
                    trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * speed) + 1);
                break;
                 case 5:
                    trackPiece[val].transform.Find("WallFront").Translate(0, 3.5f, -3);
                    trackPiece[val].transform.Find("WallBack").Translate(0, 3.5f, 1);
                    trackPiece[val].transform.Find("JumpZoneF").Translate(0, 0.6f, (9 * speed) + 1);
                    trackPiece[val].transform.Find("JumpZoneB").Translate(0, 0.6f, (9 * speed) + 1);
                break;
            default:
                    break;
            }
    }

    void DifJumpSpawn(int val)
    {
        int variable = difficulty;

        if (input.NManual == 0)
        {
            variable = difficulty;
        }
        if (input.NManual == 1)
        {
            variable = (int)input.nJumpDifficulty;
        }

        if (variable == 1)
        {
            int side = rand.Next(1,3);
            //spawns the platforms on the same side
            if (side == 1)
            {
                trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad3").gameObject.SetActive(false);
                trackPiece[val].transform.Find("Pad5").gameObject.SetActive(false);

                trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0.0f, 0);
                trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
            }
            else
            {
                trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad2").gameObject.SetActive(false);
                trackPiece[val].transform.Find("Pad4").gameObject.SetActive(false);

                trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0.0f, 0);
                trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
            }
        }
        else
        {
            int front = 1;// rand.Next(1, 3);
            int back = 2;// rand.Next(1, 3);
            int endP = rand.Next(1, 3);
            
            //Spawns random pads
            if (front == 1)
            {
                trackPiece[val].transform.Find("Pad2").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad3").gameObject.SetActive(false);
            }
            else
            {
                trackPiece[val].transform.Find("Pad3").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad2").gameObject.SetActive(false);
            }
            if (back == 1)
            {
                trackPiece[val].transform.Find("Pad4").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad5").gameObject.SetActive(false);
            }
            else
            {
                trackPiece[val].transform.Find("Pad5").Translate(0, 1.0f, 0);
                trackPiece[val].transform.Find("Pad4").gameObject.SetActive(false);
            }
            if (endP == 1)
            {
                trackPiece[val].transform.Find("Pad6").Translate(-2.5f, 0.0f, 0);
                trackPiece[val].transform.Find("Pad1").Translate(2.5f, 0, 0);
            }
            else
            {
                trackPiece[val].transform.Find("Pad6").Translate(2.5f, 0.0f, 0);
                trackPiece[val].transform.Find("Pad1").Translate(-2.5f, 0, 0);
            }
        }

    }

    void SpeedUpdate()
    { 
        speed = input.NSpeed;
        scrollSpeed = new Vector3(0.0f, 0.0f, speed);
    }


    void ScrollingWorld(GameObject g)
    {
        g.transform.position -= scrollSpeed;
    }

    public void SetScoreText()
    {
        
        scoreInt =(int)score;
        scoreText.text =  scoreInt.ToString();
    }
   
    void UpdateScore()
    {
            score += speed * Time.deltaTime * 5;
        if (score < 0)
        {
            score = 0; 
        }
            SetScoreText();
    }
    void UpdateDifficultyScore()
    {
        //Contain within 0-50
        if (difficultyScore  < 0)
        {
            difficultyScore = 0;
        }
        else if (difficultyScore >= 70)
        {
            difficultyScore = 70;
        }
        //Difficulty set based on performance
        if (difficultyScore >= 0 && difficultyScore < 5)
        {
            difficulty = 1;
            oneLegDif = 1;
        }
        else if (difficultyScore >= 5 && difficultyScore < 15)
        {
            difficulty = 2;
            oneLegDif = 2;
        }
        else if (difficultyScore >= 15 && difficultyScore < 30)
        {
            difficulty = 3;
            oneLegDif = 3;
        }
        else if (difficultyScore >= 30 && difficultyScore < 50)
        {
            difficulty = 4;
            oneLegDif = 4;
        }
        else if (difficultyScore >= 50 && difficultyScore < 70)
        {
            difficulty = 5;
            oneLegDif = 5;
        }
    }
    public void SteppingStones()
    {
        if (input.NManual == 0)
        {
            stepping.GenerateButtons(difficulty);
        }
        if (input.NManual == 1)
        {
            stepping.GenerateButtons((int)input.nNumberofSteps);
        }
    }

    public void CamShake()
    {
        cam.GetComponent<CameraShake>().SetShakeAmount(0.1f);
    }

    //Flashes 
    public IEnumerator Earthquake()
    {
        while(earthquake)
        {
            int variable = difficulty;

            if (input.NManual == 0)
            {
                variable = difficulty;
            }
            if (input.NManual == 1)
            {
                variable = (int)input.nEarthquakeShake;
            }
            switch (variable)
            {
                case 1:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.05f);
                    break;
                case 2:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.1f);
                    break;
                case 3:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.2f);
                    break;
                case 4:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.3f);
                    break;
                case 5:
                    cam.GetComponent<CameraShake>().SetShakeAmount(0.35f);
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

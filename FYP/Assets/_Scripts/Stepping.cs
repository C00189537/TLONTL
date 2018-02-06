using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stepping : MonoBehaviour {

    public List<Direction> Steps = new List<Direction>();
    private GameObject[] ArrowsOne = new GameObject[4];
    private GameObject[] ArrowsTwo = new GameObject[5];
    private GameObject[] ArrowsThree = new GameObject[6];
    private int numberOfSteps = 4;
    private Direction left, right, up, down;

    // Use this for initialization
    void Start()
    {
         Steps.Add(new Direction() { DirectionName = "Left", DirectionId = 0, Image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/left.png")});
         Steps.Add(new Direction() { DirectionName = "Right", DirectionId = 1, Image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/right.png")});
         Steps.Add(new Direction() { DirectionName = "Up", DirectionId = 2, Image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/up.png" )});
         Steps.Add(new Direction() { DirectionName = "Down", DirectionId = 3, Image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/down.png" )});

      /*  left = Direction.CreateInstance(0, "left", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/left.png"));
        right = Direction.CreateInstance(1, "right", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/right.png"));
        up = Direction.CreateInstance(2, "up", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/up.png"));
        down = Direction.CreateInstance(3, "down", AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Sprites/Arows/down.png"));*/

        Steps.Add(left);
        Steps.Add(right);
        Steps.Add(up);
        Steps.Add(down);
    }

	
	// Update is called once per frame
	void Update () {
        DifficultyOne();

    }


    void DifficultyOne()
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            int rand = UnityEngine.Random.Range(0, Steps.Count);


            // Direction.CreateInstance(Steps.Exists(x => x.DirectionId == rand)); 
            //Direction.CreateInstance(Steps.Contains(new Direction { DirectionId = rand }).ToString());
            /*Direction step = Steps.Find(delegate (Direction generatedStep)
                {
                    return generatedStep.DirectionId == rand;
                });
            if (step!== null)
            {
                Direction one = Direction.CreateInstance(step.DirectionId, step.DirectionName, step.Image);
               // one. =
                Debug.Log("it found something");
            }
            else
            {
                Debug.Log("Direction not found in Steps list");
            }*/
        }

    }

 
}

public struct Direction 
{
    public int DirectionId { get; set; }
    public string DirectionName { get; set; } // can be the same as keycode to string
    public Texture2D Image { get; set; } 

    /*
    public int DirectionId;
    public string DirectionName;
    public Texture2D Image;
        
    public void Init (int DirectionId, string DirectionName, Texture2D Image)
    {
        this.DirectionId = DirectionId;
        this.DirectionName = DirectionName;
        this.Image = Image; 
    }  

    public static Direction CreateInstance(int DirectionId, string DirectionName, Texture2D Image)
    {
        var data = Instantiate(data, );
        data.Init(DirectionId, DirectionName, Image);
        return data; 
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private Text scoreText; 

    public void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        scoreText = animator.GetComponent<Text>();
    }

    public void SetText (string passedtext, int color)
    {
        scoreText.text = passedtext;

        if (color == 0)
        {
            scoreText.color = Color.green;
        }
        if (color == 1)
        {
            scoreText.color = Color.red; 
        }
    }
}

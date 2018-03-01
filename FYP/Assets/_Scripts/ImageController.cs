using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ImageController : MonoBehaviour {

    public Image iLeaning;
    public Image iOneLeg;
    public Image iStepping;
    public Image iJumpTwoLegs;
    public Image iJumpOneleg;

    public PlayerController3 player; 
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController3>();
        iLeaning.enabled = false;
        iOneLeg.enabled = false;
        iStepping.enabled = false;
        iJumpTwoLegs.enabled = false; 
        iJumpOneleg.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {

        if (player.lean)
        {
            iLeaning.enabled = true;
            iOneLeg.enabled = false;
            iStepping.enabled = false;
            iJumpTwoLegs.enabled = false;
            iJumpOneleg.enabled = false;
        }

        if (player.oneLeg)
        {
            iLeaning.enabled = false;
            iOneLeg.enabled = true;
            iStepping.enabled = false;
            iJumpTwoLegs.enabled = false;
            iJumpOneleg.enabled = false;
        }

        if (player.step)
        {
            iLeaning.enabled = false;
            iOneLeg.enabled = false;
            iStepping.enabled = true;
            iJumpTwoLegs.enabled = false;
            iJumpOneleg.enabled = false;
        }

        if (player.jump)
        {
            iLeaning.enabled = false;
            iOneLeg.enabled = false;
            iStepping.enabled = false;
            iJumpTwoLegs.enabled = true;
            iJumpOneleg.enabled = false;
        }

        if (player.jump2)
        {
            iLeaning.enabled = false;
            iOneLeg.enabled = false;
            iStepping.enabled = false;
            iJumpTwoLegs.enabled = false;
            iJumpOneleg.enabled = true ;
        }
    }
}

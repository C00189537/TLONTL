﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkOutput : MonoBehaviour {

    public DFlowNetwork network;

    public void SetPos(float x, float z)
    {
        network.setInput(0, x);
        network.setInput(1, z);
    }
}

﻿using UnityEngine;
using System.Collections;

public class IronAmulett : ITrinket {

	// Use this for initialization
	void Start () {
        ap = 5;
        cooldown = 1;
        duration = 1;

        // Utility (Q): Block the next melee attack for 1s
    }

    // Update is called once per frame
    void Update () {
	
	}
}
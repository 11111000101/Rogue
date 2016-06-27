using UnityEngine;
using System.Collections;

public class ArmorOfLight : IArmor {

	// Use this for initialization
	void Start () {
        ap = 50;
        time = 5;
        armor = 5;

        // Defensive (F): Build up a shield negating all incoming dmg for 2s and healing you for every hit for 10HP
    }

    // Update is called once per frame
    void Update () {
	
	}
}

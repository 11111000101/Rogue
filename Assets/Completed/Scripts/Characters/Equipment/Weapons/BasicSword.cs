using UnityEngine;
using System.Collections;

public class BasicSword : IWeapon {

	// Use this for initialization
	void Start () {
        AP1 = 10;
        AP2 = -1;
        time1 = 1;
        time2 = -1;
        dmg = 5;
        dmgEffect = -1;
        range = 0;
        aoe = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class FlameSword : IWeapon {

	// Use this for initialization
	void Start () {
        AP1 = 10;
        AP2 = 50;
        time1 = 1;
        time2 = 1;
        dmg = 10;
        dmgEffect = 15;
        range = 0;
        aoe = 1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

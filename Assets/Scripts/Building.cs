using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Unit {

    // Use this for initialization
    protected override void Start () {
        base.Start();        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // The Game calls this when an action is requested at a specific location
    public override void doAction(Vector3 loc)
    {

    }

    // The game calls this when an action is requested at a specific unit
    public override void doAction(Unit unit)
    {

    }
}

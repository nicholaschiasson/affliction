using UnityEngine;

public class BloodCell : Microorganism {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();	
	}

    // The Game calls this when an action is requested at a specific location
    public override void doAction(Vector3 loc)
    {
        MoveTo(loc);
    }
}

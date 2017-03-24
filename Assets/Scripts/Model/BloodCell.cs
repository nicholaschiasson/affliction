using UnityEngine;

public abstract class BloodCell : Microorganism
{
	// The Game calls this when an action is requested at a specific location
	public override void doAction(Vector3 loc)
	{
		MoveTo(loc);
	}
}

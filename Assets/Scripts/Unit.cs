using UnityEngine;

public abstract class Unit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Override this in the implementation, the Game calls this when an action is requested at a specific location
    public abstract void doAction(Vector3 loc);
    
    // Override this implementaion, the game calls this when an action is requested at a specific unit
    public abstract void doAction(Unit unit);

    // Update is called once per frame
    void Update () {
		
	}
}

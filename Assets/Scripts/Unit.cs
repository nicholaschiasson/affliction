using UnityEngine;

public abstract class Unit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Override this in the implementation, the Game calls this when an action is requested at a specific location
    public void doAction(Vector3 loc)
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;

public abstract class Unit : MonoBehaviour {

	// Use this for initialization
	protected virtual void Start () {

    }

    // Override this in the implementation, the Game calls this when an action is requested at a specific location
    public abstract void doAction(Vector3 loc);
    
    // Override this implementaion, the game calls this when an action is requested at a specific unit
    public abstract void doAction(Unit unit);

    // Update is called once per frame
    void Update () {
		
	}


    //Mouse Handling
    protected virtual void handleLeftPress()
    {
        Debug.Log("Left Press");
    }

    protected virtual void handleRightPress()
    {
        Debug.Log("Right Press");
    }

    protected virtual void handleMiddlePress()
    {
        Debug.Log("Middle Press");
    }

    protected virtual void handleHover()
    {

    }


    protected virtual void handleExit()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) { // Left
            handleLeftPress();

        }
        else if (Input.GetMouseButtonDown(1)) { // Right
            handleRightPress();

        }
        else if (Input.GetMouseButtonDown(2)) { // Middle
            handleMiddlePress();
        }
        else{ // just hovering
            handleHover();
        }
             
    }

    private void OnMouseExit()
    {
        handleExit();
    }
}

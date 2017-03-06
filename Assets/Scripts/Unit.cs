using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	bool leftMouse = false;
	bool rightMouse = false;
	bool middleMouse = false;

	public int Health;

	// Override this in the implementation, the Game calls this when an action is requested at a specific location
	public abstract void doAction(Vector3 loc);

	// Override this implementaion, the game calls this when an action is requested at a specific unit
	public abstract void doAction(Unit unit);

	protected virtual void Start()
	{
	}

	//Mouse Handling
	private void OnMouseOver()
	{
		// Left Mouse Button
		if (Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnLeftMouseDown();
				leftMouse = true;
			}
			else if (leftMouse)
				OnLeftMouseHold();
		}
		else
		{
			if (leftMouse)
				OnLeftMouseClick();
			leftMouse = false;
		}

		// Right Mouse Button
		if (Input.GetMouseButton(1))
		{
			if (Input.GetMouseButtonDown(1))
			{
				OnRightMouseDown();
				rightMouse = true;
			}
			else if (rightMouse)
				OnRightMouseHold();
		}
		else
		{
			if (rightMouse)
				OnRightMouseClick();
			rightMouse = false;
		}

		// Middle Mouse Button
		if (Input.GetMouseButton(2))
		{
			if (Input.GetMouseButtonDown(2))
			{
				OnMiddleMouseDown();
				middleMouse = true;
			}
			else if (middleMouse)
				OnMiddleMouseHold();
		}
		else
		{
			if (middleMouse)
				OnMiddleMouseClick();
			middleMouse = false;
		}

		// Mouse Hover
		if (!leftMouse && !rightMouse && !middleMouse)
			OnMouseHover();
	}

	protected virtual void OnMouseEnter()
	{
		Debug.Log(this + ": Mouse enter");
	}


	protected virtual void OnMouseExit()
	{
		Debug.Log(this + ": Mouse exit");
		leftMouse = false;
		rightMouse = false;
		middleMouse = false;
	}

	protected virtual void OnLeftMouseDown()
	{
		Debug.Log(this + ": Left mouse down");
	}

	protected virtual void OnRightMouseDown()
	{
		Debug.Log(this + ": Right mouse down");
	}

	protected virtual void OnMiddleMouseDown()
	{
		Debug.Log(this + ": Middle mouse down");
	}

	protected virtual void OnLeftMouseHold()
	{
		//Debug.Log(this + ": Left mouse hold");
	}

	protected virtual void OnRightMouseHold()
	{
		//Debug.Log(this + ": Right mouse hold");
	}

	protected virtual void OnMiddleMouseHold()
	{
		//Debug.Log(this + ": Middle mouse hold");
	}

	protected virtual void OnLeftMouseClick()
	{
		Debug.Log(this + ": Left mouse click");
	}

	protected virtual void OnRightMouseClick()
	{
		Debug.Log(this + ": Right mouse click");
	}

	protected virtual void OnMiddleMouseClick()
	{
		Debug.Log(this + ": Middle mouse click");
	}

	protected virtual void OnMouseHover()
	{
		//Debug.Log(this + ": Mouse hover");
	}

	protected virtual void OnAttacked(UnitAttackedEventArgs e)
	{
		Health -= (int)e.Damage;
	}
}

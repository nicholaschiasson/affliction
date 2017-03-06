using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	bool leftMouse = false;
	bool rightMouse = false;
	bool middleMouse = false;

	protected Transform mainCamera = null;

	public int Health;

	void Awake()
	{
		mainCamera = Camera.main.transform;
	}

	// Override this in the implementation, the Game calls this when an action is requested at a specific location
	public abstract void doAction(Vector3 loc);

	// Override this implementaion, the game calls this when an action is requested at a specific unit
	public abstract void doAction(Unit unit);

	// Override this with selection functionality, called when the unit is clicked
	public abstract void select();

	// Called when unit is attacked by another unit
	protected virtual void OnAttacked(UnitAttackedEventArgs e)
	{
		Health -= (int)e.Damage;
	}

	//Mouse Handling
	protected virtual void OnMouseEnter() { }
	protected virtual void OnMouseExit() { }
	protected virtual void OnLeftMouseDown() { }
	protected virtual void OnRightMouseDown() { }
	protected virtual void OnMiddleMouseDown() { }
	protected virtual void OnLeftMouseHold() { }
	protected virtual void OnRightMouseHold() { }
	protected virtual void OnMiddleMouseHold() { }
	protected virtual void OnLeftMouseClick() { select(); }
	protected virtual void OnRightMouseClick() { }
	protected virtual void OnMiddleMouseClick() { }
	protected virtual void OnMouseHover() { }

	// Do not override: this method delegates the specific mouse events defined above
	void OnMouseOver()
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
			if (Input.GetMouseButtonUp(0) && leftMouse)
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
			if (Input.GetMouseButtonUp(1) && rightMouse)
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
			if (Input.GetMouseButtonUp(2) && middleMouse)
				OnMiddleMouseClick();
			middleMouse = false;
		}

		// Mouse Hover
		if (!leftMouse && !rightMouse && !middleMouse)
			OnMouseHover();
	}
}

using UnityEngine;

public class MouseButtonEventHandler
{
	public delegate void MouseButtonEventCallback();

	public event MouseButtonEventCallback OnLeftMouseDown;
	public event MouseButtonEventCallback OnRightMouseDown;
	public event MouseButtonEventCallback OnMiddleMouseDown;
	public event MouseButtonEventCallback OnLeftMouseHold;
	public event MouseButtonEventCallback OnRightMouseHold;
	public event MouseButtonEventCallback OnMiddleMouseHold;
	public event MouseButtonEventCallback OnLeftMouseClick;
	public event MouseButtonEventCallback OnRightMouseClick;
	public event MouseButtonEventCallback OnMiddleMouseClick;
	public event MouseButtonEventCallback OnMouseHover;

	bool leftMouse;
	bool rightMouse;
	bool middleMouse;

	public void OnMouseOver()
	{
		// Left Mouse Button
		if (Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (OnLeftMouseDown != null)
					OnLeftMouseDown();
				leftMouse = true;
			}
			else if (leftMouse && OnLeftMouseHold != null)
				OnLeftMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(0) && leftMouse && OnLeftMouseClick != null)
				OnLeftMouseClick();
			leftMouse = false;
		}

		// Right Mouse Button
		if (Input.GetMouseButton(1))
		{
			if (Input.GetMouseButtonDown(1))
			{
				if (OnRightMouseDown != null)
					OnRightMouseDown();
				rightMouse = true;
			}
			else if (rightMouse && OnRightMouseHold != null)
				OnRightMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(1) && rightMouse && OnRightMouseClick != null)
				OnRightMouseClick();
			rightMouse = false;
		}

		// Middle Mouse Button
		if (Input.GetMouseButton(2))
		{
			if (Input.GetMouseButtonDown(2))
			{
				if (OnMiddleMouseDown != null)
					OnMiddleMouseDown();
				middleMouse = true;
			}
			else if (middleMouse && OnMiddleMouseHold != null)
				OnMiddleMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(2) && middleMouse && OnMiddleMouseClick != null)
				OnMiddleMouseClick();
			middleMouse = false;
		}

		// Mouse Hover
		if (!leftMouse && !rightMouse && !middleMouse && OnMouseHover != null)
			OnMouseHover();
	}
}
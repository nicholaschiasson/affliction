﻿using UnityEngine;

public class SelectionBox : MonoBehaviour
{
	HUD hud;
	const float MIN_BOX_DIMENSION = 20.0f;
	Vector3 p1 = Vector3.zero;
	Vector2 p2 = Vector2.zero;
	Camera mainCamera;
	GameController gameController;
	bool mouseDown;
	bool mouseOverGUI;

	void Awake()
	{
		mainCamera = Camera.main;
		gameController = mainCamera.GetComponent<GameController>();
		hud = GetComponent<HUD>();
	}

	void OnEnable()
	{
		if (hud != null)
		{
			hud.OnMouseOverGUI += OnMouseOverGUI;
			hud.OnMouseOutsideGUI += OnMouseOutsideGUI;
		}
	}

	void Disable()
	{
		if (hud != null)
		{
			hud.OnMouseOverGUI -= OnMouseOverGUI;
			hud.OnMouseOutsideGUI -= OnMouseOutsideGUI;
		}
	}

	void Update()
	{
		Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0) && !mouseOverGUI)
		{
			mouseDown = true;
			p1 = ScreenToWorldPoint(mousePos);
		}
		if (Input.GetMouseButton(0) && mouseDown)
		{
			p2 = new Vector2(mousePos.x, mousePos.y);
		}
		if (Input.GetMouseButtonUp(0) && mouseDown)
		{
			// For now, selection box must be MIN_BOX_DIMENSION pixels in either width or height to register
			Vector2 rectSize = mousePos - mainCamera.WorldToScreenPoint(p1);
			if (Mathf.Abs(rectSize.x) >= MIN_BOX_DIMENSION || Mathf.Abs(rectSize.y) >= MIN_BOX_DIMENSION)
			{
				var p = ScreenToWorldPoint(mousePos);
				Vector3 size = p - p1;
				var boundsRect = new Rect(Mathf.Min(p1.x, p.x), Mathf.Min(p1.z, p.z), Mathf.Abs(size.x), Mathf.Abs(size.z));
				var bounds = new Bounds(new Vector3(boundsRect.center.x, 0, boundsRect.center.y), new Vector3(boundsRect.width, float.PositiveInfinity, boundsRect.height));
				gameController.handleSelectionBox(bounds);
			}
			p1 = Vector3.zero;
			p2 = Vector2.zero;
			mouseDown = false;
		}
	}

	void OnGUI()
	{
		if (p1 != Vector3.zero || p2 != Vector2.zero)
		{
			var p = mainCamera.WorldToScreenPoint(p1);
			var drawRect = new Rect(p.x, Screen.height - p.y, p2.x - p.x, -(p2.y - p.y));
			DrawQuad(drawRect, new Color(1.0f, 1.0f, 1.0f, 0.5f));
		}
	}

	Vector3 ScreenToWorldPoint(Vector3 screenPoint)
	{
		// Default output point to Vector3.Zero
		// Get point at intersection with XZ 0 plane
		// Try to get point at intersection with any real object in scene
		// Return point
		var plane = new Plane(Vector3.up, Vector3.zero);
		var ray = mainCamera.ScreenPointToRay(screenPoint);
		Vector3 point = Vector3.zero;
		float distance;
		RaycastHit hit;
		if (plane.Raycast(ray, out distance))
			point = ray.GetPoint(distance);
		if (Physics.Raycast(ray, out hit))
			point = hit.point;
		return point;
	}

	void DrawQuad(Rect position, Color color)
	{
		var texture = new Texture2D(1, 1);
		texture.SetPixel(0, 0, color);
		texture.Apply();
		GUI.DrawTexture(position, texture);
	}

	void OnMouseOverGUI()
	{
		mouseOverGUI = true;
	}

	void OnMouseOutsideGUI()
	{
		mouseOverGUI = false;
	}
}

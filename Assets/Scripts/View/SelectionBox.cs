using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
	Vector3 p1 = Vector3.zero;
	Vector2 p2 = Vector2.zero;
	Camera mainCamera = null;

	public delegate void SelectionBoundsCheckAction(Bounds bounds, List<GameObject> boundedUnits);
	public static event SelectionBoundsCheckAction OnSelectionBoundsCheck;

	void Awake()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			p1 = ScreenToWorldPoint(mousePos);
		}
		if (Input.GetMouseButton(0))
		{
			p2 = new Vector2(mousePos.x, mousePos.y);
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 p = ScreenToWorldPoint(mousePos);
			Vector3 size = p - p1;
			Rect boundsRect = new Rect(Mathf.Min(p1.x, p.x), Mathf.Min(p1.z, p.z), Mathf.Abs(size.x), Mathf.Abs(size.z));
			Bounds bounds = new Bounds(new Vector3(boundsRect.center.x, 0, boundsRect.center.y), new Vector3(boundsRect.width, float.PositiveInfinity, boundsRect.height));
			List<GameObject> boundedUnits = new List<GameObject>();
			if (OnSelectionBoundsCheck != null)
				OnSelectionBoundsCheck(bounds, boundedUnits);
			mainCamera.SendMessage("selectUnits", new SelectUnitsEventArgs(boundedUnits, Input.GetKey(KeyCode.LeftShift)));
			p1 = Vector3.zero;
			p2 = Vector2.zero;
		}
	}

	void OnGUI()
	{
		if (p1 != Vector3.zero || p2 != Vector2.zero)
		{
			Vector3 p = mainCamera.WorldToScreenPoint(p1);
			Rect drawRect = new Rect(p.x, Screen.height - p.y, p2.x - p.x, -(p2.y - p.y));
			DrawQuad(drawRect, new Color(1.0f, 1.0f, 1.0f, 0.5f));
		}
	}

	Vector3 ScreenToWorldPoint(Vector3 screenPoint)
	{
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = mainCamera.ScreenPointToRay(screenPoint);
		Vector3 point = Vector3.zero;
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			point = ray.GetPoint(distance);
		}
		return point;
	}

	void DrawQuad(Rect position, Color color)
	{
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0, 0, color);
		texture.Apply();
		GUI.DrawTexture(position, texture);
	}
}

using UnityEngine;

public class SelectionBox : MonoBehaviour
{
	Camera mainCamera = null;
	Vector3 p1 = Vector3.zero;
	Vector2 p2 = Vector2.zero;

	void Awake()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			float distance;
			if (plane.Raycast(ray, out distance))
			{
				p1 = ray.GetPoint(distance);
			}
		}
		if (Input.GetMouseButton(0))
		{
			p2 = new Vector2(mousePos.x, mousePos.y);
		}
		if (Input.GetMouseButtonUp(0))
		{
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

	void DrawQuad(Rect position, Color color)
	{
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0, 0, color);
		texture.Apply();
		GUI.DrawTexture(position, texture);
	}
}

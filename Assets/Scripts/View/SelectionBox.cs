using UnityEngine;

public class SelectionBox : MonoBehaviour
{
	Transform mainCamera = null;
	Vector2 p1 = Vector2.zero;
	Vector2 p2 = Vector2.zero;
	Vector2 c = Vector2.zero;

	void Awake()
	{
		mainCamera = Camera.main.transform;
	}

	// TODO: Have selection box size appropriately update with camera movement
	void Update()
	{
		Vector3 mousePos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			p1 = new Vector2(mousePos.x, Screen.height - mousePos.y);
			c = new Vector2(mainCamera.position.x, mainCamera.position.z);
		}
		if (Input.GetMouseButton(0))
		{
			p2 = new Vector2(mousePos.x, Screen.height - mousePos.y);
		}
		if (Input.GetMouseButtonUp(0))
		{
			p1 = Vector2.zero;
			p2 = Vector2.zero;
			c = Vector2.zero;
		}
	}

	void OnGUI()
	{
		if (p1 != Vector2.zero || p2 != Vector2.zero)
		{
			Rect drawRect = new Rect(p1.x, p1.y, p2.x - p1.x, p2.y - p1.y);
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

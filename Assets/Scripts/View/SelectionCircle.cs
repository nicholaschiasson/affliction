using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
	Renderer modelRenderer;

	void Awake()
	{
		modelRenderer = GetComponentInChildren<Renderer>();
	}

	public void SetColor(Color color)
	{
		modelRenderer.material.color = new Color(color.r, color.g, color.b, modelRenderer.material.color.a);
	}
}

using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
	Renderer modelRenderer = null;

	void Awake()
	{
		modelRenderer = GetComponentInChildren<Renderer>();
	}

	public void SetColor(Color color)
	{
		modelRenderer.material.color = color;
	}
}

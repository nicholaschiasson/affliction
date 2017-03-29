using UnityEngine;

public class Environment : MonoBehaviour
{
	GameController gameController;
	MouseButtonEventHandler mouseHandler;

	void Awake()
	{
		gameController = Camera.main.GetComponent<GameController>();
		mouseHandler = new MouseButtonEventHandler();
	}

	void OnEnable()
	{
		mouseHandler.OnLeftMouseClick += OnLeftMouseClick;
		mouseHandler.OnRightMouseClick += OnRightMouseClick;
	}

	void OnDisable()
	{
		mouseHandler.OnLeftMouseClick -= OnLeftMouseClick;
		mouseHandler.OnRightMouseClick -= OnRightMouseClick;
	}

	void OnLeftMouseClick()
	{
		if (!Input.GetKey(KeyCode.LeftShift))
			gameController.selectUnit(null, false, true);
	}

	void OnRightMouseClick()
	{
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			gameController.doAction(hit.point);
		}
	}

	void OnMouseOver()
	{
		mouseHandler.OnMouseOver();
	}
}

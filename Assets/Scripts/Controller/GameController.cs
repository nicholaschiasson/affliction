using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	new Camera camera;
	HashSet<GameObject> selectedUnits;
	// Use this for initialization
	void Start()
	{
		camera = this.GetComponent<Camera>();
		selectedUnits = new HashSet<GameObject>();

	}

	// Add an organism to the selected list. We can have multiple organisms selected for batch commands
	public void selectUnit(SelectUnitEventArgs e)
	{
		if (!e.Append)
		{
			foreach (GameObject u in selectedUnits)
				u.SendMessage("Deselect");
			selectedUnits = new HashSet<GameObject>();
		}
		e.Unit.SendMessage("Select");
		selectedUnits.Add(e.Unit);
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	private void doAction(Vector3 loc)
	{
		foreach (GameObject u in selectedUnits)
			u.SendMessage("doAction", loc);
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	private void doAction(Unit unit)
	{
		foreach (GameObject u in selectedUnits)
			u.SendMessage("doAction", unit);
	}

	// Update is called once per frame
	void Update()
	{
		
		//todo implement raycasting to determine where a user clicked

		// var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
	}

	// Update called once per frame after every update
	void LateUpdate()
	{
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		camera.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue), Space.World);
		//Debug.Log("Camera Coords: " + camera.transform.position.ToString());
	}
}

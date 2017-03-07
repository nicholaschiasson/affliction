using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	new Camera camera;
	HashSet<Unit> selectedUnits;

	public delegate void SelectionBoundsCheckAction(Bounds bounds, List<Unit> boundedUnits);
	public static event SelectionBoundsCheckAction OnSelectionBoundsCheck;

	// Use this for initialization
	void Start()
	{
		camera = this.GetComponent<Camera>();
		selectedUnits = new HashSet<Unit>();
	}

	// Add a unit to the selected list. We can have multiple units selected for batch commands
	public void selectUnit(SelectUnitEventArgs e)
	{
		if (!e.Append)
		{
			foreach (Unit u in selectedUnits)
				u.Deselect();
			selectedUnits = new HashSet<Unit>();
		}
		e.Unit.Select();
		selectedUnits.Add(e.Unit);
	}

	// Add several units with a selection box to selected list
	public void selectUnits(SelectUnitsEventArgs e)
	{
		if (!e.Append)
		{
			foreach (Unit u in selectedUnits)
				u.Deselect();
			selectedUnits = new HashSet<Unit>();
		}
		// It is at this point that we can filter the units list before performing the actual selection
		// For example, if our selection box caught a few ally units and a few buildings:
		// In that case, we probably only want to actually select the units
		foreach (Unit u in e.Units)
			selectUnit(new SelectUnitEventArgs(u, true));
	}

	public void handleSelectionBox(Bounds bounds)
	{
		List<Unit> boundedUnits = new List<Unit>();
		if (OnSelectionBoundsCheck != null)
			OnSelectionBoundsCheck(bounds, boundedUnits);
		selectUnits(new SelectUnitsEventArgs(boundedUnits, Input.GetKey(KeyCode.LeftShift)));
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	public void doAction(Vector3 loc)
	{
		foreach (Unit u in selectedUnits)
			u.doAction(loc);
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	public void doAction(Unit unit)
	{
		foreach (Unit u in selectedUnits)
			u.doAction(unit);
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

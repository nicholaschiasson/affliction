﻿using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	new Camera camera;
	List<Microorganism> selectedUnits;
    Building selectedOrgan;
	// Use this for initialization
	void Start()
	{
		camera = this.GetComponent<Camera>();
		selectedUnits = new List<Microorganism>();
		selectedOrgan = null;

	}

	// Add an organism to the selected list. We can have multiple organisms selected for batch commands
	public void selectOrganism(SelectMicroorganismEventArgs e)
	{
		//deselect a building if we have it selected
		selectedOrgan = null;

		if (e.Reset)
		{
			selectedUnits = new List<Microorganism>();

		}
		selectedUnits.Add(e.Microorganism);
	}

    // We can only select One Building so we are always resetting the list
    public void selectOrgan(SelectBuildingEventArgs e)
    {
        selectedUnits = new List<Microorganism>();
		selectedOrgan = e.Building;
    }

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	private void doAction(Vector3 loc)
	{
		if (selectedOrgan)
		{
			selectedOrgan.doAction(loc);
		}
		else
		{
			foreach (Microorganism unit in selectedUnits)
			{
				unit.doAction(loc);
			}
		}
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	private void doAction(Unit unit)
	{
		if (selectedOrgan)
		{
			selectedOrgan.doAction(unit);
		}
		else
		{
			foreach (Microorganism u in selectedUnits)
			{
				u.doAction(unit);
			}
		}
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

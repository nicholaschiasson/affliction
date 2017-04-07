using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
	public delegate void SelectionBoundsCheckAction(Bounds bounds, List<Unit> boundedUnits);
	public static event SelectionBoundsCheckAction OnSelectionBoundsCheck;

	new Camera camera;
	HashSet<Unit> selectedUnits;
	Dictionary<string, Vector3> cameraWarpLocations;

	public HUD hud;

	void Awake()
	{
		cameraWarpLocations = new Dictionary<string, Vector3>();
	}

	void OnEnable()
	{
		if (hud)
		{
			hud.OnActionZeroButtonPressed += OnZPressed;
			hud.OnActionOneButtonPressed += OnOnePressed;
			hud.OnActionTwoButtonPressed += OnTwoPressed;
			hud.OnActionThreeButtonPressed += OnThreePressed;
			hud.OnActionFourButtonPressed += OnFourPressed;
			hud.OnActionFiveButtonPressed += OnFivePressed;
		}
	}

	void OnDisable()
	{
		if (hud)
		{
			hud.OnActionZeroButtonPressed -= OnZPressed;
			hud.OnActionOneButtonPressed -= OnOnePressed;
			hud.OnActionTwoButtonPressed -= OnTwoPressed;
			hud.OnActionThreeButtonPressed -= OnThreePressed;
			hud.OnActionFourButtonPressed -= OnFourPressed;
			hud.OnActionFiveButtonPressed -= OnFivePressed;
		}
	}

	// Use this for initialization
	void Start()
	{
		camera = GetComponent<Camera>();
		selectedUnits = new HashSet<Unit>();

        //Ingoring All Colliisons with objects of the same layer.
        int redBloodCellLayer = 10;
        int whiteBloodCellLayer = 11;
        int sporeLayer = 12;
        int pathogenLayer = 13;
        Physics.IgnoreLayerCollision(redBloodCellLayer, redBloodCellLayer);
        Physics.IgnoreLayerCollision(whiteBloodCellLayer, whiteBloodCellLayer);
        Physics.IgnoreLayerCollision(sporeLayer, sporeLayer);
        Physics.IgnoreLayerCollision(pathogenLayer, pathogenLayer);
    }

	// Add a unit to the selected list. We can have multiple units selected for batch commands
	public void selectUnit(Unit unit, bool append, bool single)
	{
		if (!append)
		{
			foreach (Unit u in selectedUnits)
				u.Deselect();
			selectedUnits = new HashSet<Unit>();
		}
		if (unit != null)
		{
			unit.Select();
			selectedUnits.Add(unit);
		}
		if (single && hud != null)
			hud.UpdateInfoPanel(selectedUnits);
	}

	// Add several units with a selection box to selected list
	public void selectUnits(List<Unit> units, bool append)
	{
		if (!append)
		{
			foreach (Unit u in selectedUnits)
				u.Deselect();
			selectedUnits = new HashSet<Unit>();
		}
		// It is at this point that we can filter the units list before performing the actual selection
		// For example, if our selection box caught a few ally units and a few buildings:
		// In that case, we probably only want to actually select the units
		foreach (Unit u in units)
			selectUnit(u, true, false);
		if (hud != null)
			hud.UpdateInfoPanel(selectedUnits);
	}

	public void handleSelectionBox(Bounds bounds)
	{
		List<Unit> boundedUnits = new List<Unit>();
		if (OnSelectionBoundsCheck != null)
			OnSelectionBoundsCheck(bounds, boundedUnits);
		selectUnits(boundedUnits, Input.GetKey(KeyCode.LeftShift));
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	public void doAction(Vector3 loc)
	{
		foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.doAction(loc);
	}

	// Sending the action command to the selected lists and the location to which the action needs to be executed
	public void doAction(Unit unit)
	{
		foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.doAction(unit);
	}

	public void OnOnePressed()
	{
		foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnOnePressed();
	}

	public void OnTwoPressed()
	{
		foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnTwoPressed();
	}

    public void OnThreePressed()
    {
        foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnThreePressed();
    }

    public void OnFourPressed()
    {
        foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnFourPressed();
    }

    public void OnFivePressed()
    {
        foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnFivePressed();
    }

    public void OnZPressed()
    {
        foreach (Unit u in selectedUnits)
            if (u.Affiliation == UnitAffiliation.Ally)
                u.OnZPressed();
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
			Application.Quit();

		//todo implement proper UI selection options instead of keybinds
		if (Input.GetKey(KeyCode.Space))
		{
			string warpTo = "";
			if (Input.GetKeyDown("1"))
				warpTo = "Heart";
			if (Input.GetKeyDown("2"))
				warpTo = "Brain";
			if (Input.GetKeyDown("3"))
				warpTo = "Lungs";
			if (Input.GetKeyDown("4"))
				warpTo = "Stomach";
			if (Input.GetKeyDown("5"))
				warpTo = "Left Kidney";
			if (Input.GetKeyDown("6"))
				warpTo = "Right Kidney";
			if (cameraWarpLocations != null && cameraWarpLocations.ContainsKey(warpTo))
				transform.position = new Vector3(cameraWarpLocations[warpTo].x, transform.position.y, cameraWarpLocations[warpTo].z - 10.0f);
		}
		else
		{
			if (Input.GetKeyUp("1"))
			{
				OnOnePressed();
			}
			else if (Input.GetKeyUp("2"))
			{
				OnTwoPressed();
			}
            else if (Input.GetKeyUp("3"))
            {
                OnThreePressed();
            }
            else if (Input.GetKeyUp("4"))
            {
                OnFourPressed();
            }
            else if (Input.GetKeyUp("5"))
            {
                OnFivePressed();
            }

            else if (Input.GetKeyUp("z"))
            {
                OnZPressed();
            }
        }
    }

	// Update called once per frame after every update
	void LateUpdate()
	{
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		camera.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue), Space.World);
		camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -130.0f, 130.0f),
		                                        camera.transform.position.y,
												Mathf.Clamp(camera.transform.position.z, -144.0f, 124.0f));
	}

	public void RegisterCameraWarpLocation(string locationName, Vector3 location)
	{
		if (cameraWarpLocations != null)
			cameraWarpLocations[locationName] = location;
	}
}

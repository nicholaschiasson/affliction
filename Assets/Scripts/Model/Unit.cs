using System.Collections.Generic;
using UnityEngine;

public enum UnitAffiliation
{
	None,
	Ally,
	Opponent
}

public abstract class Unit : MonoBehaviour
{
	protected GameController gameController = null;
	bool leftMouse = false;
	bool rightMouse = false;
	bool middleMouse = false;

    protected Rigidbody rb;

	protected GameObject selectionCircle = null;

	public UnitAffiliation Affiliation = UnitAffiliation.None;
	public int Health;

    protected virtual void Start()
    {

    }

	protected virtual void Awake()
	{
        rb = GetComponent<Rigidbody>();
		gameController = Camera.main.GetComponent<GameController>();
		selectionCircle = Instantiate(Resources.Load(Util.Path.Combine("Prefabs", "SelectionCircle"))) as GameObject;
		selectionCircle.transform.parent = transform;
		selectionCircle.transform.position = transform.position;
		selectionCircle.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
		if (Affiliation == UnitAffiliation.Ally)
			selectionCircle.SendMessage("SetColor", Color.green);
		else if (Affiliation == UnitAffiliation.Opponent)
			selectionCircle.SendMessage("SetColor", Color.red);
		else
			selectionCircle.SendMessage("SetColor", Color.gray);
		selectionCircle.SetActive(false);
	}

    protected virtual void Update()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

    }

    protected virtual void OnCollisionStay(Collision collision)
    {

    }


    void OnEnable()
	{
		GameController.OnSelectionBoundsCheck += SelectionBoundsCheck;
	}

	void OnDisable()
	{
		GameController.OnSelectionBoundsCheck += SelectionBoundsCheck;
	}

	// Override this in the implementation, the Game calls this when an action is requested at a specific location
	public virtual void doAction(Vector3 loc) { }

	// Override this implementaion, the game calls this when an action is requested at a specific unit
	public virtual void doAction(Unit unit) { }

	protected virtual void requestSelect()
	{
		gameController.selectUnit(this, Input.GetKey(KeyCode.LeftShift));
	}

	// Called when a unit takes damage
	public virtual void takeDamage(int damage)
	{
		Health -= damage;
	}

	// Called when the controller registers the unit as selected
	public virtual void Select()
    {
		selectionCircle.SetActive(true);
	}

	// Called when the controller registers the unit as deselected
	public virtual void Deselect()
	{
		selectionCircle.SetActive(false);
	}

	protected virtual void SelectionBoundsCheck(Bounds bounds, List<Unit> boundedUnits)
	{
		Collider c = GetComponent<Collider>();
		if (c && bounds.Intersects(c.bounds))
			boundedUnits.Add(this);
	}

	//Mouse Handling
	protected virtual void OnMouseEnter() { }
	protected virtual void OnMouseExit() { }
	protected virtual void OnLeftMouseDown() { }
	protected virtual void OnRightMouseDown() { }
	protected virtual void OnMiddleMouseDown() { }
	protected virtual void OnLeftMouseHold() { }
	protected virtual void OnRightMouseHold() { }
	protected virtual void OnMiddleMouseHold() { }


    protected virtual void OnLeftMouseClick() {
        requestSelect();
    }

    protected virtual void OnRightMouseClick() {
        gameController.doAction(this);
    }

	protected virtual void OnMiddleMouseClick() { }
	protected virtual void OnMouseHover() { }

    // todo remove these when implementing proper User interface
    public virtual void OnOnePressed() { }
    public virtual void OnTwoPressed() { }

    // Do not override: this method delegates the specific mouse events defined above
    void OnMouseOver()
	{
		// Left Mouse Button
		if (Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnLeftMouseDown();
				leftMouse = true;
			}
			else if (leftMouse)
				OnLeftMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(0) && leftMouse)
				OnLeftMouseClick();
			leftMouse = false;
		}

		// Right Mouse Button
		if (Input.GetMouseButton(1))
		{
			if (Input.GetMouseButtonDown(1))
			{
				OnRightMouseDown();
				rightMouse = true;
			}
			else if (rightMouse)
				OnRightMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(1) && rightMouse)
				OnRightMouseClick();
			rightMouse = false;
		}

		// Middle Mouse Button
		if (Input.GetMouseButton(2))
		{
			if (Input.GetMouseButtonDown(2))
			{
				OnMiddleMouseDown();
				middleMouse = true;
			}
			else if (middleMouse)
				OnMiddleMouseHold();
		}
		else
		{
			if (Input.GetMouseButtonUp(2) && middleMouse)
				OnMiddleMouseClick();
			middleMouse = false;
		}

		// Mouse Hover
		if (!leftMouse && !rightMouse && !middleMouse)
			OnMouseHover();
	}
}

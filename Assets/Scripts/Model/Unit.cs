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
	protected GameController gameController;
    
    protected Rigidbody rb;

	protected GameObject selectionCircle;
    protected ParticleSystem[] particleSystems;

    ParticleSystem damageExplosion;

    public UnitAffiliation Affiliation = UnitAffiliation.None;

    public int level;
    public int Health;
    protected int maxHealth;
    protected int baseHealth;

	MouseButtonEventHandler mouseHandler;

	protected virtual void Awake()
	{
        // Getting our particle systems.
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        damageExplosion = null;
        foreach(ParticleSystem particle in particleSystems)
        {
            if (particle.tag.Equals("DamageExplosion"))
            {
                damageExplosion = particle;
                damageExplosion.Stop();
            }
        }
        // Getting our rigidbody
        rb = GetComponent<Rigidbody>();

        // Getting a reference to our controller
		gameController = Camera.main.GetComponent<GameController>();
		mouseHandler = new MouseButtonEventHandler();

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

        baseHealth = maxHealth = Health;
	}

    protected virtual void Update()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
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


    protected virtual void OnEnable()
	{
		GameController.OnSelectionBoundsCheck += SelectionBoundsCheck;
		if (mouseHandler != null)
		{
			mouseHandler.OnLeftMouseDown += OnLeftMouseDown;
			mouseHandler.OnRightMouseDown += OnRightMouseDown;
			mouseHandler.OnMiddleMouseDown += OnMiddleMouseDown;
			mouseHandler.OnLeftMouseHold += OnLeftMouseHold;
			mouseHandler.OnRightMouseHold += OnRightMouseHold;
			mouseHandler.OnMiddleMouseHold += OnMiddleMouseHold;
			mouseHandler.OnLeftMouseClick += OnLeftMouseClick;
			mouseHandler.OnRightMouseClick += OnRightMouseClick;
			mouseHandler.OnMiddleMouseClick += OnMiddleMouseClick;
			mouseHandler.OnMouseHover += OnMouseHover;
		}
	}

	protected virtual void OnDisable()
	{
		GameController.OnSelectionBoundsCheck -= SelectionBoundsCheck;
		if (mouseHandler != null)
		{
			mouseHandler.OnLeftMouseDown -= OnLeftMouseDown;
			mouseHandler.OnRightMouseDown -= OnRightMouseDown;
			mouseHandler.OnMiddleMouseDown -= OnMiddleMouseDown;
			mouseHandler.OnLeftMouseHold -= OnLeftMouseHold;
			mouseHandler.OnRightMouseHold -= OnRightMouseHold;
			mouseHandler.OnMiddleMouseHold -= OnMiddleMouseHold;
			mouseHandler.OnLeftMouseClick -= OnLeftMouseClick;
			mouseHandler.OnRightMouseClick -= OnRightMouseClick;
			mouseHandler.OnMiddleMouseClick -= OnMiddleMouseClick;
			mouseHandler.OnMouseHover -= OnMouseHover;
		}
	}

	// Override this in the implementation, the Game calls this when an action is requested at a specific location
	public virtual void doAction(Vector3 loc) { }

	// Override this implementaion, the game calls this when an action is requested at a specific unit
	public virtual void doAction(Unit unit) { }

	protected virtual void RequestSelect()
	{
		gameController.selectUnit(this, Input.GetKey(KeyCode.LeftShift), true);
	}

	// Called when a unit takes damage
	public virtual void takeDamage(int damage)
	{
		Health -= damage;

        //Playing damage animation
        if(damageExplosion && !damageExplosion.isPlaying)
        {
            damageExplosion.Play();
        }
	}

	public virtual bool canLevelUp()
	{
		return false;
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
		var c = GetComponent<Collider>();
		if (c && bounds.Intersects(c.bounds))
			boundedUnits.Add(this);
	}

	public virtual string GetTypeName()
	{
		return name;
	}

	public virtual string GetStatsInfo()
	{
		return ("Level: " + level + "\nHP: " + Health);
	}

    public int getLevel()
    {
        return level;
    }

    // Attempts to level up the unit, returns true if successful
    public virtual bool levelUp()
    {
        int oldMax = maxHealth;

        // Increasing our new max health by the base health * our level before level up
        maxHealth += baseHealth / 2 * level;

        // Healing ourselves by the amount of health we gained for the level.
        Health += maxHealth - oldMax;

        // Increase our level
        level++;

        return true;
    }

    // Calls level up n times
    public void setLevel(int levels)
    {
        // Calculating how many more level ups are needed to level up 
        int levelUpsNeeded = levels - level;
        while(levelUpsNeeded > 0)
        {
            levelUp();
            levelUpsNeeded--;
        }
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
        RequestSelect();
    }

    protected virtual void OnRightMouseClick() {
        gameController.doAction(this);
    }

	protected virtual void OnMiddleMouseClick() { }
	protected virtual void OnMouseHover() { }

    // todo remove these when implementing proper User interface
    public virtual void OnOnePressed() { }
    public virtual void OnTwoPressed() { }
    public virtual void OnThreePressed() { }
    public virtual void OnFourPressed() { }
    public virtual void OnFivePressed() { }
    public virtual void OnZPressed() { }

    // Do not override: this method delegates the specific mouse events defined above
    void OnMouseOver()
	{
		if (mouseHandler != null)
			mouseHandler.OnMouseOver();
	}
}

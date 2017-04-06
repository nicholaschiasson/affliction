public abstract class Organ : Unit
{
	//All organs have a store of oxygen that when gets to 0, dies.
	ResourceStore oxygenStore;
	ResourceStore proteinStore;

	public int consumeCost;
	public int basicUpgradeCost;
	int upgradesAvailable;
	protected override void Awake()
	{
		base.Awake();
		oxygenStore = new ResourceStore(Resource.Oxygen, 10000);
		proteinStore = new ResourceStore(Resource.Protein);
		upgradesAvailable = 0;
	}

	protected void Start()
	{
		gameController.RegisterCameraWarpLocation(GetTypeName(), transform.position);
	}

	public override string GetStatsInfo()
	{
		string stats = base.GetStatsInfo();
		stats += "\n" + oxygenStore.getType().ToString() + " Stored: " + getOxygenLevels();
		stats += "\n" + proteinStore.getType().ToString() + " Stored: " + getProteinLevels();
		return stats;
	}

	// Keybind to upgrade
	public override void OnZPressed()
	{
		levelUp();
	}

	protected virtual void consumeOxygen(int cost)
	{
		ResourceStore consumed = takeOxygen(consumeCost);
		// If there is not enough oxygen left, consume health
		if (consumed == null)
		{
			Health--;
		}
	}

	protected override void Update()
	{
		base.Update();

		// Update our oxygen intake for surviving
		consumeOxygen(consumeCost);
	}

	//Get the basic upgrade cost
	public int getBasicUpgradeCost()
	{
		return basicUpgradeCost;
	}

	//Get the OxygenLevels
	public int getOxygenLevels()
	{
		return oxygenStore.getValue();
	}

	//Get the Protein levels
	public int getProteinLevels()
	{
		return proteinStore.getValue();
	}

	public ResourceStore takeOxygen(int cost)
	{
		if (oxygenStore.getValue() >= cost)
		{
			return oxygenStore.takeOut(cost);
		}
		return null;
	}

	public ResourceStore takeProtein(int cost)
	{
		if (proteinStore.getValue() >= cost)
		{
			return proteinStore.takeOut(cost);
		}
		return null;
	}

	public void addUpgrade()
	{
		upgradesAvailable++;
	}

	public override bool canLevelUp()
	{
		return upgradesAvailable > 0;
	}

	public override bool levelUp()
	{
		if (canLevelUp() && base.levelUp() && takeProtein(basicUpgradeCost * level) != null)
		{
			// Increase the Oxygen consumption cost every second level
			consumeCost = level % 2 == 0 ? consumeCost : consumeCost + 1;

			// Consume the upgrade
			upgradesAvailable--;
			return true;
		}
		return false;
	}

	//Deliver a resouce to the organ
	public virtual void deliver(ResourceStore deposit)
	{
		switch (deposit.getType())
		{
			case Resource.Oxygen:
				oxygenStore += deposit;
				break;
			case Resource.Protein:
				proteinStore += deposit;
				break;
		}
	}
}

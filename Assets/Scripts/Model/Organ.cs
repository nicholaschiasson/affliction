public abstract class Organ : Unit
{
	//All organs have a store of oxygen that when gets to 0, dies.
	ResourceStore oxygenStore;
	ResourceStore proteinStore;

	public int consumeCost;

	protected override void Awake()
	{
		base.Awake();
		oxygenStore = new ResourceStore(Resource.Oxygen, 1000);
		proteinStore = new ResourceStore(Resource.Protein);
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

	protected void consumeOxygen(int cost)
	{
		ResourceStore consumed = oxygenStore.takeOut(consumeCost);
		// If there is not enough oxygen left, consume health
		if (consumed.getValue() != consumeCost)
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

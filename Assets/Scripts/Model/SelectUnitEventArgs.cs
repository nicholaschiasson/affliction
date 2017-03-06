public class SelectUnitEventArgs
{
	public Unit Unit { get; private set; }
	public bool Reset { get; private set; }

	public SelectUnitEventArgs(Unit unit, bool reset)
	{
		Reset = reset;
	}
}

public class SelectMicroorganismEventArgs : SelectUnitEventArgs
{
	public Microorganism Microorganism
	{
		get
		{
			return Unit as Microorganism;
		}
	}

	public SelectMicroorganismEventArgs(Microorganism unit, bool reset) : base(unit, reset) { }
}

public class SelectBuildingEventArgs : SelectUnitEventArgs
{
	public Building Building
	{
		get
		{
			return Unit as Building;
		}
	}

	public SelectBuildingEventArgs(Building unit, bool reset) : base(unit, reset) { }
}
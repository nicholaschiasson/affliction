using System.Collections.Generic;
using UnityEngine;

public class SelectUnitEventArgs
{
	public Unit Unit { get; private set; }
	public bool Append { get; private set; }

	public SelectUnitEventArgs(Unit unit, bool append)
	{
		Unit = unit;
		Append = append;
	}
}

public class SelectUnitsEventArgs
{
	public List<Unit> Units { get; private set; }
	public bool Append { get; private set; }

	public SelectUnitsEventArgs(List<Unit> units, bool append)
	{
		Units = units;
		Append = append;
	}
}
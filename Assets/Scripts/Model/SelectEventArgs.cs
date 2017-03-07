using System.Collections.Generic;
using UnityEngine;

public class SelectUnitEventArgs
{
	public GameObject Unit { get; private set; }
	public bool Append { get; private set; }

	public SelectUnitEventArgs(GameObject unit, bool append)
	{
		Unit = unit;
		Append = append;
	}
}

public class SelectUnitsEventArgs
{
	public List<GameObject> Units { get; private set; }
	public bool Append { get; private set; }

	public SelectUnitsEventArgs(List<GameObject> units, bool append)
	{
		Units = units;
		Append = append;
	}
}
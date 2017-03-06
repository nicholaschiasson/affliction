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
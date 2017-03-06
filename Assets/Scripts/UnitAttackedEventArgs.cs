using UnityEngine;

public class UnitAttackedEventArgs
{
	public GameObject Attacker { get; private set; }
	public uint Damage { get; private set; }

	public UnitAttackedEventArgs(GameObject attacker, uint damage)
	{
		Attacker = attacker;
		Damage = damage;
	}
}

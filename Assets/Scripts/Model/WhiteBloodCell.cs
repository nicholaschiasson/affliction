public class WhiteBloodCell : BloodCell {

    public int damage;

	public override string GetTypeName()
	{
		return "White Blood Cell";
	}

	public override string GetStatsInfo()
	{
		string stats = base.GetStatsInfo();
		stats += "\nDamage: " + damage;
		return stats;
	}

    public override void doAction(Unit unit)
    {
        base.doAction(unit);

        //todo I hate using typeof, is there a better way to do this?
        //If we clicked on a miner 
        if (unit.Affiliation != UnitAffiliation.Ally)
        {
            commandQueue.Enqueue(new AttackCommand(unit, damage, true));
        }
    }
}

using UnityEngine;

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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        float radius = visionRange / 5;
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in hitCollider)
        {
            Unit unit = collider.GetComponent<Unit>();

            // Find an enemy, and attack first one seen
            if (unit != null && unit.Affiliation != this.Affiliation)
            {
                this.doAction(unit);
            }
        }
    }

    public override void doAction(Unit unit)
    {
        base.doAction(unit);

        //todo I hate using typeof, is there a better way to do this?
        //If we clicked on a miner 
        if (unit.Affiliation != this.Affiliation)
        {
            commandQueue.Enqueue(new AttackCommand(unit, damage, true));
        }
    }

    public override bool levelUp()
    {
        // Increasing our level
        if (base.levelUp())
        {
            //Increasing our damage
            damage++;
            return true;
        }

        return false;        
    }
}

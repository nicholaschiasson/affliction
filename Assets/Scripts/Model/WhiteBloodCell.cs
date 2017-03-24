public class WhiteBloodCell : BloodCell {

    public int damage;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

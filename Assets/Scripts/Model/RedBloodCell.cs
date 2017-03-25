public class RedBloodCell : BloodCell
{
    WorkCommand newCommand;
    ResourceStoreContainer rsContainer;

    protected override void Awake()
    {
        base.Awake();
        newCommand = null;
        rsContainer = new ResourceStoreContainer();
    }

	public override string GetTypeName()
	{
		return "Red Blood Cell";
	}

	public override string GetStatsInfo()
	{
		string stats = base.GetStatsInfo();
		ResourceStore rs = rsContainer.getResourceStore();
		if (rs != null)
			stats += "\nCarry: " + rs.getValue() + " " + rs.getType().ToString();
		else
			stats += "\nCarry: None";
		return stats;
	}

    public override void doAction(Unit unit)
    {
        base.doAction(unit);

        //todo I hate using typeof, is there a better way to do this?
        //If we clicked on a miner 
        if(unit.GetType() == typeof(Miner) && newCommand == null)
        {
            newCommand = new WorkCommand((Miner)unit, rsContainer, true);
        }
        //If we clicked on an organ and have a current workCommand
        else if (((unit.GetType()).IsSubclassOf(typeof(Organ)) || unit.GetType() == typeof(Miner)) 
            && unit.Affiliation == UnitAffiliation.Ally 
            && newCommand != null)
        {
            newCommand.setDepot((Organ)unit, rsContainer);
            //Adding the new built command to our command list
            commandQueue.Enqueue(newCommand);
            //clearing our current building command
            newCommand = null;
        }
    }
}

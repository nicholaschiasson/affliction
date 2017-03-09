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
        if(unit.GetType() == typeof(Miner) && newCommand == null)
        {
            newCommand = new WorkCommand((Miner)unit, rsContainer, true);
        }
        //If we clicked on an organ and have a current workCommand
        else if (((unit.GetType()).IsSubclassOf(typeof(Organ)) || unit.GetType() == typeof(Miner)) && newCommand != null)
        {
            newCommand.setDepot((Organ)unit, rsContainer);
            //Adding the new built command to our command list
            commandQueue.Enqueue(newCommand);
            //clearing our current building command
            newCommand = null;
        }
    }
}

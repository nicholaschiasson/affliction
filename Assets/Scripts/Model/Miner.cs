public class Miner : Organ
{
    public Resource resource;
    int baseYield;
    public int yield;

    protected override void Awake()
    {
        base.Awake();
        baseYield = yield;
    }

	public override string GetStatsInfo()
	{
		var stats = base.GetStatsInfo();
		stats += "\n" + resource.ToString() + " Yield: " + yield;
		return stats;
	}

    public Resource getResource()
    {
        return resource;
    }

    public ResourceStore extract()
    {
        //consume an oxygen point to extract
        consumeOxygen(1);
        return new ResourceStore(resource, yield);
    }

    public override bool levelUp()
    {
        if (base.levelUp())
        {
            // base level up increases our level, increase the yield by our previous level * the base yield
            yield += baseYield * level - 1;
            return true;
        }

        return false;

    }
}

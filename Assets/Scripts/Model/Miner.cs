public class Miner : Organ
{
    public Resource resource;
    public int yield;

	public override string GetStatsInfo()
	{
		var stats = base.GetStatsInfo();
		stats += "\n" + resource.ToString() + ": " + yield;
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
}

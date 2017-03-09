public class Miner : Organ
{
    public Resource resource;
    public int yield;

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

    public Resource getResource()
    {
        return resource;
    }

    public ResourceStore extract()
    {
        return new ResourceStore(resource, yield);
    }
}

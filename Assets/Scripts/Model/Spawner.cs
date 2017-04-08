using UnityEngine;
using UnityEngine.AI;

public class Spawner : Organ
{
    public GameObject[] spawnables;
    ResourceStore erythStore;
    public int [] cost; // array of costs corresponding to the spawnables index

    protected override void Awake()
    {
        base.Awake();
        erythStore = new ResourceStore(Resource.Eryth);
    }

	public override string GetStatsInfo()
	{
		string stats = base.GetStatsInfo();
		stats += "\n" + erythStore.getType().ToString() + " Stored: " + getStoreValue();
		return stats;
	}

    //Spawns the gameObject at the index 
    public void spawn(int index)
    {
        if (spawnables.Length != cost.Length){
            Debug.LogError("spawnables or cost array inproperly formatted");
            return;
        }
        if (index < 0 || index >= spawnables.Length)
        {
            Debug.LogError("invalid index");
            return;
        }

        //Check if we have enough Resoucces
        if (getStoreValue() < cost[index]) {
            Debug.LogWarning("Attempted to spawn item with insufficient resources");
            return;
        }

        //Consume the resources to spawn
        erythStore.takeOut(cost[index]);

		Vector3 randomPosition = Random.insideUnitSphere * transform.localScale.magnitude * 1.2f + transform.position;
		NavMeshHit hit;
		Vector3 newPos = new Vector3(transform.position.x,
									 transform.position.y,
									 transform.position.z - transform.localScale.z);
		if (NavMesh.SamplePosition(randomPosition, out hit, transform.localScale.magnitude * 1.2f, 1))
			newPos = hit.position;

        // Spawning the game object and setting the level to our level
        GameObject spawned = Instantiate(spawnables[index], newPos, this.transform.rotation);
        spawned.GetComponent<Unit>().setLevel(level);
    }

    //return the value of eryth stored.
    public int getStoreValue()
    {
        return erythStore.getValue();
    }

    //Temporary spawning mechanisms
    public override void OnOnePressed()
    {
        if (spawnables.Length > 0)
        {
            spawn(0);
        }
    }
    public override void OnTwoPressed()
    {
        if (spawnables.Length > 1)
        {
            spawn(1);
        }
    }

    public override void deliver(ResourceStore deposit)
    {
        base.deliver(deposit);
        if(deposit.getType() == Resource.Eryth)
        {
            erythStore += deposit;
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Organ
{
    public GameObject[] spawnables;
    ResourceStore erythStore;
    public int [] cost; // array of costs corresponding to the spawnables index

    protected override void Awake()
    {
        base.Awake();
        erythStore = new ResourceStore(Resource.Erythropoietin);
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

    //Spawns the gameObject at the index 
    void spawn(int index)
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

        Vector3 newPos = new Vector3(this.transform.position.x + this.transform.position.x * this.transform.localScale.x,
                                            this.transform.position.y,
                                            this.transform.position.z + this.transform.position.z * this.transform.localScale.z);
        Instantiate(spawnables[index], newPos, this.transform.rotation);
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
        if(deposit.getType() == Resource.Erythropoietin)
        {
            erythStore += deposit;
        }
    }
}

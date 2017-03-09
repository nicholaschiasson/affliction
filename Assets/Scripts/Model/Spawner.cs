using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Organ
{
    public GameObject[] spawnables;
    ResourceStore erythStore;

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

    void spawn(GameObject obj)
    {
        Vector3 newPos = new Vector3(this.transform.position.x + this.transform.position.x * this.transform.localScale.x,
                                            this.transform.position.y,
                                            this.transform.position.z + this.transform.position.z * this.transform.localScale.z);
        Instantiate(obj, newPos, this.transform.rotation);
    }

    //Temporary spawning mechanisms
    public override void OnOnePressed()
    {
        if (spawnables.Length > 0)
        {
            spawn(spawnables[0]);
        }
    }
    public override void OnTwoPressed()
    {
        if (spawnables.Length > 1)
        {
            spawn(spawnables[1]);
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

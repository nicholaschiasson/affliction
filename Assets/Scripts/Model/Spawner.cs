using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Organ
{
    public GameObject[] spawnables;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawn(GameObject obj)
    {
        Vector3 newPos = new Vector3(this.transform.position.x + this.transform.position.x * this.transform.localScale.x,
                                            this.transform.position.y,
                                            this.transform.position.z + this.transform.position.z * this.transform.localScale.z);
        Instantiate(obj, newPos, this.transform.rotation);
    }

    //Temporary spawning mechanisms
    protected override void OnLeftMouseDown()
    {
        if (spawnables.Length > 0)
        {
            spawn(spawnables[0]);
        }
    }
    protected override void  OnRightMouseDown()
    {
        if (spawnables.Length > 1)
        {
            spawn(spawnables[1]);
        }
    }
}

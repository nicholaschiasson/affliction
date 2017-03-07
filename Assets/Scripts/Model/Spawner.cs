using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Building
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

    protected override void  OnRightMouseDown()
    {
        if (spawnables.Length > 0)
        {
            Instantiate(spawnables[0], this.transform.position + this.transform.localScale, this.transform.rotation);
        }
    }
}

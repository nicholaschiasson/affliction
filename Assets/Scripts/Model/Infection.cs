using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : Spawner {
    protected override void Awake()
    {
        base.Awake();
    }

    // Overriding consume Oxygen to do nothing
    protected override void consumeOxygen(int cost)
    {
        return;
    }
}

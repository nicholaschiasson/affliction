using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Building {
    //All organs have a store of oxygen that when gets to 0, dies.
    ResourceStore oxygenStore;
    ResourceStore proteinStore;

    protected override void Awake()
    {
        base.Awake();
        Affiliation = UnitAffiliation.Ally;
        oxygenStore = new ResourceStore(Resource.Oxygen, 100);
        oxygenStore = new ResourceStore(Resource.Protein);
    }

    public virtual void deliver(ResourceStore deposit)
    {
        switch (deposit.getType())
        {
            case Resource.Oxygen:
                oxygenStore += deposit;
                break;
            case Resource.Protein:
                proteinStore += deposit;
                break;
        }
    }
}

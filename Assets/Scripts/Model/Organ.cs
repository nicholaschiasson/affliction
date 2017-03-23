using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Unit {
    //All organs have a store of oxygen that when gets to 0, dies.
    ResourceStore oxygenStore;
    ResourceStore proteinStore;

    public int consumeCost;

    //TODO REMOVE 
    public int oxygenLevel;

    protected override void Awake()
    {
        base.Awake();
        oxygenStore = new ResourceStore(Resource.Oxygen, 1000);
        proteinStore = new ResourceStore(Resource.Protein);
    }

    protected override void Update()
    {
        base.Update();

        // Update our oxygen intake
        ResourceStore consumed = oxygenStore.takeOut(consumeCost);
        oxygenLevel = oxygenStore.getValue();
        // If there is not enough oxygen left, consume health
        if(consumed.getValue() != consumeCost)
        {
            Health--;
        }
    }

    //Get the OxygenLevels
    public int getOxygenLevels()
    {
        return oxygenStore.getValue();
    }

    //Get the Protein levels
    public int getProteinLevels()
    {
        return proteinStore.getValue();
    }

    //Deliver a resouce to the organ
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

    //Notifying that this organ has been right clicked.
    protected override void OnRightMouseClick() {
        gameController.doAction(this);
    }
}

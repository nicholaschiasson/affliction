﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource {None, Erythropoietin, Oxygen, Protein };

public class ResourceStore
{
    Resource type;
    int value;
    public ResourceStore(Resource t, int val)
    {
        type = t;
        value = val;
    }

    public ResourceStore(Resource t)
    {
        type = t;
        value = 0;
    }

    public Resource getType()
    {
        return type;
    }

    public int getValue()
    {
        return value;
    }

    // Only Adds two of the same type, otherwise returns the lefthand side argument
    public static ResourceStore operator +(ResourceStore a, ResourceStore b)
    {
        ResourceStore newStore = new ResourceStore(a.type);
        newStore.add(a.value);
        if (a.type == b.type)
        {
            newStore.add(b.value);
        }

        return newStore;
    }

    // Add resources to the store
    public void add(int val)
    {
        value += val;
    }

    // Take some resources out of the store, 
    // Returns a new ResourceStore with the value, 0 if requested greater then had
    public ResourceStore takeOut(int val)
    {
        ResourceStore newStore = new global::ResourceStore(type);
        if (val <= value)
        {
            value -= val;
            newStore.add(val);
        }

        return newStore;
    }
}

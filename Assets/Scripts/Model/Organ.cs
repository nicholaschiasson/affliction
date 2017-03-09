using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : Building {
    protected override void Awake()
    {
        base.Awake();
        Affiliation = UnitAffiliation.Ally;
    }
}

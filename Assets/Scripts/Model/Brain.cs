using UnityEngine;

public class Brain : Organ
{
    public Organ[] organs;
    int baseCost = 1000;
    protected override void Awake()
    {
        base.Awake();
        
    }
}

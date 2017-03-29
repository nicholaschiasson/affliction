using UnityEngine;

public class Brain : Organ
{
    public Organ[] organs;
    int researched;
    protected override void Awake()
    {
        base.Awake();
        researched = 1;
    }

    void upgradeOrgan(int index)
    {
        // If we have a valid organ
        if(index >= 0 && index < organs.Length && organs[index] != null)
        {
            // If we have enough Resources
            ResourceStore consumed = takeProtein(researched * getBasicUpgradeCost());
            if(consumed != null)
            {
                organs[index].addUpgrade();
            }
        }
    }

    public Organ getOrgan(int index)
    {
        if(index >= 0 && index < organs.Length)
        {
            Debug.Log(organs[index]);
            return organs[index];
        }

        return null;
    }

    // Upgrade the first organ in the list
    public override void OnOnePressed()
    {
        upgradeOrgan(0);
    }

    // Upgrade the second organ in the list
    public override void OnTwoPressed()
    {
        upgradeOrgan(1);
    }

    // Upgrade the third organ in the list
    public override void OnThreePressed()
    {
        upgradeOrgan(2);
    }

    // Upgrade the fourth organ in the list
    public override void OnFourPressed()
    {
        upgradeOrgan(3);
    }

    // Upgrade the fith organ in the list
    public override void OnFivePressed()
    {
        upgradeOrgan(4);
    }
}

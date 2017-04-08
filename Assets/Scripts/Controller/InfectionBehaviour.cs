using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionBehaviour : MonoBehaviour {

    Spawner infection;
    int oldHealth;

    enum InfectionState {Dormant, Idle, Spread, Attack, Defend}
    InfectionState currentState;
    float timer;
    float state_timer;
    float cooldown;

    public int numUnitsAvail;
    int numUnitSpawned;

    int currentLevel;

    bool hasLeveledUp;

    const float DEFAULT_COOLDOWN = 5.0f;
    const float BASE_PREPARE_TIME = 2.0f;
    const float MATURE_TIME = 15.0f;
    const float STATE_SWITCH_TIME = 5.0f;

    private void Awake()
    {
        infection = GetComponent<Spawner>();
        oldHealth = infection.Health;
        setState(InfectionState.Dormant);
        numUnitsAvail = 0;
        currentLevel = 0;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        timer += Time.deltaTime;
        state_timer += Time.deltaTime;
        currentLevel = infection.getLevel();

        if (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }

        //Updating Health
        if(infection.Health != oldHealth && cooldown <= 0.0f)
        {
            setState(InfectionState.Defend);
        }

        switch (currentState)
        {
            // Initial state on creation, once timer has passed, levels up and moves to idle state
            case InfectionState.Dormant:
                handleDormant();
                break;
            case InfectionState.Idle: // Idle State is for preparing resources, and leveling up
                handleIdle();
                break;
            case InfectionState.Spread: // Spread State is for spawning spores
                handleSpread();
                break;
            case InfectionState.Attack: // Attack State is for spawning pathogens to attack
                handleAttack();
                break;
            case InfectionState.Defend: // Defend State is for spawning pathogens, to defend itself
                handleDefend();
                break;
            default:
                break;
        }
	}

    void setState(InfectionState newState)
    {
        currentState = newState;
        timer = 0.0f;
        state_timer = 0.0f;
        numUnitSpawned = 0;
        hasLeveledUp = false;
    }

    void handleDormant()
    {
        if (currentLevel == 0 && timer >= MATURE_TIME * BASE_PREPARE_TIME)
        {
            infection.addUpgrade();
            setState(InfectionState.Idle);
            return;
        }
    }

    void handleIdle()
    {
        if (timer > STATE_SWITCH_TIME * BASE_PREPARE_TIME)
        {
            setState(InfectionState.Spread);
        }
        else if (state_timer > BASE_PREPARE_TIME)
        {
            if (!hasLeveledUp) // Can Only Level Up Once Per Idle State
            {
                infection.levelUp();
                hasLeveledUp = true;
            }
            numUnitsAvail += currentLevel;
            state_timer = 0.0f;
        }
    }

    void handleSpread()
    {
        float unitsAllowed = currentLevel * currentLevel / 2.0f;
        if ((float)numUnitSpawned >= unitsAllowed || numUnitsAvail == 0)
        {
            setState(InfectionState.Attack);
        }
        else if (state_timer > BASE_PREPARE_TIME)
        {
            //Spawn a Spore
            infection.spawn(0);
            numUnitsAvail--;
            numUnitSpawned++;
            state_timer = 0.0f;
        }
    }

    void handleAttack()
    {
        float unitsAllowed = ((currentLevel * currentLevel) - 1) / 4.0f;
        if ((float)numUnitSpawned >= unitsAllowed || numUnitsAvail == 0)
        {
            setState(InfectionState.Idle);
        }
        else if (state_timer > BASE_PREPARE_TIME)
        {
            //Spawn a Spore
            infection.spawn(1);
            numUnitsAvail--;
            numUnitSpawned++;
            state_timer = 0.0f;
        }

    }

    void handleDefend()
    {
        float unitsAllowed = 2.0f * currentLevel;
        // Aditional to standard checks: only defend if our level is high enough
        if (currentLevel < 1 || numUnitSpawned >= unitsAllowed || numUnitsAvail == 0) 
        {            
            // Setting the cooldown to defend
            cooldown = DEFAULT_COOLDOWN;
            setState(InfectionState.Idle);
        }
        else if (state_timer > BASE_PREPARE_TIME)
        {
            //Spawn a Spore
            infection.spawn(1);
            numUnitsAvail--;
            numUnitSpawned++;
            state_timer = 0.0f;
        }
    }
}

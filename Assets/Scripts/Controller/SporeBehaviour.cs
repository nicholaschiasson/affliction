using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SporeBehaviour : MonoBehaviour {

    Spore spore;

    public enum SporeState {Dormant, Wander, Colonize}
    public SporeState currentState;

    const float BASE_CREATION_TIME = 5.0f;
    float timer;
    public float range;

    void Awake()
    {
        spore = GetComponent<Spore>();
        currentState = SporeState.Dormant;
        timer = 0.0f;
    }
    // Use this for initialization
    void Start () {
    }


    Vector3 newWanderLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        NavMeshHit navHit;


        randomDirection += transform.position;
        // If we do not find a position return the current position
        if (!NavMesh.SamplePosition(randomDirection, out navHit, range * 2, NavMesh.GetAreaFromName("Wawlkable")))
        {
            return transform.position;
        }
                
        return navHit.position;
    }

    private void FixedUpdate()
    {
        if (currentState == SporeState.Wander)
        {
            float radius = range / 2;
            float percent = Random.Range(0.0f, 1.0f);
            
            if(percent < 0.02)
            {
                Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider collider in hitCollider)
                {
                    Spawner spawner = collider.GetComponent<Spawner>();
                    Unit unit = collider.GetComponent<Unit>();

                    // Find an infection and then potentially go it
                    if (spawner != null && spawner.Affiliation == spore.Affiliation)
                    {
                        //spore.doAction(spawner);
                        //currentState = SporeState.Colonize;
                    }
                }
            }            
        }        
    }

    bool spawnInfection()
    {
        return false;
    }

	// Update is called once per frame
	void Update () {
        // Is the spore needing orders
        
        if (timer > BASE_CREATION_TIME && currentState == SporeState.Dormant)
        {
            currentState = SporeState.Wander;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        if (!spore.hasOrders())
        {
            if (currentState != SporeState.Colonize)
            {
                float percent = Random.Range(0.0f, 1.0f);
                bool infectionSpawned = false;
                Debug.Log(percent);
                if(percent < 0.2 && currentState == SporeState.Wander)
                {
                    currentState = SporeState.Colonize;
                    infectionSpawned = spawnInfection();
                }
                if (!infectionSpawned)
                {
                    spore.doAction(newWanderLocation());
                }                
            }
            else
            {
                // Have no more orders and were colonizing, Colonizing has been complete we can die.
                spore.takeDamage(spore.Health);
            }
        }
	}
}

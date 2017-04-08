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
    Bounds referenceBound;
    public GameObject spawnable;

    void Awake()
    {
        spore = GetComponent<Spore>();
        currentState = SporeState.Dormant;
        timer = 0.0f;

        GameObject infectionReference = GameObject.FindGameObjectWithTag("InfectionReference");
        Bounds referenceBound = infectionReference.GetComponentInChildren<Renderer>().bounds;
    }
    // Use this for initialization
    void Start () {
    }


    Vector3 newWanderLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spore.visionRange;
        NavMeshHit navHit;


        randomDirection += transform.position;
        // If we do not find a position return the current position
        if (!NavMesh.SamplePosition(randomDirection, out navHit, spore.visionRange * 2, NavMesh.GetAreaFromName("Wawlkable")))
        {
            return transform.position;
        }
                
        return navHit.position;
    }

    private void FixedUpdate()
    {
        if (currentState == SporeState.Wander)
        {
            float radius = spore.visionRange/ 2;
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
                        spore.doAction(spawner);
                        currentState = SporeState.Colonize;
                    }
                }
            }            
        }        
    }

    bool canSpawn()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, referenceBound.extents.x +1);

        bool organHit = false;
        foreach (Collider collider in hitCollider)
        {
            Organ organ= collider.GetComponent<Organ>();

            if(organ != null)
            {                
                organHit = true;
            }
        }
                
        return !organHit;
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
                bool spawnAvail = false;
                if (percent < 0.15 && currentState == SporeState.Wander)
                {                    
                    spawnAvail = canSpawn();
                    if (spawnAvail)
                    {
                        currentState = SporeState.Colonize;
                        var spwnbl = Instantiate(spawnable, this.transform.position, this.transform.rotation);
						spwnbl.name = spawnable.name;
                    } 
                    
                }
                if (!spawnAvail)
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

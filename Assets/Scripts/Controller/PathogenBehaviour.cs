using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathogenBehaviour : MonoBehaviour
{

    Pathogen pathogen;
    
    void Awake()
    {
        pathogen = GetComponent<Pathogen>();
    }
    // Use this for initialization
    void Start()
    {
    }


    Vector3 newWanderLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * pathogen.visionRange;
        NavMeshHit navHit;


        randomDirection += transform.position;
        // If we do not find a position return the current position
        if (!NavMesh.SamplePosition(randomDirection, out navHit, pathogen.visionRange * 2, NavMesh.GetAreaFromName("Wawlkable")))
        {
            return transform.position;
        }

        return navHit.position;
    }    

    // Update is called once per frame
    void Update()
    {
        if (!pathogen.hasOrders())
        {
            pathogen.doAction(newWanderLocation());         
        }
    }
}

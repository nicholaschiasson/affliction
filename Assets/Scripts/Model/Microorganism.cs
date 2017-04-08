using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Microorganism : Unit
{
    //public float speed;
    protected Queue<Command> commandQueue;
    ParticleSystem trail;
    ParticleSystem spawn;
	NavMeshAgent navAgent;
    public float visionRange;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        commandQueue = new Queue<Command>();

        trail = null;
        spawn = null;
		navAgent = GetComponent<NavMeshAgent>();

        foreach(ParticleSystem particle in particleSystems)
        {
            if (particle.tag.Equals("trail"))
            {
                trail = particle;
                trail.Stop();
            }

            if (particle.tag.Equals("Spawn"))
            {
                spawn = particle;
            }
        }        
    }

    //for movement and physics, called on timer instead of per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(commandQueue.Count > 0)
        {
            Vector3 currentPos = transform.position;
            Command command = commandQueue.Peek();
            Vector3 target = command.moveToLocation(currentPos);
            if (target != currentPos)
            {
				//todo move using addForce???
				//rb.MovePosition(Vector3.MoveTowards(transform.position, target, speed));
				if (navAgent != null && new Vector3(target.x, 0, target.z) != new Vector3(navAgent.destination.x, 0, navAgent.destination.z))
					navAgent.destination = target;
            }
            //else // stopped moving
            //{
            //    rb.velocity = Vector3.zero;
            //    rb.angularVelocity = Vector3.zero;
            //    rb.ResetInertiaTensor();           
            //}
        }
    }

	// Update is called once per frame
	protected override void Update()
	{
        base.Update();
        // Checking if our command needs to be removed
        if(commandQueue.Count > 0)
        {
            Command command = commandQueue.Peek();

            //Updating the trail particle
            if (trail)
            {
                Vector3 currentPos = transform.position;
                
                // Check to determine if object is needing to move to a location
                Vector3 target = command.moveToLocation(currentPos);

                if (target != currentPos)
                {
                    Transform trailTransform = trail.GetComponentInParent<Transform>();
                    trailTransform.LookAt(2 * transform.position - target);
                    if (!trail.isPlaying)
                    {
                        trail.Clear();
                        trail.Play();
                    }
                }
                else if (trail.isPlaying)
                {                    
                    trail.Stop();
                }        
            }

            if (command.isComplete())
            {
                commandQueue.Dequeue();
            }
        }
        else if (trail != null && trail.isPlaying) // Trail should not be playing as no action is being performed
        {
            trail.Stop();
        }
    }

    //Called on CollisionEnter
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (commandQueue.Count > 0)
        {
            Command command = commandQueue.Peek();
            command.onCollision((collision.gameObject.GetComponent<Unit>()));            
        }
    }

    //Called on every frame for every collision
    protected override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionEnter(collision);

        // Ignore colliding with the board....
        if(collision.gameObject.tag == "board")
        {
            return;
        }
        if (commandQueue.Count > 0)
        {
            Command command = commandQueue.Peek();
            command.onCollision((collision.gameObject.GetComponent<Unit>()));
        }
    }

    protected void MoveTo(Vector3 pos)
    {
        commandQueue.Clear();
        if(pos != transform.position)
        {
            commandQueue.Enqueue(new MoveCommand(new Vector3(pos.x, transform.position.y, pos.z)));
        }        
    }

    public bool hasOrders()
    {
        return commandQueue.Count > 0;
    }
}

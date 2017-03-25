using System.Collections.Generic;
using UnityEngine;

public abstract class Microorganism : Unit
{
    public float speed;
    protected Queue<Command> commandQueue;
    ParticleSystem trail;
    ParticleSystem spawn;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        commandQueue = new Queue<Command>();

        trail = null;
        spawn = null;

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
    
    Vector3 MovingTo(Command command, Vector3 currentPos)
    {
        Vector3 target = command.moveToLocation(currentPos);
        float dist = Vector3.Distance(target, currentPos);
        return dist > Command.MARGIN_OF_ERROR ? target : currentPos;
    }

    //for movement and physics, called on timer instead of per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(commandQueue.Count > 0)
        {
            Vector3 currentPos = transform.position;
            Command command = commandQueue.Peek();
            Vector3 target = MovingTo(command, currentPos);
            if(target != currentPos)
            {
                //todo move using addForce???
                rb.MovePosition(Vector3.MoveTowards(transform.position, target, speed));
            }
            else // stopped moving
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.ResetInertiaTensor();           
            }
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
                Vector3 target = MovingTo(command, currentPos);
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
        commandQueue.Enqueue(new MoveCommand(new Vector3(pos.x, transform.position.y, pos.z)));
        return;
    }
}

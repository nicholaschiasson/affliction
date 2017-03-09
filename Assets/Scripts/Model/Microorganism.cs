using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Microorganism : Unit
{
    public float speed;
    Queue<Command> commandQueue;
    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        commandQueue = new Queue<Command>();
    }
    protected virtual void Start()
	{
	}

    //for movement and physics, called on timer instead of per frame
    void FixedUpdate()
    {
        if(commandQueue.Count > 0)
        {
            Vector3 currentPos = transform.position;
            Command command = commandQueue.Peek();
            Vector3 target = command.moveToLocation(currentPos);
            if(currentPos != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
            }
        }
    }

	// Update is called once per frame
	void Update()
	{
        // Checking if our command needs to be removed
        if(commandQueue.Count > 0)
        {
            Command command = commandQueue.Peek();
            if (command.isComplete())
            {
                commandQueue.Dequeue();
            }
        }
	}

    protected void MoveTo(Vector3 pos)
    {
        commandQueue.Clear();
        commandQueue.Enqueue(new MoveCommand(new Vector3(pos.x, transform.position.y, pos.z)));
        
    }
}

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Command
{
	public const float MARGIN_OF_ERROR = 0.05f;
	public abstract class Action
	{
		protected Unit target;
		protected Vector3 targetLocation;
		protected bool complete;

		protected Action(Unit t)
		{
			target = t;
			targetLocation = Vector3.zero;
			complete = false;
		}

		protected Action(Vector3 t)
		{
			target = null;
			targetLocation = t;
		}

		public bool isComplete()
		{
			return complete;
		}

		public virtual Vector3 moveToLocation(Vector3 currentLocation)
		{
			return currentLocation;
		}

		public void resetAction()
		{
			complete = false;
		}

		//collision handler for an action, by default does nothing
		public virtual bool OnCollision(Unit t) { return false; }

	}

	public class MoveAction : Action
	{
		public MoveAction(Unit t) : base(t)
		{
		}

		public MoveAction(Vector3 t) : base(t)
		{
		}

		public override Vector3 moveToLocation(Vector3 currentLocation)
		{            
            Vector3 goalPosition = targetLocation;

            if (target != null)
			{
				goalPosition = target.transform.position;
			}

            // Removing Y Positions since our game has no vertical element
            complete = Vector3.Distance(new Vector3(targetLocation.x, 0, targetLocation.z), new Vector3(currentLocation.x, 0, currentLocation.z)) < MARGIN_OF_ERROR;
            if (complete)
            {
                goalPosition =  currentLocation;
            }
            return goalPosition;		
		}

		public override bool OnCollision(Unit t)
		{
			//we have collided with the object we are looking for
			if (t != null && target != null && t.GetInstanceID() == target.GetInstanceID())
			{
				complete = true;
				return true;
			}
			return false;
		}
	}

	public class ExtractAction : Action
	{
		ResourceStoreContainer workerResourceContainer;
		public ExtractAction(Miner m, ResourceStoreContainer rsc) : base(m)
		{
			workerResourceContainer = rsc;
		}

		public override bool OnCollision(Unit t)
		{   //we have found our target
			if (target != null && t.GetInstanceID() == target.GetInstanceID())
			{
				workerResourceContainer.containResource(((Miner)target).extract());
				complete = true;
				return true;
			}

			return false;
		}
	}

	public class DeliverAction : Action
	{
		ResourceStoreContainer workerResourceContainer;
		public DeliverAction(Organ o, ResourceStoreContainer rsc) : base(o)
		{
			workerResourceContainer = rsc;
		}

		public override bool OnCollision(Unit t)
		{   //we have found our target
			if (target != null && target != null && t.GetInstanceID() == target.GetInstanceID())
			{
				if (workerResourceContainer.getResourceStore() != null)
				{
					((Organ)target).deliver(workerResourceContainer.getResourceStore());
					complete = true;
				}
				return true;
			}
			return false;
		}
	}

    public class ColonizeAction: Action
    {
        public ColonizeAction(Spawner s): base(s)
        {
        }

        public ColonizeAction(Vector3 t): base(t)
        {
        }

        public override bool OnCollision(Unit t)
        {
            if (target != null && target != null && t.GetInstanceID() == target.GetInstanceID())
            {
                ((Spawner)target).addUpgrade();
                complete = true;
                return true;
            }
            return false;
        }
    }

	//todo attack action
	public class AttackAction : Action
	{
		int damage;
		public AttackAction(Unit u, int d) : base(u)
		{
			damage = d;
		}

		public override bool OnCollision(Unit t)
		{
			// If this is not our target, we do not want to do anything
			if (target == null || t == null || target.GetInstanceID() != t.GetInstanceID())
				return false;

			//If this is our target attack.
			t.takeDamage(damage);
			complete = true;
			return true;
		}

	}

	protected Queue<Action> actions;
	bool loop;

	public Command()
	{
		actions = new Queue<Action>();
		loop = false;
	}

	protected Command(bool l)
	{
		actions = new Queue<Action>();
		loop = l;
	}

	// returns when the command is complete, can be overriden by derived commands
	public virtual bool isComplete()
	{
		return actions.Count <= 0;
	}

	// Add a new target or return target, default does nothing
	public virtual void setTarget(Unit t) { }
	public virtual void setTarget(Vector3 t) { }
	public virtual void setReturn(Unit r) { }
	public virtual void setReturn(Vector3 r) { }

	public virtual Vector3 moveToLocation(Vector3 currentLocation)
	{
		Action currentAction;
		try
		{
			currentAction = actions.Peek();
		}
		catch (InvalidOperationException)
		{
			currentAction = null;
		}

		if (currentAction != null)
		{
			Vector3 targetLocation = currentAction.moveToLocation(currentLocation);

			//check our Queue
			updateActionQueue();
			return targetLocation;
		}
		else
		{
			return currentLocation;
		}

	}

	public virtual void onCollision(Unit t)
	{
		bool keepGoing;

        if (t == null)
        {
            return;
        }

        // todo fix this. work around. 1 collision event happens when objects collide but multiple consecutive actions might depend on collision
        // we keep going until an action is not listening for this collision, that way we don't accidently trigger a collison for an action later
        foreach (Action action in actions.ToList())
		{
			keepGoing = action.OnCollision(t);
			// Check our Queue
			updateActionQueue();
			if (!keepGoing)
			{
				break;
			}
		}
	}

	void updateActionQueue()
	{
		if (actions.Peek().isComplete())
		{
			Action popped = actions.Dequeue(); //remove it from the queue
			if (loop) //if we are looping this action add it on the end of the queue
			{
				popped.resetAction();
				actions.Enqueue(popped);
			}
		}
	}
}

public class MoveCommand : Command
{
	public MoveCommand(Unit t) : base()
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
	}

	public MoveCommand(Vector3 t) : base()
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
	}

	public MoveCommand(Unit t, Unit r, bool l) : base(l)
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
		m = new MoveAction(r);
		actions.Enqueue(m);
	}

	public MoveCommand(Unit t, Vector3 r, bool l) : base(l)
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
		m = new MoveAction(r);
		actions.Enqueue(m);
	}

	public MoveCommand(Vector3 t, Unit r, bool l) : base(l)
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
		m = new MoveAction(r);
		actions.Enqueue(m);
	}

	public MoveCommand(Vector3 t, Vector3 r, bool l) : base(l)
	{
		MoveAction m = new MoveAction(t);
		actions.Enqueue(m);
		m = new MoveAction(r);
		actions.Enqueue(m);
	}
}

public class WorkCommand : Command
{
	public WorkCommand(Miner m, ResourceStoreContainer wrorkerRSC, bool l) : base(l)
	{
		actions.Enqueue(new MoveAction(m));
		actions.Enqueue(new ExtractAction(m, wrorkerRSC));
	}

	public WorkCommand(Miner m, ResourceStoreContainer wrorkerRSC, Vector3 r, bool l) : base(l)
	{
		actions.Enqueue(new MoveAction(m));
		actions.Enqueue(new ExtractAction(m, wrorkerRSC));
		actions.Enqueue(new MoveAction(r));
	}

	public void setMine(Miner m, ResourceStoreContainer wrorkerRSC)
	{

	}
	public void setDepot(Organ r, ResourceStoreContainer wrorkerRSC)
	{
		actions.Enqueue(new MoveAction(r));
		actions.Enqueue(new DeliverAction(r, wrorkerRSC));
	}
	public override void setReturn(Vector3 r)
	{
		actions.Enqueue(new MoveAction(r));
	}
}

//todo attack command
public class AttackCommand : Command
{
	Unit target;
	//standard attack unit order
	public AttackCommand(Unit t, int damage, bool l) : base(l)
	{
		target = t;
		actions.Enqueue(new MoveAction(t));
		actions.Enqueue(new AttackAction(target, damage));
	}

	//attack unit order and return to location
	public AttackCommand(Unit target, Vector3 returnPoint, int damage, bool l) : base(l)
	{
		//todo implement this if needed
	}

	//attack unit order and return to GameObject
	public AttackCommand(Unit target, Unit returnObject, int damage, bool l) : base(l)
	{
		//todo implement this if needed
	}

	// Overriding isComplete, we monitor the health of our target, if it has died we have finished our actions
	public override bool isComplete()
	{
		return base.isComplete() || target.Health <= 0;
	}
}

public class ColonizeCommand : Command
{
    public ColonizeCommand(Spawner t): base(false)
    {
        actions.Enqueue(new MoveAction(t));
        actions.Enqueue(new ColonizeAction(t));
    }

    public ColonizeCommand(Vector3 t) : base(false)
    {

    }
}

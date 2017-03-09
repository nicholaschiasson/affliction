using System.Collections;
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

            complete = Vector3.Distance(goalPosition, currentLocation) < MARGIN_OF_ERROR;
            return goalPosition;
        }
    }

    public class ExtractAction : Action
    {
        public ExtractAction(Miner m) : base(m)
        {
        }
    }

    public class DeliverAction : Action
    {
        DeliverAction(Organ o) : base(o)
        {
        }
    }

    //todo attack action
    public class AttackAction : Action
    {
        AttackAction(Unit u) : base(u)
        {
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

    public bool isComplete()
    {
        return actions.Count <= 0;
    }

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
        
        if(currentAction != null)
        {
            Vector3 targetLocation = currentAction.moveToLocation(currentLocation);

            //if we have completed the action
            if (currentAction.isComplete())
            {
                actions.Dequeue(); //remove it from the queue
                if (loop) //if we are looping this action add it on the end of the queue
                {
                    actions.Enqueue(currentAction);
                }
            }

            return targetLocation;
        }
        else
        {
            return currentLocation;
        }
        
    }

    public virtual bool collidedWithTarget(GameObject target)
    {
        return false;
    }
}

public class MoveCommand: Command
{
    public MoveCommand(Unit t): base()
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
    }

    public MoveCommand(Vector3 t): base()
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
    }

    public MoveCommand(Unit t, Unit r, bool l): base(l)
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
        m = new MoveAction(r);
        actions.Enqueue(m);
    }

    public MoveCommand(Unit t, Vector3 r, bool l): base(l)
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
        m = new MoveAction(r);
        actions.Enqueue(m);
    }

    public MoveCommand(Vector3 t, Unit r, bool l ): base(l)
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
        m = new MoveAction(r);
        actions.Enqueue(m);
    }

    public MoveCommand(Vector3 t, Vector3 r, bool l): base(l)
    {
        MoveAction m = new MoveAction(t);
        actions.Enqueue(m);
        m = new MoveAction(r);
        actions.Enqueue(m);
    }
}

public class WorkCommand: Command
{

}

//todo attack command
public class AttackCommand : Command
{
    public override bool collidedWithTarget(GameObject target)
    {
        return false;
    }
}

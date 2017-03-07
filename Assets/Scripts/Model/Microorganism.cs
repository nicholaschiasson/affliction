using UnityEngine;

public class Microorganism : Unit
{
    Vector3 movePosition;
    public float speed;
	// Use this for initialization
	protected virtual void Start()
	{
        movePosition = transform.position;
	}

    //for movement and physics, called on timer instead of per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosition, speed);
    }

	// Update is called once per frame
	void Update()
	{

	}

    protected void MoveTo(Vector3 pos)
    {
        movePosition = new Vector3(pos.x, transform.position.y, pos.z);
    }
}

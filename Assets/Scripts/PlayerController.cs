using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0) desiredx = 5;
        else if (Input.GetAxis("Horizontal") < 0) desiredx = -5;
        else desiredx = 0;

        if (Input.GetButton("Jump") && grounded)
        {
            grounded = false;
            velocity.y = 6;
        }
    }

    public override void OnCollision(Collider2D other)
    {
        if (other.gameObject.CompareTag("lethal"))
        {
            transform.position = startpos;
            velocity = new Vector3(0, 0, 0);
        }
    }
}

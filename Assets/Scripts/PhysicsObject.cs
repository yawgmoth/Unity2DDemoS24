using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 velocity;
    public float desiredx;
    public bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void DoMove(Vector3 move, bool ishorizontal)
    {
        if (move.magnitude < 0.001f) return;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int n = GetComponent<Rigidbody2D>().Cast(move, hits, 0.2f);
        bool isblocked = false;
        for(int i = 0; i < n; ++i)
        {
            bool collision = false;
            if (ishorizontal && Mathf.Abs(hits[i].normal.x) > 0.5f)
            {
                isblocked = true;
                collision = true;
                OnCollisionHorizontal(hits[i].collider);
            }
            if (!ishorizontal && Mathf.Abs(hits[i].normal.y) > 0.5f)
            {
                if (hits[i].normal.y > 0.5f)
                    grounded = true;
                isblocked = true;
                collision = true;
                OnCollisionVertical(hits[i].collider);
            }
            if (collision)
            {
                OnCollision(hits[i].collider);
            }
        }
        if (!isblocked)
        { 
            transform.position += move;
        }
    }

    public virtual void OnCollision(Collider2D other)
    {

    }

    public virtual void OnCollisionHorizontal(Collider2D other)
    {

    }

    public virtual void OnCollisionVertical(Collider2D other)
    {

    }

    void FixedUpdate()
    {
        velocity += 9.81f * Vector3.down * Time.fixedDeltaTime;
        Vector3 movement = velocity * Time.fixedDeltaTime;
        movement.x = desiredx*Time.fixedDeltaTime;
        DoMove(new Vector3(movement.x, 0, 0), true);
        DoMove(new Vector3(0, movement.y, 0), false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 acceleration;
    public Vector3 displacement;
    public Vector3 velocity;
    float time;
    public float mass = 1;
    public Vector3 force;
    Vector3 gravity = new Vector3(0, -9.8f, 0);
    public float elasticity;

    void Start()
    {
        displacement = transform.position;
    }


    void FixedUpdate()
    {
        float tempo = Time.fixedDeltaTime;
        acceleration = force / mass; // + gravity;
        velocity += acceleration * tempo;
        displacement += tempo * velocity;
        transform.position = displacement;
        force = Vector3.zero;

        if (displacement.x < -4.33f)
        {
            velocity = velocity.magnitude * elasticity *
                    Refletir(velocity.normalized, Vector3.right);
        }

        if (displacement.x > 4.33f)
        {
            velocity = velocity.magnitude * elasticity *
                    Refletir(velocity.normalized, Vector3.left);
        }
        
        if (displacement.y > 15.58f)
        {
            velocity = velocity.magnitude * elasticity *
                    Refletir(velocity.normalized, Vector3.down);
        }

        if(displacement.y < -0.16f && coliding)
        {
            velocity = velocity.magnitude * elasticity *
                    Refletir(velocity.normalized, Vector3.up);
        }

        if (velocity.magnitude > 0.1f)
            transform.position = displacement;
    }
    public void AddForce(Vector3 f)
    {
        force = f;
    }
    public static Vector3 Refletir(Vector3 vector, Vector3 normal)
    {
        return vector - 2 * Vector3.Dot(vector, normal) * normal;
    }

    bool coliding = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            coliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            coliding = false;
        }
    }


    public void ReflectBallFromBrickColliding(Brick_Col.reflection_direction dir)
    {
        if(dir == Brick_Col.reflection_direction.Right) { velocity = velocity.magnitude * elasticity *Refletir(velocity.normalized, Vector3.right);}
        if(dir == Brick_Col.reflection_direction.Left)  { velocity = velocity.magnitude * elasticity * Refletir(velocity.normalized, Vector3.left); }
        if(dir == Brick_Col.reflection_direction.Up)    { velocity = velocity.magnitude * elasticity * Refletir(velocity.normalized, Vector3.up); }
        if(dir == Brick_Col.reflection_direction.Down)  { velocity = velocity.magnitude * elasticity * Refletir(velocity.normalized, Vector3.down); }
    }


}

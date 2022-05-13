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

    bool isPaused = true;
    bool hitOnLooseLine = false;

    public Transform resetPos;

    public bool isMetalBall = false;
    public GameObject normalBallSkin;
    public GameObject metalBallSkin;

   

    void Start()
    {
        displacement = transform.position;
    }

    public void UnPauseBall()
    {
        isPaused = false;
    }

    void FixedUpdate()
    {
        
        if (isPaused) return;


        float time = Time.fixedDeltaTime;
        acceleration = force / mass; // + gravity;
        velocity += acceleration * time; 
        displacement += time * velocity;
        transform.position = displacement;
        force = Vector3.zero;

        if (displacement.x < -4.33f)
        {
            velocity = velocity.magnitude * elasticity *
                    Reflect(velocity.normalized, Vector3.right);
            PlayBallSound();
        }

        if (displacement.x > 4.33f)
        {
            velocity = velocity.magnitude * elasticity *
                    Reflect(velocity.normalized, Vector3.left);
            PlayBallSound();
        }
        
        if (displacement.y > 15.58f)
        {
            velocity = velocity.magnitude * elasticity *
                    Reflect(velocity.normalized, Vector3.down);
            PlayBallSound();
        }

        if(displacement.y < -0.16f && coliding)
        {
            velocity = velocity.magnitude * elasticity *
                    Reflect(velocity.normalized, Vector3.up);

            PlayBallSound();

        }

        if (displacement.y < -4f)
        {
            if(hitOnLooseLine == false)
            {
                Pause_Ball(true);
                Game_Manager.Instance.Game_Over();
                hitOnLooseLine = true;
            }
        }

        if (velocity.magnitude > 0.1f)
            transform.position = displacement;
    }
    public void AddForce(Vector3 f)
    {
        force = f;
    }
    public static Vector3 Reflect(Vector3 vector, Vector3 normal)
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
        if(dir == Brick_Col.reflection_direction.Right) { velocity = velocity.magnitude * elasticity *Reflect(velocity.normalized, Vector3.right);}
        if(dir == Brick_Col.reflection_direction.Left)  { velocity = velocity.magnitude * elasticity * Reflect(velocity.normalized, Vector3.left); }
        if(dir == Brick_Col.reflection_direction.Up)    { velocity = velocity.magnitude * elasticity * Reflect(velocity.normalized, Vector3.up); }
        if(dir == Brick_Col.reflection_direction.Down)  { velocity = velocity.magnitude * elasticity * Reflect(velocity.normalized, Vector3.down); }

        PlayBallSound();
    }

    public void Pause_Ball(bool key)
    {
        isPaused = key;
    }

    public void ResetBallPos()
    {
        isMetalBall = false;
        metalBallSkin.SetActive(false);
        normalBallSkin.SetActive(true);

        hitOnLooseLine = true;
        Pause_Ball(true);
        velocity = Vector3.zero;
        transform.position = resetPos.transform.position;
        displacement = transform.position;

        Invoke("ReShootBall", 1f);
    }

    void ReShootBall()
    {
        hitOnLooseLine = false;
        Pause_Ball(false);

        float value = Random.Range(-150, 150);
        AddForce(new Vector3(value, 150, 0));
    }

    public void TrnsformToMetalBall()
    {
        isMetalBall = true;
        metalBallSkin.SetActive(true);
        normalBallSkin.SetActive(false);


    }


    void PlayBallSound()
    {
        if (isMetalBall) { Game_Manager.Instance.audio_manager.PlaySound(Game_Manager.Instance.audio_manager.metalball); }
        else { Game_Manager.Instance.audio_manager.PlayAnyPong(); }
    }

}

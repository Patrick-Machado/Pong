using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_Col : MonoBehaviour
{
    Brick master_brick;
    public reflection_direction reflect_to = reflection_direction.Right;
    void Start()
    {
        master_brick = transform.parent.GetComponent<Brick>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Ball>().ReflectBallFromBrickColliding(reflect_to);
            master_brick.Ball_Hit();
        }
    }

    public enum reflection_direction
    {
        Right, Left, Up, Down
    }

}

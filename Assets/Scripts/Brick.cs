using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public Brick_Type mBrick_Type = Brick.Brick_Type.common;
    Level_Bricks level_Bricks;
    public Material concrete_cracked_mat;

    void Start()
    {
        level_Bricks = GameObject.FindGameObjectWithTag("Level_Ctrl").GetComponent<Level_Bricks>();
    }

    bool notcallReduceTwice = false; int hitcount = 0; bool onDelay; Ball ball;
    public void Ball_Hit()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        if(mBrick_Type == Brick.Brick_Type.common)
        {
            if (!notcallReduceTwice) { level_Bricks.Reduce_A_Brick(); }
            
            notcallReduceTwice = true;
            DestroyBrick();
        }
        else if (mBrick_Type == Brick.Brick_Type.concrete) // neets two hits to be destroyed
        {
            if (!onDelay)
            {
                hitcount += 1;
                GetComponent<MeshRenderer>().material = concrete_cracked_mat;
                if (ball.isMetalBall) { hitcount += 1; }

                onDelay = true;
                Invoke("removeOnDelay", .8f);
            }
            if(hitcount > 1) // when the brick is hit twice
            {
                if (!notcallReduceTwice) { level_Bricks.Reduce_A_Brick(); }

                notcallReduceTwice = true;
                DestroyBrick();
            }
            
        }
        else if(mBrick_Type == Brick.Brick_Type.metal)// is indestructible so only bounces
        {

        }
    }

    void removeOnDelay()
    {
        onDelay = false;
    }

    public enum Brick_Type
    {
        common, metal, concrete
    }

    void DestroyBrick()
    {
        int powerUP_rand = Random.Range(1, 100);
        if(powerUP_rand < 45) { level_Bricks.InitPowerUP(transform.position); }

        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public Brick_Type mBrick_Type = Brick.Brick_Type.common;
    Level_Bricks level_Bricks;
    void Start()
    {
        level_Bricks = GameObject.FindGameObjectWithTag("Level_Ctrl").GetComponent<Level_Bricks>();
    }

    bool notcallReduceTwice = false; int hitcount = 0; bool onDelay;
    public void Ball_Hit()
    {
        if(mBrick_Type == Brick.Brick_Type.common)
        {
            if (!notcallReduceTwice) { level_Bricks.Reduce_A_Brick(); }
            
            notcallReduceTwice = true;
            Destroy(gameObject);
        }
        else if (mBrick_Type == Brick.Brick_Type.concrete) // neets two hits to be destroyed
        {
            if (!onDelay)
            {
                hitcount += 1;
                onDelay = true;
                Invoke("removeOnDelay", .8f);
            }
            if(hitcount > 1) // when the brick is hit twice
            {
                if (!notcallReduceTwice) { level_Bricks.Reduce_A_Brick(); }

                notcallReduceTwice = true;
                Destroy(gameObject);
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
}

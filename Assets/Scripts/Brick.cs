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

    bool notcallReduceTwice = false;
    public void Ball_Hit()
    {
        if(mBrick_Type == Brick.Brick_Type.common)
        {
            if (!notcallReduceTwice) { level_Bricks.Reduce_A_Brick(); }
            
            notcallReduceTwice = true;
            Destroy(gameObject);
        }
    }


    public enum Brick_Type
    {
        common, metal, 
    }
}

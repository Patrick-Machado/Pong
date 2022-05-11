using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public Brick_Type mBrick_Type = Brick.Brick_Type.common;
    
    void Start()
    {
        
    }

    public void Ball_Hit()
    {
        if(mBrick_Type == Brick.Brick_Type.common)
        {
            Destroy(gameObject);
        }
    }


    public enum Brick_Type
    {
        common, metal, 
    }
}

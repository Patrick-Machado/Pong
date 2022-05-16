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



    #region Reflections
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {   
            if(master_brick.mBrick_Type == Brick.Brick_Type.common    &&  other.gameObject.GetComponent<Ball>().isMetalBall) { soundCommonBrick_MetalBall(); } //NOT REFLECT the metal ball should pass through destroying
            if(master_brick.mBrick_Type == Brick.Brick_Type.concrete  &&  other.gameObject.GetComponent<Ball>().isMetalBall) { soundConcreteBrick_MetalBall(); } //reflect but destroy with one hit
            if(master_brick.mBrick_Type == Brick.Brick_Type.metal     &&  other.gameObject.GetComponent<Ball>().isMetalBall) { soundMetalBrick_MetalBall(); metalToMetal(other); } //just reflect with metal sound

            if(master_brick.mBrick_Type == Brick.Brick_Type.common    &&  other.gameObject.GetComponent<Ball>().isMetalBall == false) { soundCommonBrick_NormalBall();   callReflection(other.gameObject); }
            if(master_brick.mBrick_Type == Brick.Brick_Type.concrete  &&  other.gameObject.GetComponent<Ball>().isMetalBall == false) { soundConcreteBrick_NormalBall(); callReflection(other.gameObject); }
            if(master_brick.mBrick_Type == Brick.Brick_Type.metal     &&  other.gameObject.GetComponent<Ball>().isMetalBall == false) { soundMetalBrick_NormalBall();    callReflection(other.gameObject); }

            master_brick.Ball_Hit();
        }
    }

    public enum reflection_direction
    {
        Right, Left, Up, Down, UpLeft, UpRight
    }

    void callReflection(GameObject ball)
    {
        
        ball.gameObject.GetComponent<Ball>().ReflectBallFromBrickColliding(reflect_to);
    }
    void metalToMetal(Collider other)
    {
        master_brick.TriggerVFX(master_brick.ironHitParticle);
        callReflection(other.gameObject); 
        other.gameObject.GetComponent<Rotating>().Rotate();
    }
    #endregion



    #region SoundTriggers
    void soundCommonBrick_MetalBall()
    {
        Game_Manager.Instance.audio_manager.PlaySound(Game_Manager.Instance.audio_manager.metalball);
        Game_Manager.Instance.audio_manager.PlaySound2(Game_Manager.Instance.audio_manager.brickhit2);
    }
    void soundConcreteBrick_MetalBall()
    {
        Game_Manager.Instance.audio_manager.PlaySound(Game_Manager.Instance.audio_manager.metalball);
        Game_Manager.Instance.audio_manager.PlaySound2(Game_Manager.Instance.audio_manager.concretebreak);
    }
    void soundMetalBrick_MetalBall()
    {
        Game_Manager.Instance.audio_manager.PlaySound(Game_Manager.Instance.audio_manager.metalball);
        Game_Manager.Instance.audio_manager.PlaySound2(Game_Manager.Instance.audio_manager.metalball);
    }

    void soundCommonBrick_NormalBall()
    {
        Game_Manager.Instance.audio_manager.PlayAnyPong();
        Game_Manager.Instance.audio_manager.PlaySound2(Game_Manager.Instance.audio_manager.brickhit1);
    }

    void soundConcreteBrick_NormalBall()
    { 
        Game_Manager.Instance.audio_manager.PlayAnyPong();
        Game_Manager.Instance.audio_manager.PlaySound2(Game_Manager.Instance.audio_manager.brickhit1);
    }
    
    void soundMetalBrick_NormalBall()
    { 
        Game_Manager.Instance.audio_manager.PlayAnyPong();
    }

    #endregion

}

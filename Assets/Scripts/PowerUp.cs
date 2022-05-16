using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float speed = 2;
    bool isActive = true;

    public PowerUpTag PowerUpType;

    private void FixedUpdate()
    {
        if(Game_Manager.Instance.player_state == Game_Manager.Player_State.Resting) { isActive = false; Reset_PowerUp(); }
        if (!isActive) return;
        transform.position = new Vector3( 0, transform.position.y - speed * Time.deltaTime,0);

        if (transform.position.y < -4f)
        {
            GameObject.FindGameObjectWithTag("Level_Ctrl").GetComponent<Level_Bricks>().setPowerUpIsRunningOff();
            Reset_PowerUp();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (Game_Manager.Instance.player_state == Game_Manager.Player_State.Resting) { isActive = false; }
            if (!isActive) return;

            if (PowerUpType == PowerUpTag.TribBalls) { Game_Manager.Instance.TriBall_PowerUp(); }
            if(PowerUpType == PowerUpTag.MetalBall)  { Game_Manager.Instance.MetalBall_PowerUp(); }
            if(PowerUpType == PowerUpTag.MegaPaddle) { Game_Manager.Instance.MegaPaddle_PowerUp(true); }

            Game_Manager.Instance.audio_manager.PlaySound(Game_Manager.Instance.audio_manager.powerup);

            Reset_PowerUp();
        }
    }

    public enum PowerUpTag
    {
        TribBalls, MetalBall, MegaPaddle,
    }


    void Reset_PowerUp()
    {
        transform.position = GameObject.FindGameObjectWithTag("Reset_PowerUpPos").transform.position;
        gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Level_Ctrl").GetComponent<Level_Bricks>().setPowerUpIsRunningOff();
    }

}

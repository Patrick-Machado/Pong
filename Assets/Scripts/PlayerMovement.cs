using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool TestingOnPC = false;
    public float moveSpeed = 20.0f;
    public float moveSpeedMobile = 900.0f;
    public float limitMovement = 4f;
    float deltaX;

    public Ball ball;

    private void Start()
    {
        if (TestingOnPC) { Invoke("InitBall",1f); }
        Game_Manager.Instance.mainMenu.SetActive(false);
    }

    void Update()
    {
        if (TestingOnPC) //on PC running tests
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            transform.position += move * moveSpeed * Time.deltaTime;

            if (transform.position.x > limitMovement)
            { transform.position = new Vector3(limitMovement,transform.position.y, 0);}
            else if ((transform.position.x < -limitMovement))
            { transform.position = new Vector3(-limitMovement,transform.position.y, 0);}
            
        }
        else //on Mobile device
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 conversor = new Vector3(touch.position.x, touch.position.y, 1f);
                Vector2 touchpos = UnityEngine.Camera.main.ScreenToWorldPoint(conversor);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        deltaX = touchpos.x - transform.position.x; InitBall();
                        break;
                    case TouchPhase.Moved:
                        transform.position = new Vector3(touchpos.x * moveSpeedMobile * Time.fixedDeltaTime, transform.position.y, transform.position.z) ;
                        break;
                    case TouchPhase.Stationary:
                        break;
                }

                if (transform.position.x > limitMovement)
                { transform.position = new Vector3(limitMovement, transform.position.y, 0); }
                else if ((transform.position.x < -limitMovement))
                { transform.position = new Vector3(-limitMovement, transform.position.y, 0); }
            }
        }
    
        
    }


    void InitBall()
    {
        ball.Pause_Ball(false);
    }


}

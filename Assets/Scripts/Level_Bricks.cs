using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level_Bricks : MonoBehaviour
{
    public  int number_of_bricks = 10;
    public Text life_number_txt;
    private int life = 3;

    private void Start()
    {
        life_number_txt.text = 3.ToString(); 
    }

    public int Lose_A_Life()
    {
        life -=1;
        if(life < 1)
        {
            life_number_txt.text = 0.ToString();
            return 0;
        }
        else
        {
            life_number_txt.text = life.ToString();
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().ResetBallPos();
            return 1;
        }
    }

    public void Reduce_A_Brick()
    {
        number_of_bricks -= 1;

        if(number_of_bricks < 1)
        {
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().Pause_Ball(true);
            Game_Manager.Instance.Stage_Win();
        }
    }
}

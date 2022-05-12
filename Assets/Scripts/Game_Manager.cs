using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    public Stage_Screens current_Screen = Stage_Screens.Menu;
    public Player_State player_state = Player_State.Resting;

    public GameObject mainMenu;
    public GameObject replay;
    public GameObject backtomenu;
    public GameObject nextlevel;

    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public enum Stage_Screens
    {
        Menu, Configs, Credits, Level1, Level2, Level3, Level4, Level5
    }

    public enum Player_State
    {
        Resting, Playing, Dead
    }


    #region MenuFunctions
    public void Play()
    {
        mainMenu.SetActive(false);
        ChangeScene("Level_1");
        current_Screen = Stage_Screens.Level1;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Back_to_menu()
    {
        ChangeScene("Menu");
    }
    #endregion


    #region GameFunctions

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextStage()
    {
        if(current_Screen == Stage_Screens.Level4) { ChangeScene("Level_5"); current_Screen = Stage_Screens.Level5; }
        if(current_Screen == Stage_Screens.Level3) { ChangeScene("Level_4"); current_Screen = Stage_Screens.Level4; }
        if(current_Screen == Stage_Screens.Level2) { ChangeScene("Level_3"); current_Screen = Stage_Screens.Level3; }
        if(current_Screen == Stage_Screens.Level1) { ChangeScene("Level_2"); current_Screen = Stage_Screens.Level2; }
    }

    public void Stage_Win()
    {
        nextlevel.SetActive(true);
    }

    GameObject Ball; Level_Bricks level_bricks;
    public void Game_Over()
    {
        level_bricks = GameObject.FindGameObjectWithTag("Level_Ctrl").GetComponent<Level_Bricks>();

        if(level_bricks.Lose_A_Life() == 0)
        {
            replay.SetActive(true);
            backtomenu.SetActive(true);
        }
    }

    #endregion

}

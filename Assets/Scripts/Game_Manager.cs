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

    public Audio audio_manager;

    [HideInInspector]List< GameObject> BallClones = new List<GameObject>();


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
        Resting, Playing,
    }


    #region MenuFunctions
    public void Play()
    {
        mainMenu.SetActive(false);
        ChangeScene("Level_1");
        current_Screen = Stage_Screens.Level1;
        setPlayerStateAs(Player_State.Playing);
        Time.timeScale = 1f;
    }

    public void setPlayerStateAs(Player_State state)
    {
        player_state = state;
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
        setPlayerStateAs(Player_State.Playing);
        if(current_Screen == Stage_Screens.Level4) { ChangeScene("Level_5"); current_Screen = Stage_Screens.Level5; Time.timeScale = 2f; }
        if(current_Screen == Stage_Screens.Level3) { ChangeScene("Level_4"); current_Screen = Stage_Screens.Level4; Time.timeScale = 1.6f; }
        if(current_Screen == Stage_Screens.Level2) { ChangeScene("Level_3"); current_Screen = Stage_Screens.Level3; Time.timeScale = 1.3f; }
        if(current_Screen == Stage_Screens.Level1) { ChangeScene("Level_2"); current_Screen = Stage_Screens.Level2; Time.timeScale = 1.1f; }
    }

    public void Stage_Win()
    {
        setPlayerStateAs(Player_State.Resting);
        if (BallClones.Count > 0)
        {
            foreach(GameObject ball in BallClones)
            {
                if (ball != null)
                { ball.GetComponent<Ball>().Pause_Ball(true); }
            }
        }
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
        setPlayerStateAs(Player_State.Resting);
    }

    #endregion



    #region PowerUps
    public void TriBall_PowerUp()
    {

        Ball ball_aux = GameObject.FindGameObjectWithTag("Ball").gameObject.GetComponent<Ball>();

        GameObject b = Instantiate(GameObject.FindGameObjectWithTag("Ball").gameObject);
        GameObject b2 =Instantiate(GameObject.FindGameObjectWithTag("Ball").gameObject);

        if (ball_aux.isMetalBall) { b.GetComponent<Ball>().TrnsformToMetalBall(); b2.GetComponent<Ball>().TrnsformToMetalBall(); }

        b.transform.position = ball_aux.transform.position;
        b2.transform.position = ball_aux.transform.position;

        b.GetComponent<Ball>().UnPauseBall();
        b2.GetComponent<Ball>().UnPauseBall();

        BallClones.Add(b.gameObject);
        BallClones.Add(b2.gameObject);

        float dir = Random.RandomRange(1, 180);
        b.GetComponent<Ball>().AddForce( new  Vector3(dir, 150, 0));
        dir = Random.RandomRange(180, 360);
        b2.GetComponent<Ball>().AddForce( new  Vector3(dir, 150, 0));
    }
    public void MetalBall_PowerUp()
    {
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().TrnsformToMetalBall();
        
        if (BallClones.Count > 0)
        {
            foreach (GameObject ball in BallClones)
            {
                if (ball != null)
                { ball.GetComponent<Ball>().TrnsformToMetalBall(); }
            }
        }

    }
    public void MegaPaddle_PowerUp(bool key)
    {
        if(key == true)  GameObject.FindGameObjectWithTag("Paddle").GetComponent<Paddle_Skin>().TransformToMegaVisually(); Invoke("ResetMega", 10f);
        if(key == false) GameObject.FindGameObjectWithTag("Paddle").GetComponent<Paddle_Skin>().TransformToNormalVisually();
    }

    void ResetMega()
    {
        MegaPaddle_PowerUp(false);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//for testing purposes automatic win button
        {
            NextStage();
        }
    }
    private void FixedUpdate()
    {
    }
}

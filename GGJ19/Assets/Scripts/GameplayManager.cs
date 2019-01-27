using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Playing,
    Win,
    Lose
};


public class GameplayManager : Singleton<GameplayManager>
{
    public GameState gameState = GameState.Title;

    public delegate void GameWin();     public static event GameWin OnGameWin;
    public delegate void GameLose();
    public static event GameLose OnGameLose;



    private int currentLevel = 1;
    private int prepareTime = 3;

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //called by title
    public void StartGame()
    {
        LoadLevel();
        gameState = GameState.Playing;
        SpawnCharacterInUI();
    }


    public void LoadLevel()
    {
        //mock date 
        int[,] boarddata = new int[11, 14];
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                boarddata[i, j] = 1;
            }
        }

        boarddata[0, 1] = 0;
        boarddata[0, 1] = 3;
        prepareTime = 3;

        //load real data below


        //
        BoardPanel.Instance.InitBoard(boarddata, 0, 153);
    }

    public void Win()
    {
        OnGameWin();
    }

    public void Lose()
    {
        OnGameLose();
    }


    /// <summary>
    /// Spawns the character in UI section
    /// this is the preparation stage
    /// </summary>
    public void SpawnCharacterInUI()
    {
        Invoke("SpawnCharacterOnBoard", 3);
    }

    public void SpawnCharacterOnBoard()
    {
        KidCharacterController.Instance.Init();
    }


}

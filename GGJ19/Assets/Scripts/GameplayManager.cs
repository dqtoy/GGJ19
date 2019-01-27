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
        // Add the start point
        BoardPanel.Instance.SetEntryExit(
            PipeFactory.Instance.SpawnTileByName("PipeSectionStraight1"), 
            0, 
            0, 
            PipeFactory.Instance.SpawnTileByName("ExitTile"), 
            13, 
            9);
        
        // Add an obstacle
        BoardPanel.Instance.AddToBoard(
            PipeFactory.Instance.SpawnTileByName("NonRemoveableObstacle"),
            5, 
            5);
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
        KidCharacterController.Instance.Init(BoardPanel.Instance.m_EntryTile);
    }


}

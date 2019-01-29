using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,
    Playing,
    Win,
    Lose
};


public class GameplayManager : Singleton<GameplayManager>
{
    public TileFactory TileFactory { get; set; }
    public BoardPanel BoardPanel { get; set; }
    public KidCharacterController KidCharacterController { get; set; }
    public MusicManager MusicManager { get; set; }

    public GameState gameState = GameState.Title;

    public delegate void GameWin();     
    public static event GameWin OnGameWin;
    public delegate void GameLose();
    public static event GameLose OnGameLose;



    private int currentLevel = 1;
    private int prepareTime = 2;

    void Start()
    {
        //StartGame();
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
        Instance.BoardPanel.SetEntryExit(
            Instance.TileFactory.SpawnTileByName("PipeSectionStraight1"), 
            0, 
            0, 
            Instance.TileFactory.SpawnTileByName("ExitTile"), 
            13, 
            9);
        
        // Add an obstacle
        Instance.BoardPanel.AddToBoard(
            Instance.TileFactory.SpawnTileByName("NonRemoveableObstacle"),
            5, 
            5);
        
        Instance.BoardPanel.AddToBoard(
            Instance.TileFactory.SpawnTileByName("PipeSectionCross"),
            1,
            0);
        
        Instance.BoardPanel.AddToBoard(
            Instance.TileFactory.SpawnTileByName("PipeSectionCurve1"),
            2,
            0);
        
        Instance.BoardPanel.AddToBoard(
            Instance.TileFactory.SpawnTileByName("BlackHole"),
            9, 
            5);
    }

    public void Win()
    {
        OnGameWin?.Invoke();
        SceneManager.LoadScene("Win");
        //Debug.Log("Win");
    }

    public void Lose()
    {
        //Debug.Log("Lose");

        OnGameLose?.Invoke();
        SceneManager.LoadScene("Lose");
    }


    /// <summary>
    /// Spawns the character in UI section
    /// this is the preparation stage
    /// </summary>
    private void SpawnCharacterInUI()
    {
        Invoke("SpawnCharacterOnBoard", 3);
    }

    private void SpawnCharacterOnBoard()
    {
        KidCharacterController.Init(Instance.BoardPanel.m_EntryTile);
    }


}

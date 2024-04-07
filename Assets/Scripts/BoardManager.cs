using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    PlayerTurn,
    EnemyTurn,
    GameOver
}


public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance { get; private set; }
    Button[][] board;

    [SerializeField] private GameState gameState;
    public GameState GameState { get => gameState; set => gameState = value; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState = GameSettings.Instance.PlayerFirst ? GameState.PlayerTurn : GameState.EnemyTurn;
        if (gameState == GameState.EnemyTurn)
        {
            EnemyAI.Instance.OnEnemyTurn(board);
        }
    }


    public void InitializeButtons(Button[][] board)
    {
        this.board = board;
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                Button button = board[i][j];
                Tile tile = button.GetComponent<Tile>();
                button.onClick.AddListener(() => OnButtonClick(tile));
            }
        }
    }

    private void OnButtonClick(Tile tile)
    {
        if (gameState != GameState.PlayerTurn) return;

        if (tile.TileType == TileType.Idle)
        {
            tile.OnPlayerPlace();
            if (CheckWin(TileType.Player))
            {
                gameState = GameState.GameOver;
                Debug.Log("Player Wins");
                return;
            }
            gameState = GameState.EnemyTurn;
            EnemyAI.Instance.OnEnemyTurn(board);
        }
    }

    public bool CheckWin(TileType tileType)
    {
        if (CheckHorizontal(tileType) || CheckVertical(tileType) || CheckDiagonal(tileType))
        {
            return true;
        }

        return false;
    }

    private bool CheckHorizontal(TileType tileType)
    {
        for (int i = 0; i < board.Length; i++)
        {
            int count = 0;
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].GetComponent<Tile>().TileType == tileType)
                {
                    count++;
                }
            }
            if (count == 3)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckVertical(TileType tileType)
    {
        for (int i = 0; i < board.Length; i++)
        {
            int count = 0;
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[j][i].GetComponent<Tile>().TileType == tileType)
                {
                    count++;
                }
            }
            if (count == 3)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDiagonal(TileType tileType)
    {
        int count = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][i].GetComponent<Tile>().TileType == tileType)
            {
                count++;
            }
        }
        if (count == 3)
        {
            return true;
        }

        count = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][board.Length - 1 - i].GetComponent<Tile>().TileType == tileType)
            {
                count++;
            }
        }
        if (count == 3)
        {
            return true;
        }

        return false;
    }


}

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

    private List<Button> winButtons = new List<Button>();


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
        Init();
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
                GameWin(TileType.Player);
                Debug.Log("Player Wins");
                return;
            }

            if (CheckDraw()) return;
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
            winButtons.Clear();
            int count = 0;
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].GetComponent<Tile>().TileType == tileType)
                {
                    winButtons.Add(board[i][j]);
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
            winButtons.Clear();
            int count = 0;
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[j][i].GetComponent<Tile>().TileType == tileType)
                {
                    winButtons.Add(board[j][i]);
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
        winButtons.Clear();
        int count = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][i].GetComponent<Tile>().TileType == tileType)
            {
                winButtons.Add(board[i][i]);
                count++;
            }
        }
        if (count == 3)
        {
            return true;
        }

        winButtons.Clear();
        count = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][board.Length - 1 - i].GetComponent<Tile>().TileType == tileType)
            {
                winButtons.Add(board[i][board.Length - 1 - i]);
                count++;
            }
        }
        if (count == 3)
        {
            return true;
        }

        return false;
    }

    public void GameWin(TileType tileType)
    {
        gameState = GameState.GameOver;

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (!winButtons.Contains(board[i][j]))
                {
                    board[i][j].GetComponent<Tile>().Fade();
                }
            }
        }

        if (tileType == TileType.Player)
        {
            MainMenu.Instance.OnPlayerWin();
            ScoreCounter.Instance.IncreasePlayerScore();
        }
        else
        {
            MainMenu.Instance.OnEnemyWin();
            ScoreCounter.Instance.IncreaseEnemyScore();
        }
    }

    public bool CheckDraw()
    {
        bool isDraw = true;
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].GetComponent<Tile>().TileType == TileType.Idle)
                {
                    isDraw = false;
                    return isDraw;
                }
            }
        }

        InvokeAnim();

        Debug.Log("Draw");

        return isDraw;
    }

    private void InvokeAnim()
    {

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                board[i][j].GetComponent<Tile>().Fade();
            }
        }

        gameState = GameState.GameOver;
        ScoreCounter.Instance.IncreaseDrawScore();

        MainMenu.Instance.OnDraw();

    }

    public void ResetGame()
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                board[i][j].GetComponent<Tile>().ResetTileState();
            }
        }

        Init();
    }

    private void Init()
    {
        MainMenu.Instance.OnStartGame();

        gameState = GameSettings.Instance.PlayerFirst ? GameState.PlayerTurn : GameState.EnemyTurn;
        if (gameState == GameState.EnemyTurn)
        {
            EnemyAI.Instance.OnEnemyTurn(board);
        }
    }
}

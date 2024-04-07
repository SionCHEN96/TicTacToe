using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float delayMoveTime = 0.5f;
    public static EnemyAI Instance { get; private set; }
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

    public void OnEnemyTurn(Button[][] board)
    {
        if (BoardManager.Instance.GameState == GameState.EnemyTurn)
        {
            StartCoroutine(DelayedEnemyTurn(board));
        }
    }

    IEnumerator DelayedEnemyTurn(Button[][] board)
    {
        yield return new WaitForSeconds(delayMoveTime);
        CalculateBestMove(board);
    }

    private void CalculateBestMove(Button[][] board)
    {
        if (Check(board))
        {
            return;
        }
        ApplyBestRandomMove(board);
        BoardManager.Instance.GameState = GameState.PlayerTurn;
    }


    private bool Check(Button[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].GetComponent<Tile>().TileType == TileType.Idle)
                {
                    if (CanWin(new Vector2Int(i, j), board))
                    {
                        return true;
                    }
                    if (CanBlock(new Vector2Int(i, j), board))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool CanWin(Vector2Int position, Button[][] board)
    {
        board[position.x][position.y].GetComponent<Tile>().OnEnemyPlace();
        if (BoardManager.Instance.CheckWin(TileType.Enemy))
        {
            Debug.Log("Enemy Wins");
            BoardManager.Instance.GameState = GameState.GameOver;
            return true;
        }
        else
        {
            ResetTileState(position, board);
        }

        return false;
    }

    private bool CanBlock(Vector2Int position, Button[][] board)
    {
        board[position.x][position.y].GetComponent<Tile>().OnPlayerPlace();
        if (BoardManager.Instance.CheckWin(TileType.Player))
        {
            Debug.Log("Enemy Block");
            ResetTileState(position, board);
            board[position.x][position.y].GetComponent<Tile>().OnEnemyPlace();
            BoardManager.Instance.GameState = GameState.PlayerTurn;
            return true;
        }
        else
        {
            ResetTileState(position, board);
        }

        return false;
    }

    private void ResetTileState(Vector2Int position, Button[][] board)
    {
        board[position.x][position.y].GetComponent<Tile>().ResetTileState();
    }

    private void ApplyBestRandomMove(Button[][] board)
    {
        //Priority 1: Center
        if (board[1][1].GetComponent<Tile>().TileType == TileType.Idle)
        {
            board[1][1].GetComponent<Tile>().OnEnemyPlace();
            return;
        }

        //Priority 2: Corners
        if (board[0][0].GetComponent<Tile>().TileType == TileType.Idle)
        {
            board[0][0].GetComponent<Tile>().OnEnemyPlace();
            return;
        }

        if (board[0][2].GetComponent<Tile>().TileType == TileType.Idle)
        {
            board[0][2].GetComponent<Tile>().OnEnemyPlace();
            return;
        }

        if (board[2][0].GetComponent<Tile>().TileType == TileType.Idle)
        {
            board[2][0].GetComponent<Tile>().OnEnemyPlace();
            return;
        }

        if (board[2][2].GetComponent<Tile>().TileType == TileType.Idle)
        {
            board[2][2].GetComponent<Tile>().OnEnemyPlace();
            return;
        }

        //Priority 3: random
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].GetComponent<Tile>().TileType == TileType.Idle)
                {
                    board[i][j].GetComponent<Tile>().OnEnemyPlace();
                    return;
                }
            }
        }
    }
}

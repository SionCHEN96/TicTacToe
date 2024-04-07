using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardInitializer : MonoBehaviour
{
    [SerializeField] private BoardSettings boardSettings;
    Button[][] board;

    private void Awake()
    {
        InitializeBoard();
        InstantiateBoard();
        BoardManager.Instance.InitializeButtons(board);
    }

    private void InitializeBoard()
    {
        board = new Button[boardSettings.rows][];
        for (int i = 0; i < boardSettings.rows; i++)
        {
            board[i] = new Button[boardSettings.columns];
        }
    }

    private void InstantiateBoard()
    {
        for (int i = 0; i < boardSettings.rows; i++)
        {
            for (int j = 0; j < boardSettings.columns; j++)
            {
                GameObject tile = Instantiate(boardSettings.tilePrefab);
                tile.transform.SetParent(transform);
                Vector3 position = transform.position + new Vector3(boardSettings.firstTilePositionOffset.x + j * boardSettings.tileOffset,
                                                                    boardSettings.firstTilePositionOffset.y + i * boardSettings.tileOffset,
                                                                    0);
                tile.transform.position = position;

                InitButton(i, j, tile);
            }
        }
    }

    private void InitButton(int i, int j, GameObject tile)
    {
        Button button = tile.GetComponent<Button>();
        board[i][j] = button;
        Tile tileComponent = tile.GetComponent<Tile>();
        tileComponent.SetPosition(new Vector2Int(i, j));
    }
}

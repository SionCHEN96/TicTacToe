using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum TileType
{
    Idle,
    Player,
    Enemy
}


public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TileSettings tileSettings;
    Vector2Int position;
    Image buttonImage;
    private TileType tileType;
    public TileType TileType { get { return tileType; } set { tileType = value; } }

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        text.text = tileSettings.defaultText;
        buttonImage.color = tileSettings.defaultColor;

        tileType = TileType.Idle;
    }

    public void SetPosition(Vector2Int position)
    {
        this.position = position;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public void OnPlayerPlace()
    {
        text.text = tileSettings.playerText;
        buttonImage.color = tileSettings.playerColor;
        tileType = TileType.Player;
    }

    public void OnEnemyPlace()
    {
        text.text = tileSettings.enemyText;
        buttonImage.color = tileSettings.enemyColor;
        tileType = TileType.Enemy;
    }

    public void ResetTileState()
    {
        text.text = tileSettings.defaultText;
        buttonImage.color = tileSettings.defaultColor;
        tileType = TileType.Idle;
    }


    public void Fade()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        float elapsedTime = 0;
        Color color = buttonImage.color;
        float startAplha = color.a;
        float finalAlpha = tileSettings.finalAlpha;

        while (elapsedTime < tileSettings.fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAplha, finalAlpha, elapsedTime / tileSettings.fadeDuration);
            buttonImage.color = color;
            yield return null;
        }
    }
}

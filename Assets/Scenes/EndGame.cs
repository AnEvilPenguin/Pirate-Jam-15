using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite PlayerDied;
    public Sprite PlayerAlive;

    public void Start() =>
        SpriteRenderer.sprite = GameMaster.Instance.PlayerDied ?
            PlayerDied : PlayerAlive;

    public void EndGameplay() =>
        GameMaster.Instance.LoadMainMenuScene();
}

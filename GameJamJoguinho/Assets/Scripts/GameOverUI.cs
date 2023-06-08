using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }
    private void Awake()
    {
        Instance= this;

        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });        
        
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);

        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

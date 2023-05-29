using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    public Canvas MainMenu;
    public Canvas BuildABattalion;
    public Canvas SetUpGame;
    public Canvas GamePlay;
    public Canvas Results;

    private void Start()
    {
        // normally main menu
        GamePlay.gameObject.SetActive(true);
    }

    public void MainMenuScreen()
    {
        MainMenu.gameObject.SetActive(true);
        BuildABattalion.gameObject.SetActive(false);
        GamePlay.gameObject.SetActive(false);
    }
    public void BuildABattalionScreen()
    {
        MainMenu.gameObject.SetActive(false);
        BuildABattalion.gameObject.SetActive(true);
        GamePlay.gameObject.SetActive(false);
    }
    public void GamePlayScreen()
    {
        MainMenu.gameObject.SetActive(false);
        BuildABattalion.gameObject.SetActive(false);
        GamePlay.gameObject.SetActive(true);
    }
}

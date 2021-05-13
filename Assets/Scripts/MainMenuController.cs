using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void selectAlex() {
        PlayerPrefs.SetString("selectedChar", "alex");
        PlayerPrefs.Save();
        StartGame();
    }

    public void selectMC() {
        PlayerPrefs.SetString("selectedChar", "MC");
        PlayerPrefs.Save();
        StartGame();
    }

    public void selectFallGuy() {
        PlayerPrefs.SetString("selectedChar", "FallGuy");
        PlayerPrefs.Save();
        StartGame();
    }

    public void highScores() {
        SceneManager.LoadScene("HighScores");
    }

} 

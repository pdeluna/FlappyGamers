using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool gameHasEnded;
    
    public GameObject Alex;
    public GameObject FallAdrian;
    public GameObject MC;


    public void Awake()
    {
        gameHasEnded = false;
        
        if (PlayerPrefs.GetString("selectedChar") == "alex") {
            GameObject.Instantiate(Alex);
        }
        if (PlayerPrefs.GetString("selectedChar") == "FallGuy") {
            GameObject.Instantiate(FallAdrian);
        }
        if (PlayerPrefs.GetString("selectedChar") == "MC") {
            GameObject.Instantiate(MC);
        }

        Time.timeScale = 1f;
    }

    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;

        }

    }

} 

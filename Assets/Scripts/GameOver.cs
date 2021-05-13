using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Score;

public class GameOver : MonoBehaviour
{

    public GameObject[] finishObjects;
    public GameManager theGameManager;
    public HighscoreEntry newScore;
    int playerScore;
    public string jsonString;
    public bool scoreRecorded;
    int plays;
    public TopTenList currTopTenList;

    void Awake() {
        finishObjects = GameObject.FindGameObjectsWithTag("Finish");
        hideFinished(); 

        theGameManager = FindObjectOfType<GameManager>();
        scoreRecorded = false;

        // Loads current top ten list of scores from a PlayerPref variable
        currTopTenList = JsonUtility.FromJson<TopTenList>(PlayerPrefs.GetString("topTenList"));

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (theGameManager.gameHasEnded == true && scoreRecorded == false) {
            //Debug.Log("show gameover");
            showFinished(); 
            playerScore = ScoreScript.scoreValue;

            AddHighscoreEntry(playerScore);

            scoreRecorded = true;

        }
    }
    
    public void hideFinished(){
        foreach(GameObject g in finishObjects) {
            g.SetActive(false);
        }
    }

    public void showFinished(){
        foreach(GameObject a in finishObjects) {
            a.SetActive(true);
        }
    }


    private void AddHighscoreEntry(int thePlayersScore) {


        if (thePlayersScore > currTopTenList.theTopTenList[0].score) {
            FindObjectOfType<AudioManager>().Play("HighScore");
        }

        if (PlayerPrefs.GetString("selectedChar") == "FallGuy") {

            // Create Entry object
            TopTenEntry testEntry = new TopTenEntry{score = thePlayersScore, selChar = "skin1"};

            // Retrieve the current top ten scores from PlayerPref variable
            currTopTenList = JsonUtility.FromJson<TopTenList>(PlayerPrefs.GetString("topTenList"));

            // Add Entry object into current top ten list
            currTopTenList.theTopTenList.Add(testEntry);

            // Convert current top ten list into a Json string
            string ttJson = JsonUtility.ToJson(currTopTenList);

            // Save string to a PlayerPref variable for persistent data
            PlayerPrefs.SetString("topTenList", ttJson);

            //Debug.Log(PlayerPrefs.GetString("topTenList"));
            

            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("selectedChar") == "alex") {
            
            // Create Entry object
            TopTenEntry testEntry = new TopTenEntry{score = thePlayersScore, selChar = "skin2"};

            // Retrieve the current top ten scores from PlayerPref variable
            currTopTenList = JsonUtility.FromJson<TopTenList>(PlayerPrefs.GetString("topTenList"));

            // Add Entry object into current top ten list
            currTopTenList.theTopTenList.Add(testEntry);

            // Convert current top ten list into a Json string
            string ttJson = JsonUtility.ToJson(currTopTenList);

            // Save string to a PlayerPref variable for persistent data
            PlayerPrefs.SetString("topTenList", ttJson);

            //Debug.Log(PlayerPrefs.GetString("topTenList"));

            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("selectedChar") == "MC") {
            
            // Create Entry object
            TopTenEntry testEntry = new TopTenEntry{score = thePlayersScore, selChar = "skin3"};

            // Retrieve the current top ten scores from PlayerPref variable
            currTopTenList = JsonUtility.FromJson<TopTenList>(PlayerPrefs.GetString("topTenList"));

            // Add Entry object into current top ten list
            currTopTenList.theTopTenList.Add(testEntry);

            // Convert current top ten list into a Json string
            string ttJson = JsonUtility.ToJson(currTopTenList);

            // Save string to a PlayerPref variable for persistent data
            PlayerPrefs.SetString("topTenList", ttJson);

            //Debug.Log(PlayerPrefs.GetString("topTenList"));

            PlayerPrefs.Save();
        }

        //Debug.Log(PlayerPrefs.GetString("highscoresA"));
        //Debug.Log(PlayerPrefs.GetString("highscoresFG"));
        //Debug.Log(PlayerPrefs.GetString("highscoresMC"));

    }

    [System.Serializable]
    public struct Highscores {
        public List<int> highscoreEntryList;
    }

    [System.Serializable]
    public struct HighscoreEntry {
        public int score;
        //public string name;
    }

    [System.Serializable]
    public struct TopTenList {
        // Only add to list if current score >= last entry
        public List<TopTenEntry> theTopTenList;
    }

    [System.Serializable]
    public struct TopTenEntry {
        public int score;
        public string selChar;
    }

}

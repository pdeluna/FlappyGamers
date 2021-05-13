using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public Image myImage;
    public Sprite skin1;
    public Sprite skin2;
    public Sprite skin3;
    public HighscoreEntries highscoreEntryTransformList;
    public Highscores theFGHSList;
    public Highscores theAHSList;
    public Highscores theMCHSList;
    public Highscores theFGHSListOrdered;
    public Highscores theAHSListOrdered;
    public Highscores theMCHSListOrdered;
    public TopTenList currTopTenList;
    public List<Transform> newTransformList;

    public void goBack() {
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake() {

        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighScoreEntry");

        entryTemplate.gameObject.SetActive(false);

        string jsonString4 = PlayerPrefs.GetString("topTenList");

        currTopTenList = JsonUtility.FromJson<TopTenList>(jsonString4);

     
        newTransformList = new List<Transform>();
        
        if (currTopTenList.theTopTenList.Count == 1) {
            if (currTopTenList.theTopTenList[0].selChar == "skin1") {
                HighscoreEntry convertEntry = new HighscoreEntry{score = currTopTenList.theTopTenList[0].score, skin = skin1};
                CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                //highscoreEntryTransformList.highscoreTransformList.Add(convertEntry);
            }
            if (currTopTenList.theTopTenList[0].selChar == "skin2") {
                HighscoreEntry convertEntry = new HighscoreEntry{score = currTopTenList.theTopTenList[0].score, skin = skin2};
                CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
            }
            if (currTopTenList.theTopTenList[0].selChar == "skin3") {
                HighscoreEntry convertEntry = new HighscoreEntry{score = currTopTenList.theTopTenList[0].score, skin = skin3};
                CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
            }

        }
        else if (currTopTenList.theTopTenList.Count > 1 && currTopTenList.theTopTenList.Count < 10) {
            TopTenList orderedTopTenList = orderTopTen(currTopTenList);
            
            //Debug.Log(JsonUtility.ToJson(orderedTopTenList));

            for (int i=0; i<= currTopTenList.theTopTenList.Count-1; i++) {
                if (orderedTopTenList.theTopTenList[i].selChar == "skin1") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin1};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
                if (orderedTopTenList.theTopTenList[i].selChar == "skin2") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin2};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
                if (orderedTopTenList.theTopTenList[i].selChar == "skin3") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin3};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
            }
        }

        else if (currTopTenList.theTopTenList.Count >= 10) {
            TopTenList orderedTopTenList = orderTopTen(currTopTenList);
            for (int i=0; i<=9; i++) {
                if (orderedTopTenList.theTopTenList[i].selChar == "skin1") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin1};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
                if (orderedTopTenList.theTopTenList[i].selChar == "skin2") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin2};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
                if (orderedTopTenList.theTopTenList[i].selChar == "skin3") {
                    HighscoreEntry convertEntry = new HighscoreEntry{score = orderedTopTenList.theTopTenList[i].score, skin = skin3};
                    CreateHighscoreEntryTransform(convertEntry, entryContainer, newTransformList);
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
            float templateHeight = 50f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string score = highscoreEntry.score.ToString();

            myImage = entryTransform.Find("Image").GetComponent<Image>();
            myImage.sprite = highscoreEntry.skin;
            entryTransform.Find("posText").GetComponent<Text>().text = rank.ToString();
            entryTransform.Find("scoreText").GetComponent<Text>().text = score;

            transformList.Add(entryTransform);
    }

    private TopTenList orderTopTen(TopTenList currentList) {

        int size = currentList.theTopTenList.Count;
        
        for (int i=0; i<=size; i++) {
            for (int j=i+1; j<size; j++) {
                if (currentList.theTopTenList[j].score > currentList.theTopTenList[i].score) {
                    TopTenEntry tmp = currentList.theTopTenList[i];
                    currentList.theTopTenList[i] = currentList.theTopTenList[j];
                    currentList.theTopTenList[j] = tmp;
                }
            }
        }

        //Debug.Log(JsonUtility.ToJson(currentList));

        string jsonTT = JsonUtility.ToJson(currentList);
        PlayerPrefs.SetString("topTenList", jsonTT);
        PlayerPrefs.Save();

        // Need to check if null ?
        // for (int k=0; k<= FGlist.highscoreEntryList.Count; k++) {
        //     for (int l=0; l<= AList.highscoreEntryList.Count; l++) {
        //         for(int m=0; m<= MCList.highscoreEntryList.Count; m++) {
        //             if (MCList.highscoreEntryList[m] >= AList.highscoreEntryList[l] && MCList.highscoreEntryList[m] >= FGlist.highscoreEntryList[k]) {
        //                 HighscoreEntry tmp = new HighscoreEntry{score = MCList.highscoreEntryList[m], skin = skin3};
        //                 // topTenList.Add(tmp);
        //             }
        //             else {
        //                 break;
        //             }
        //         }
        //         if (AList.highscoreEntryList[m] >= AList.highscoreEntryList[l] && MCList.highscoreEntryList[m] >= FGlist.highscoreEntryList[k]) {
        //             //HighscoreEntry tmp = new HighscoreEntry{score = MCList.highscoreEntryList[m], skin = skin3};
        //         }
        //         else {
        //             break;
        //         }
        //     }
        // }

        return currentList;
    }

    [System.Serializable]
    public struct Highscores {
        public List<int> highscoreEntryList;
    }

    public class HighscoreEntries {
        public List<Transform> highscoreTransformList;
    }

    [System.Serializable]
    public class HighscoreEntry {
        public int score;
        public Sprite skin;
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



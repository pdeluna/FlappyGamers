using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Score;

public class Pipe : MonoBehaviour {

    public GameObject character;
    public GameObject pipeDown;
    public GameObject starObj;
    float currPipeX;

    // Use this for initialization
    void Start () {
        character = GameObject.FindWithTag("Player");
    }

    void generatePipes(float offset) {

            float xRan = Random.Range(8, 11);     

            float yRan = 13;

            if (offset == 35) {
                yRan += 2;
            }
            int starProb =0;


            if (xRan == 8) {
                yRan += Random.Range(-1, 1);
            }
            if (xRan == 9) {
                yRan += Random.Range(-2,2);
            }
            if (xRan == 10) {
                yRan += Random.Range(-4, 4);
            }
            if (xRan == 11) {
                yRan += Random.Range(-7, 7);
            }

            xRan += offset;

            GameObject[] allPipes = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject obj in allPipes) {
                if (xRan + character.transform.position.x + 10 >= obj.transform.position.x - 6 & xRan + character.transform.position.x + 10 <= obj.transform.position.x + 6) {
                    xRan += 8;
                    
                }
            }

            float gapRan = Random.Range(0, 2);

            Instantiate(gameObject, new Vector2(character.transform.position.x + 10 + xRan, yRan - 26), transform.rotation);
            starProb = Random.Range(1, 100);
            if (starProb % 5 == 0) {
                Instantiate (starObj, new Vector2(character.transform.position.x + 10 + xRan, yRan - 13), transform.rotation);
            }
            Instantiate(pipeDown, new Vector2(character.transform.position.x + 10 + xRan, yRan + gapRan), transform.rotation);

    }

      
       // Update is called once per frame
       void Update () {

        if (character.transform.position.x - transform.position.x >= 30 )
        {

            generatePipes(0);

            if (character.transform.position.x - transform.position.x > 30 )
            {
                Destroy(gameObject);
                //Destroy(pipeDown);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (character.GetComponent<Character>().invincible == false) {
                character.GetComponent<Character>().Death();  
            }
            else {
                generatePipes(14);
                Destroy(this.gameObject);
                FindObjectOfType<AudioManager>().Play("DestroyPipe");
                ScoreScript.scoreValue += 500; // need to make visible to player
            }

        }
    }

}

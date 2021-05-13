using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Score;

public class PipeD : MonoBehaviour {

    //private Character character;
    public GameObject character;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (character.transform.position.x - transform.position.x > 30)
        {
            Destroy(gameObject);
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
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("DestroyPipe");
                ScoreScript.scoreValue += 500;
            }
        }

    }

}
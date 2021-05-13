using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    //private Character character;
    public GameObject character;

    // Start is called before the first frame update
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

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Destroy(gameObject);
            character.GetComponent<Character>().Invincible();
        }

    }
}

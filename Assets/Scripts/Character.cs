using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Score;

public class Character : MonoBehaviour {

    public Rigidbody2D rb;
    public float moveSpeed;
    public float flapHeight;
    public GameObject pipe_up;
    public GameObject pipe_down;
    public GameManager theGameManager;
    public float minXGap = 8;
    public float maxXGap = 12;
    public bool invincible;
    public float powerUpTimer = 10.0f;
    public float endBuildX;
    public float endBuildY;
    public bool alive = true;

    void Awake() {
        alive = true;
        rb = GetComponent<Rigidbody2D>();
        theGameManager = FindObjectOfType<GameManager>();
        BuildLevel();
        invincible = false;
    }
    void Start () {
        FindObjectOfType<AudioManager>().Play("Quack");
    }

    public void BuildLevel()
    {

        ScoreScript.scoreValue = 0;
        float currX = 14;
        float currYDown = 13; 
        float xGap;

        moveSpeed = 3.0f;

        // Spawn initial pipes
        for (int i=0; i <= 3; i++) {
            Instantiate(pipe_down, new Vector3(currX, currYDown), transform.rotation);
            Instantiate(pipe_up, new Vector3(currX, currYDown - 26), transform.rotation);

            xGap = Random.Range(minXGap, maxXGap);
            currX += xGap;

            if (xGap == 8) {
                currYDown += Random.Range(-1, 1);
            }
            if (xGap == 9) {
                currYDown += Random.Range(-2,2);
            }
            if (xGap > 9 && xGap <= 11) {
                currYDown += Random.Range(-4,4);
            }
            if (xGap > 11) {
                currYDown += Random.Range(-8, 8);
            }

        }

        endBuildX = currX + 3;
        endBuildY = currYDown - 26;

    }
      

       // Update is called once per frame
       void Update () {
        
        // Player control
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
        if (Input.GetMouseButtonDown(0))
        {
            if (alive) {
                FindObjectOfType<AudioManager>().Play("Flap");
            }

            rb.velocity = new Vector3(rb.velocity.x, flapHeight);
        }

        // Game bounds
        if (transform.position.y > 18 || transform.position.y < -19)
        {
            Death();
        }

        if (invincible == true) {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0.0f) {
                invincible = false;
            }
        }

        if (Time.timeScale != 0) {
            ScoreScript.scoreValue += 1;
            moveSpeed = moveSpeed*1.00002f;
        }
        
        
    }

    public void Death()
    {
        Time.timeScale = 0;
        rb.velocity = Vector3.zero;

        if (alive == true) {
            FindObjectOfType<AudioManager>().Stop("Quack");
            FindObjectOfType<AudioManager>().Play("Dead");
        }
        this.alive = false;     

        theGameManager.EndGame();

        // GameObject[] allPipes = GameObject.FindGameObjectsWithTag("Pipe");
        // foreach(GameObject obj in allPipes) {
        //     Destroy(obj);
        // }

        // GameObject[] allStars = GameObject.FindGameObjectsWithTag("Star");
        // foreach(GameObject obj2 in allStars) {
        //     Destroy(obj2);
        // }



    }

    public void Invincible()
    {
        
        invincible = true;
        powerUpTimer = 10.0f;
    }


}
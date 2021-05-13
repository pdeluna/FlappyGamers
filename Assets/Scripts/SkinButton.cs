using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkinButton : MonoBehaviour
{
    public GameObject skinButton;
    public UnityEvent OnClick = new UnityEvent();
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skinButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            Debug.Log("Clicked Button");

            RaycastHit hitInfo = new RaycastHit();

            bool clicked = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (clicked) {
                if (hitInfo.transform.gameObject.tag == "Player") {
                    DontDestroyOnLoad(this.gameObject);
                    Debug.Log("Working");
                }
                else {
                    Debug.Log("Naw");
                }
            }
        } 
        else {
            //Debug.Log("Not clicked");
        }
    }
}

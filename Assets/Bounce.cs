using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
    public float bounceSensitivity = 70f;
    public bool water = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (water) collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.rotation.z, bounceSensitivity), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (water != true)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.rotation.z, bounceSensitivity), ForceMode2D.Impulse);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

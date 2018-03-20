using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int key = 0;
    public bool destroy = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (key == 0)
        {
            if (other.tag == "Player" && other.GetComponent<PlayerControl>().hasKey1 == true)
            {
                other.GetComponent<PlayerControl>().hasKey1 = false;
                if (destroy)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        if (key == 1)
        {
            if (other.tag == "Player" && other.GetComponent<PlayerControl>().hasKey2 == true)
            {
                other.GetComponent<PlayerControl>().hasKey2 = false;
                if (destroy)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public int key = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (key == 0)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerControl>().hasKey1 = true;
                Destroy(this.gameObject);

            }
        }
        if (key == 1)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerControl>().hasKey2 = true;
                Destroy(this.gameObject);

            }
        }
    }
}

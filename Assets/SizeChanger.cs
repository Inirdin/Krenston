using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour {
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "BigZone" || other.tag != "SmallZone")
        {
            if (other.tag == "BigZone")
            {
                if (Mathf.Sign(player.transform.localScale.x) == 1)
                {
                    player.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
                }
                else
                {
                    player.transform.localScale = new Vector3(-2.2f, 2.2f, 2.2f);
                }
            }
            else if (other.tag == "SmallZone")
            {
                if (Mathf.Sign(player.transform.localScale.x) == 1)
                {
                    player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
                else
                {
                    player.transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BigZone" || other.tag == "SmallZone")
        {
            if (Mathf.Sign(player.transform.localScale.x) == 1)
            {
                player.transform.localScale = new Vector3(1.442764f, 1.442764f, 1.442764f);
            }
            else
            {
                player.transform.localScale = new Vector3(-1.442764f, 1.442764f, 1.442764f);
            }
        }
    }
}

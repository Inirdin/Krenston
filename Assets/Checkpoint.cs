using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public GameObject respawnPoint;
    public Vector2 point;
    public GameObject trig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        point = respawnPoint.transform.position;
        if (collision.tag == "Player")
        {
            trig.GetComponent<ParticleSystem>().Play();
            collision.GetComponent<PlayerControl>().RespawnOutside(true, point);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    public GameObject endcard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Application.LoadLevel("Endcard");
        Time.timeScale = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSelector : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
            for (int i = 0; i < LevelManager.instance.numberOfLevels; i++)
            {
                LevelManager.instance.transform.GetChild(i).GetComponentInChildren<ZoneChanger>().Zone(true, other.tag);
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < LevelManager.instance.numberOfLevels; i++)
        {
            LevelManager.instance.transform.GetChild(i).GetComponentInChildren<ZoneChanger>().Zone(false, other.tag);
        }
    }
}

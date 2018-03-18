using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChanger : MonoBehaviour {
    public GameObject redObjectsHide;
    public GameObject blueObjectsHide;
    public GameObject greenObjectsHide;
    public GameObject yellowObjectsHide;

    public GameObject redObjectsShow;
    public GameObject blueObjectsShow;
    public GameObject greenObjectsShow;
    public GameObject yellowObjectsShow;

    public void Zone(bool enter, string tag)
    {
        if (enter)
        {
            switch (tag)
            {
                case "RedZone":
                    redObjectsHide.SetActive(false);
                    redObjectsShow.SetActive(true);
                    break;
                case "BlueZone":
                    blueObjectsHide.SetActive(false);
                    blueObjectsShow.SetActive(true);
                    break;
                case "GreenZone":
                    greenObjectsHide.SetActive(false);
                    greenObjectsShow.SetActive(true);
                    break;
                case "YellowZone":
                    yellowObjectsHide.SetActive(false);
                    yellowObjectsShow.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (tag)
            {
                case "RedZone":
                    redObjectsHide.SetActive(true);
                    redObjectsShow.SetActive(false);
                    break;
                case "BlueZone":
                    blueObjectsHide.SetActive(true);
                    blueObjectsShow.SetActive(false);
                    break;
                case "GreenZone":
                    greenObjectsHide.SetActive(true);
                    greenObjectsShow.SetActive(false);
                    break;
                case "YellowZone":
                    yellowObjectsHide.SetActive(true);
                    yellowObjectsShow.SetActive(false);
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public GameObject intro;

	public void End()
    {
        Application.Quit();
    }
    public void Next()
    {
        intro.SetActive(true);
    }
    public void Game()
    {
        Application.LoadLevel("Level - kopie");
    }
}

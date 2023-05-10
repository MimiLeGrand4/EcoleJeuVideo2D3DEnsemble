using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public int sceneBuildIndex;

    public void playGame()
    {
        Debug.Log("PLAY !");
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public int sceneBuildIndex;
    public int startScene2d;
    public int startScene3d;

    public void playGame()
    {
        Debug.Log("PLAY !");
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    public void start2d()
    {
        SceneManager.LoadScene(startScene2d, LoadSceneMode.Single);
    }

    public void start3d()
    {
        SceneManager.LoadScene(startScene3d, LoadSceneMode.Single);
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseCanvas;
    int sceneBuildIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button myButton = GetComponent<Button>();

        if(myButton.gameObject.name == "RestartButton")
        {
            myButton.onClick.AddListener(Restart);
        }

        if(myButton.gameObject.name == "MenuButton")
        {
            myButton.onClick.AddListener(Menu);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}

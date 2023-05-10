using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public AudioSource victorySound;
    public AudioSource levelMusic;
    int sceneBuildIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Menu);
        victorySound.Play();
        levelMusic.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Menu();
        }
    }

    void Menu()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}

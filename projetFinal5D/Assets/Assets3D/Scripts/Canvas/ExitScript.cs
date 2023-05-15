using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public Canvas victoryCanvas;
    public static MainMenuScript mainMenuScript;
    public TMP_Text yourTextField;
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string playerName = PlayerPrefs.GetString("PlayerName");
            Debug.Log(playerName);
            // Vous pouvez maintenant accéder au texte du champ d'entrée comme ceci:
            yourTextField.text = playerName + ", vous avez gagné!";
            Debug.Log(yourTextField);
            // Pause the game
            Time.timeScale = 0f;

            // Show the exit canvas
            victoryCanvas.gameObject.SetActive(true);
        }
    }
}

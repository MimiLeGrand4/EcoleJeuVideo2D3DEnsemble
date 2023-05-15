using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    private int coin;
    public Canvas victoryCanvas;
    public static MainMenuScript mainMenuScript;
    public TMP_Text yourTextField;
    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
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

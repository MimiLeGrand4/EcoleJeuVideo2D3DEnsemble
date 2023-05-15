using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoireNomScript : MonoBehaviour
{
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
        
        string playerName = PlayerPrefs.GetString("PlayerName");
        Debug.Log(playerName);
        // Vous pouvez maintenant accéder au texte du champ d'entrée comme ceci:
        yourTextField.text = playerName + ", vous avez gagné!";
        Debug.Log(yourTextField);
    }
}

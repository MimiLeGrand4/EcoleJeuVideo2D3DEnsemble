using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public TMP_InputField  nameField;
    public Button startButton2D;
    public Button startButton3D;
    public int sceneBuildIndex;
    public int startScene2d;
    public int startScene3d;
    
    
    private void Start()
    {
        // Désactiver les boutons au début
        startButton2D.interactable = false;
        startButton3D.interactable = false;
        nameField.onValueChanged.AddListener(delegate { VerifyInput(); });
    }
    public void VerifyInput()
    {
        // Activer les boutons si le champ de nom n'est pas vide
        startButton2D.interactable = nameField.text.Length >= 1;
        startButton3D.interactable = nameField.text.Length >= 1;
    }

    public void playGame()
    {
        Debug.Log("PLAY !");
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    public void start2d()
    {
            // Enregistrer le nom du joueur et charger le jeu 2D
            PlayerPrefs.SetString("PlayerName", nameField.text);
            
            Debug.Log("PLAY2 !");
            SceneManager.LoadScene(startScene2d, LoadSceneMode.Single);


    }

    public void start3d()
    {

            // Enregistrer le nom du joueur et charger le jeu 2D
            PlayerPrefs.SetString("PlayerName", nameField.text);
            Debug.Log("PLAY3 !");
            SceneManager.LoadScene(startScene3d, LoadSceneMode.Single);
        
        
        
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}

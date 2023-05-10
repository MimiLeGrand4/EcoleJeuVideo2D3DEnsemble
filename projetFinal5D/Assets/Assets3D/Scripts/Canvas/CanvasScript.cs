using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public TextMeshProUGUI nbCoins;
    public TextMeshProUGUI nbVie;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCanvas();
    }

    public void PlayerCanvas(){
        //Texte pour les vies
        if(playerMovement.health >=0){
            nbVie.text = "" + playerMovement.health;
        }

        else{
            nbVie.text = "0";
        }

        //Text pour les coins
        GameObject[] listCoins = GameObject.FindGameObjectsWithTag("Coin");

        if(listCoins != null){
            nbCoins.text = "" + listCoins.Length;
        }

        else{
            nbCoins.text = "0";
        }
    }
}

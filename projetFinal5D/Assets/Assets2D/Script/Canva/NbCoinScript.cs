using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NbCoinScript : MonoBehaviour
{
    private TextMeshProUGUI nbCoins;

    // Start is called before the first frame update
    void Start()
    {
        nbCoins = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] listCoins = GameObject.FindGameObjectsWithTag("Coin");

        if(listCoins != null){
            nbCoins.text = "" + listCoins.Length;
        }

        else{
            nbCoins.text = "0";
        }
        
    }
}

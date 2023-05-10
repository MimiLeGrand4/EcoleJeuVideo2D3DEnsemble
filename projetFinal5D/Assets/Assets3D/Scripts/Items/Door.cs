using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int coin;
    public GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] listCoins = GameObject.FindGameObjectsWithTag("Coin");

        if(listCoins != null){
            coin = listCoins.Length;
        }
        
        if(coin <= 0){
            //Test sur la console
            Debug.Log("La porte est ouverte");

            // Supprime la porte
            foreach (GameObject door in doors) {
                door.SetActive(false);
            }

        }
    }
}

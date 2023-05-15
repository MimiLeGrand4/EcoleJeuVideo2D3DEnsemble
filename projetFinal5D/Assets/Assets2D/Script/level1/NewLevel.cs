using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class NewLevel : MonoBehaviour
{
    private string level1 = "level1_2d";
    private string level2 = "level2_2d";
    private string level3 = "level3_2d";
    private string level4 = "level4_2d";
    private int coin;
    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        PorteOuverte();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == level1){
                LoadScene(level2);
            }

            else if(sceneName == level2){
                LoadScene(level3);
            }

            else if (sceneName == level3){
                LoadScene(level4);
            }
        }
    }

    private void PorteOuverte(){
        GameObject[] listCoins = GameObject.FindGameObjectsWithTag("Coin");

        if(listCoins != null){
            coin = listCoins.Length;
        }
        
        if(coin <= 0){

            // Supprimer le sprite
            GameObject tilemapObject = GameObject.Find("Porte");
            TilemapRenderer tilemapRenderer = tilemapObject.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

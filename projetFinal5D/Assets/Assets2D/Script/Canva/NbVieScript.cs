using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NbVieScript : MonoBehaviour
{
    private TextMeshProUGUI nbVie;
    public HeroKnight heroKnight;
    private float vie;
    // Start is called before the first frame update
    void Start()
    {
        heroKnight = FindObjectOfType<HeroKnight>();
        nbVie = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        vie = heroKnight.getVieActuel();
        if(vie <= 0){
            nbVie.text = "0";
        }

        else{
            nbVie.text = "" + vie;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject Chest;

    public Camera MainCamera;
    public Camera doorCamera;

    private bool nowMaincam = true;

    public void CamSwitch ()
    {
        if (nowMaincam)
        {
            MainCamera.depth = 0;
            doorCamera.depth = 1;
            nowMaincam = false;
        }else if(!nowMaincam){
            MainCamera.depth = 1;
            doorCamera.depth = 0;
            nowMaincam = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        /*Chest.SetActive(true);*/
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rest();
            CamSwitch();
            nowMaincam = false;

        }
        /*Chest.SetActive(false);*/
    }
    
    private void Rest()
    {
        anim.SetTrigger("OC");
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            CamSwitch();

        }
        
    }
}

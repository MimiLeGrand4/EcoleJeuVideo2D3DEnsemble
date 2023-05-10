using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RetourMainScene);
    }

    // Update is called once per frame
   void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            RetourMainScene();
        }
    }

    void RetourMainScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }

}

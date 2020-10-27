using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRunGameStartClick()
    {
        SceneManager.LoadScene("_Game_Scene");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}

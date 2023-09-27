using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private bool _isgameover = false;
    
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&& _isgameover==true)
        {
            SceneManager.LoadScene(0); //Game Scene
        }
    }
    public void GameOver()
    {
        _isgameover=true;
    }

}

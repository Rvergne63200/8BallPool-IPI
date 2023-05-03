using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    [SerializeField]
    private string gameScene;
    
    public void PlayButtonAction()
    {
        SceneManager.LoadScene(gameScene);
    }
}

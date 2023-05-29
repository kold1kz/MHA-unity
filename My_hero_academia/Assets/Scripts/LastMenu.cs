using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastMenu : MonoBehaviour
{
   public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        Debug.Log("Game restart");
    }

    public void ExitGame(){
        Application.Quit();
        Debug.Log("Game closed");
    }
}

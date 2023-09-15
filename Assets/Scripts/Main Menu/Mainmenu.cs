using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
   public void SinglePlayer()
   {
      SceneManager.LoadScene(1); //Main game
   }
   public void MuiltiPlayer()
   {
      SceneManager.LoadScene(2);
   }
   public void QuitGame()
   {
      Application.Quit();
   }
}

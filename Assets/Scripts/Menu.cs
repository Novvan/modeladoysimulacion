﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public void PlayGame(string scenename)
  {
    SceneManager.LoadScene(scenename);
  }

  public void QuitGame()
  {
    Application.Quit();
    Debug.Log("QuitButton");
  }
}

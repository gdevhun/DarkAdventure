using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetController : MonoBehaviour
{
    public void MenuBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
}

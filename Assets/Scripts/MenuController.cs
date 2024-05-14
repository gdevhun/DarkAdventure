using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	
	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);
	}
	
	public void StartBtn()
	{
		SceneManager.LoadScene("GameScene");
	}
	public void ExitBtn()
	{
		Application.Quit();
	}

}

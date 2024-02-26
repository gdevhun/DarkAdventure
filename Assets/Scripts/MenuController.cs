using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	private void Awake()
	{
		Screen.SetResolution(1920, 1080, false);
	}
	private void Start()
	{
		SoundManager.Instance.PlayBGM(SoundType.MenuBGM);
	}
	public void StartBtn()
	{
		SceneManager.LoadScene("FirstScene");
	}
	public void ExitBtn()
	{
		Application.Quit();
	}
	public void SettingBtn()
	{
		//씬 추가구현 필요
		SceneManager.LoadScene("SettingScene");
	}
}

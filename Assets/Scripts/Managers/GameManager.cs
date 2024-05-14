using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
	//게임메니저는 게임신에 있음.
	private WaitForSeconds delayTime = new WaitForSeconds(2f);
	public bool isGameOver = false;
	public bool isWinGame = false;

	public GameObject WinPanel;
	public GameObject LosePanel;
	public void LoseGame()
	{
		if (isGameOver)
		{
			Player.Instance.PlayerDeadEvent();
			StartCoroutine(ActiveLosePanel());
		}
	}

	private void Start()
	{
		SoundManager.Instance.PlayBGM(SoundType.FirstBGM);
	}

	public void WinGame()
	{
		if (isWinGame)
		{
			StartCoroutine(ActiveWinPanel());
		}
	}
	private IEnumerator ActiveLosePanel()
	{
		yield return delayTime;
		LosePanel.SetActive(true);
	}
	private IEnumerator ActiveWinPanel()
	{
		yield return delayTime;
		WinPanel.SetActive(true);
	}
}

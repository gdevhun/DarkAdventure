using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
	
{
	public static GameManager Instance;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(this);
		}
	}

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

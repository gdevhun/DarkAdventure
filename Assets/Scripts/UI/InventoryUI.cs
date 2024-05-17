using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	private RectTransform rectTransform;
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}
	public void Show()
	{
		rectTransform.anchoredPosition = Vector2.zero;
	}
	public void Hide()
	{
		rectTransform.anchoredPosition = Vector2.up * 1100f;
	}
}

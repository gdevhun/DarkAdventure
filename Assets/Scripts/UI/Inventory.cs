using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance=null;
	public List<Item> inventory = new List<Item>();
	public GameObject inventoryUI;

	private ItemButton[] itemBtns;

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
	private void Start()
	{
		itemBtns=inventoryUI.GetComponentsInChildren<ItemButton>();
	}

	public void GetItem(Item _itemData)
	{
		if (inventory.Count <= 16)
		{
			inventory.Add(_itemData);
			itemBtns[inventory.Count - 1].Add(_itemData);
		}
		else
		{
			Debug.Log("인벤토리창포화");
		}
	}
	public bool IsFullInventory()
	{
		return inventory.Count >= 16; 
	}
}

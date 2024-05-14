
using System;
using System.Collections.Generic;
using UnityEngine;
public class Items : MonoBehaviour
{
	[SerializeField]
	private Item itemData;


	SpriteRenderer spriteRenderer;
	Rigidbody2D rigid;
	private void Awake()
	{
		rigid= GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		itemData.itemSprite=spriteRenderer.sprite;
	}

	//private Inventory inventoryBag;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (Inventory.Instance.IsFullInventory())
			{

				rigid.AddForce(new Vector2(UnityEngine.Random.Range(-1f,1f),2f));
			}
			else
			{
				SoundManager.Instance.PlaySFX(SoundType.PlayerGetItem);
				Inventory.Instance.GetItem(itemData);
				gameObject.SetActive(false);
		    }

		}
	}
	
}
[Serializable]
public class Item
{
	public enum Type
	{
		HpPotion,
		MpPotion,
		Lighter,
		PowerItem,
	}

	public Type type;
	public string itemName;
	public string itemDesc;
	public Sprite itemSprite;
	public float value;
}


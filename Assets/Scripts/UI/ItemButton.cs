using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemButton : MonoBehaviour
{
	[SerializeField]
	Item itemData;

	public ItemInfoText explainText;
	private Image itemImage;
	private Button button;

	private void Awake()
	{
		explainText = FindObjectOfType<ItemInfoText>();
		itemImage = GetComponentsInChildren<Image>()[1];
		//btnImage = GetComponentsInChildren<Image>()[0];
		button = GetComponent<Button>();
	}

	public void OnMouseEnter()
	{
		SoundManager.Instance.PlaySFX(SoundType.PlayerBubbleSFX,1f);
		if(itemData != null)
		{
			explainText.ChangeInfo(itemData);
		}
		return;
	}
	public void OnMouseExit()
	{
		explainText.ResetItemInfo();
	}
	public void OnMouseDown()
	{
		Player.Instance.OnUseplayer(itemData);
		Remove();
	}
	public void Add(Item _itemData)
	{
		itemData = _itemData;
		itemImage.sprite = itemData.itemSprite;
		itemImage.enabled = true;
		button.interactable = true;
	}
	public void Remove()
	{
		itemData = null;
		itemImage.enabled = false;
		button.interactable = false;
	}
}


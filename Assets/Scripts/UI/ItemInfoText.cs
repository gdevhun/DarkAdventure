using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoText : MonoBehaviour
{
	public TextMeshProUGUI explainText;
	/*private readonly string hpPotion = "RESTORE PLAYER HP";
	private readonly string MpPotion = "RESTORE PLAYER MP";
	private readonly string lighter = "MAKE PLAYER LIGHER";
	private readonly string powerItem = "MAKE PLAYER STRONGER";*/

	public void ChangeInfo(Item itemData)  
	{
		explainText.text = itemData.itemName;
	}

	/*public string ChangeInfo(Item itemData) => itemData.type switch
	{
		Item.Type.HpPotion => explainText.text = hpPotion,
		Item.Type.MpPotion => explainText.text = MpPotion,
		Item.Type.PowerItem => explainText.text = powerItem,
		Item.Type.Lighter => explainText.text = lighter,

		 _ => throw new InvalidOperationException("Invalid type")
	};*/

	public void ResetItemInfo()
	{
		explainText.text = " ";
	}

}

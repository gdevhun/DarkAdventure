using UnityEngine;
using UnityEngine.UI;
public class PlayerMp : PlayerStats
{
	public static PlayerMp Instance = null;
	public Image[] MpImage;
	protected override void Awake()
    {
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(Instance);
		}

		base.Awake();
	}
	
	protected override void IncreaseStats(float amount)
	{
		base.IncreaseStats(amount);
		base.UpdateStats(MpImage, currentMp);
	}
	public float CurrentMp { get => currentMp; }

	public void UseSkill(float val)
	{
		currentMp -= val;
		if(currentMp <= 0) 
		{
			currentMp = 0;
		}
		base.UpdateStats(MpImage, currentMp);
	}
	public void RestoreMp(float val)
	{
		currentMp += val;
		if (currentMp >= 100)
		{
			currentMp = 100;
		}
		base.UpdateStats(MpImage, currentMp);
	}

}

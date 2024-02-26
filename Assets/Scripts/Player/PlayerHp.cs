using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : PlayerStats
{
	public static PlayerHp Instance = null;
	public Image[] HpImage;
    
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
		base.UpdateStats(HpImage, currentHp);
	}
	public void TakeDamage(float damage,Vector2 enemyPos) //by enemy
    {
		//³Ë¹éo ÇÔ¼ö
		Player.Instance.KnockBack(enemyPos);
		currentHp -=damage;
		if (currentHp <= 0)
        {
		    currentHp = 0;
            GameManager.Instance.isGameOver = true;
			GameManager.Instance.LoseGame();
		}
        base.UpdateStats(HpImage, currentHp);
		Player.Instance.PlayerHitSound();
	}
	public void TakeDamage(float damage) //by trap or projectile
	{   //³Ë¹éx ÇÔ¼ö
		currentHp -= damage;
		if (currentHp <= 0)
		{
			currentHp = 0;
			GameManager.Instance.isGameOver = true;
			GameManager.Instance.LoseGame();
		}
		base.UpdateStats(HpImage, currentHp);
		Player.Instance.PlayerHitSound();
	}

	public float CurrentHp { get => currentHp; }

    public void RestoreHp(float val)
    {
        currentHp += val;
		if (currentHp >= 100) {
            currentHp = 100;
        }
		base.UpdateStats(HpImage, currentHp);
    }
}

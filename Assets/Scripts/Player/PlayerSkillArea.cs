using UnityEngine;

public class PlayerSkillArea : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			if (collision.gameObject.TryGetComponent(out Enemy enemy))
			{
				enemy.DamagedbyPlayer(Player.Instance.playerAttackDmg*1.5f);
			}
		}
		if (collision.CompareTag("Boss"))
		{
			if (collision.gameObject.TryGetComponent(out Boss boss))
			{
				boss.DamagedbyPlayer(Player.Instance.playerAttackDmg*1.5f);
			}
		}
	}
}

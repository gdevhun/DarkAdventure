using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	enum ActionPattern { MoveRightAttack, StationaryAttack, TeleportAttack, SpawnZombie,
	MoveRightAttack2, MoveRightAttack3}
	[SerializeField] private ActionPattern currentPattern;
	public List<AudioSource> BossSoundSFX = new List<AudioSource>();

	private float currentBossHP;
	[SerializeField] private float maxBossHP;
	private Animator anim;
	
	private void Awake()
	{
		currentBossHP = maxBossHP;
		anim = GetComponent<Animator>();
	}
	private void Start()
	{
		Think();	
	}
	
	private void Think()
	{
		// 다음 패턴 랜덤 결정
		int ranPattern = Random.Range(0, 6); //0,1,2,3,4,5

		currentPattern = (ActionPattern)ranPattern;
		anim.SetTrigger("Pattern" + ranPattern);
	}
	public void DamagedbyPlayer(float playerDmg)
	{
		currentBossHP-=playerDmg;
		if (currentBossHP <= 0)
		{
			anim.SetTrigger("isDead");
		}
	}
	private void OnDead()
	{
		GameManager.Instance.isWinGame = true;
		Debug.Log(GameManager.Instance.isWinGame);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerHp.Instance.TakeDamage(9f);
		Player.Instance.PlayerHitSound();
	}

	//boss sound
	private void PlayAirAttack() => BossSoundSFX[0].PlayOneShot(BossSoundSFX[0].clip);
	private void PlayStationaryAttack() => BossSoundSFX[1].PlayOneShot(BossSoundSFX[1].clip);
	private void PlayLaughing1() => BossSoundSFX[2].PlayOneShot(BossSoundSFX[2].clip);
	private void PlayLaughing2() => BossSoundSFX[3].PlayOneShot(BossSoundSFX[3].clip);
	private void PlayMoveSound() => BossSoundSFX[4].PlayOneShot(BossSoundSFX[4].clip);
	private void PlayDeadSound() => BossSoundSFX[5].PlayOneShot(BossSoundSFX[5].clip);
	private void PlayVanishSound() => BossSoundSFX[6].PlayOneShot(BossSoundSFX[6].clip);
}

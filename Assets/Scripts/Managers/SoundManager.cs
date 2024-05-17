using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
	[System.Serializable]
	public class Sound
	{
		public SoundType soundType;
		public AudioClip clip;
	}

	[SerializeField]
	private List<Sound> bgms = null;
	[SerializeField]
	private List<Sound> sfxs = null;
	[SerializeField]
	private AudioSource bgmPlayer = null;
	public List<AudioSource> sfxPlayers = new List<AudioSource>();

	private Dictionary<SoundType, AudioClip> _sfxDictionary;

	private Queue<AudioSource> _sfxQueue = new Queue<AudioSource>();

	public float bgmVolume, sfxVolume; // 배경음 볼륨 및 효과음 볼륨
	protected override void Awake()
	{
		base.Awake();

		// SFX 딕셔너리 초기화
		_sfxDictionary = sfxs.ToDictionary(s => s.soundType, s => s.clip);
	}

	private void Start()
	{
		bgmPlayer = gameObject.AddComponent<AudioSource>();
		// 볼륨 초기화
		bgmVolume = 0.6f; 
		sfxVolume = 1f;
		
		// 메뉴bgm재생
		SoundManager.Instance.PlayBGM(SoundType.MenuBGM);

		// SFX 플레이어 몇 개를 초기에 생성하고 리스트에 추가
		for (int i = 0; i < 20; i++)
		{
			AudioSource sfxPlayer = gameObject.AddComponent<AudioSource>();
			sfxPlayers.Add(sfxPlayer);
			_sfxQueue.Enqueue(sfxPlayer);
		}
	}

	public void PlayBGM(SoundType soundType)
	{
		var bgm = bgms.First(b => b.soundType == soundType);
		bgmPlayer.clip = bgm.clip;
		bgmPlayer.volume = bgmVolume; // 볼륨 조절 bgmVolume
		bgmPlayer.loop = true;
		bgmPlayer.Play();
	}

	public void StopBGM()
	{
		bgmPlayer.Stop();
	}
	
	
	// 사운드 플레이가 끝나면 호출하여 큐에 다시 넣기
	public void ReturnSfxPlayerToQueue(AudioSource sfxPlayer)
	{
		_sfxQueue.Enqueue(sfxPlayer);
	}
	
	// 사운드 재생이 끝나면 풀에 반환
	private async UniTaskVoid ReturnSfxPlayerWhenFinished(AudioSource sfxPlayer, float delay)
	{
		// 사운드 재생 시간만큼 대기
		await UniTask.Delay(TimeSpan.FromSeconds(delay));
		
		// 사운드 재생이 끝났으니 큐에 반환
		ReturnSfxPlayerToQueue(sfxPlayer);
	}
	
	public void PlaySfx(SoundType soundType)
	{
		if (_sfxDictionary.TryGetValue(soundType, out AudioClip clip))
		{
			AudioSource sfxPlayer = GetAvailableSfxPlayer();
			sfxPlayer.clip = clip;
			sfxPlayer.volume = sfxVolume; // 볼륨 조절 sfxVolume
			sfxPlayer.Play();

			// 사운드 재생이 끝나면 풀에 반환
			ReturnSfxPlayerWhenFinished(sfxPlayer, clip.length).Forget();
		}
	}

	private AudioSource GetAvailableSfxPlayer()
	{
		if (_sfxQueue.Count > 0)
		{
			return _sfxQueue.Dequeue();
		}
		else
		{
			// 새 플레이어를 생성하고 리스트에 추가
			AudioSource newSFXPlayer = gameObject.AddComponent<AudioSource>();
			sfxPlayers.Add(newSFXPlayer);
			return newSFXPlayer;
		}
	}
	
	// 배경음 볼륨 조절
	public void SetBgmVolume(float volume)
	{
		// 슬라이더 값에따라 볼륨 적용
		bgmPlayer.volume = volume;

		// 슬라이더 값을 변수에 저장해서 배경음악을 실행할때마다 볼륨을 지정
		bgmVolume = volume;
	}

	// 효과음 볼륨 조절
	public void SetSfxVolume(float volume)
	{
		// 슬라이더 값을 변수에 저장해서 효과음을 실행할때마다 볼륨을 지정
		sfxVolume = volume;
	}
}
public enum SoundType
{
	MenuBGM = 0,
	FirstBGM = 1,
	SecondBGM = 2,
	PlayerWalkSfx=3,
	PlayerSwingSfx=4,
	PlayerSwingSfx2=5,
	PlayerCounterSfx3 = 6,
	PlayerJumpSfx = 7,
	PlayerCutSfx = 8,
	PlayerHitSfx=9,
	PlayerHitSfx2=10,
	PlayerElecSfx=11,
	PlayerElecSfx2=12,
	PlayerUsePotion=13,
	PlayerUseItem=14,
	PlayerGetItem=15,
	PlayerBubbleSfx=16,
	PlayerInventoryOpen=17,
	PlayerUserItem2=18,
	EnemyHit1=19,
	EnemyHit2=20,
	EnemySfx=21,
	EnemySfx2=22,
	EnemySfx3=23
}
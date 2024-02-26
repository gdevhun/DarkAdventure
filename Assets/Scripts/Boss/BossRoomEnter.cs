using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public AudioSource BossRoomEnterSound;
    public GameObject ActiveBoss;
    
    public GameObject OrdinaryVirtualCam;
    public GameObject ChangedVirtualCam;
    public GameObject bossRoomCameraEvent;
    private WaitForSeconds delayTime = new WaitForSeconds(1f);
	private WaitForSeconds delayTime1 = new WaitForSeconds(2f);
	private WaitForSeconds delayTime2 = new WaitForSeconds(6f);
    private BoxCollider2D boxCol;
	void Start()
    {
        boxCol=GetComponent<BoxCollider2D>();
        BossRoomEnterSound=GetComponent<AudioSource>();
    }
    private void PlaySoundEvent()
    {
        BossRoomEnterSound.PlayOneShot(BossRoomEnterSound.clip);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(PlayCameraEvent());
            boxCol.enabled = false;
		}
	}
	private IEnumerator PlayCameraEvent()
    {
        yield return delayTime; //1
        PlaySoundEvent();
		yield return delayTime1; //2
        OrdinaryVirtualCam.SetActive(false);
        ChangedVirtualCam.SetActive(true);
        bossRoomCameraEvent.SetActive(true);
        bossRoomCameraEvent.GetComponent<BossRoomCameraEvent>().CameraMoveEvent();
		ActiveBoss.SetActive(true);
		if (ActiveBoss.TryGetComponent(out Animator bossAnim))
		{
			bossAnim.SetTrigger("isAppeared");
		}

		yield return delayTime2; //6

		yield return delayTime; //1
		ChangedVirtualCam.SetActive(false);
        OrdinaryVirtualCam.SetActive(true);
        
        this.gameObject.SetActive(false);
	}
}

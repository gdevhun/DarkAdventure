using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SetController : MonoBehaviour
{
    public Slider bgmSlider; // 배경음 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBgmVolume); // 배경음 조절 이벤트리스너 등록
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVolume); // 효과음 조절 이벤트리스너 등록
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void ReturnToMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
   
}

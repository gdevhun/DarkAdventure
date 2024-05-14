using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SetController : MonoBehaviour
{
    public Slider bgmSlider; // ����� �����̴�
    public Slider sfxSlider; // ȿ���� �����̴�

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBgmVolume); // ����� ���� �̺�Ʈ������ ���
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVolume); // ȿ���� ���� �̺�Ʈ������ ���
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

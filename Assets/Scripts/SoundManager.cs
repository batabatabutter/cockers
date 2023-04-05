using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundID
{
    Jump,

    SoundNum
};
public enum BGMID
{
    Title,

    BGMNum
};

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField, Header("���ʉ�"), Label("����"), Range(0, 1)] float soundVol;
    [SerializeField, Label("�f��")] List<AudioClip> sound;
    [SerializeField, Label("�Đ��p")] AudioSource soundAudioSource;

    [SerializeField, Header("BGM"), Label("����"), Range(0, 1)] float bgmVol;
    [SerializeField, Label("�f��")] List<AudioClip> bgm;
    [SerializeField, Label("�Đ��p")] AudioSource bgmAudioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //  Update
    private void Update()
    {
        //  ���ʍX�V
        bgmAudioSource.volume = bgmVol;
        soundAudioSource.volume = soundVol;
    }

    //  ���ʉ��Đ�
    public void PlaySound(SoundID soundID)
    {
        soundAudioSource.PlayOneShot(sound[(int)soundID]);
    }
    
    //  ���ʉ���~
    public void StopSound()
    {
        soundAudioSource.Stop();
    }

    //  BGM�Đ�
    public void PlayBGMLoop(BGMID bGMID)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = true;
        bgmAudioSource.PlayOneShot(bgm[(int)bGMID]);
    }

    //  BGM��~
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}

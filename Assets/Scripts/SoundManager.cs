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

    [SerializeField, Header("øÊ¹"), Label("¹Ê"), Range(0, 1)] float soundVol;
    [SerializeField, Label("fÞ")] List<AudioClip> sound;
    [SerializeField, Label("Ä¶p")] AudioSource soundAudioSource;

    [SerializeField, Header("BGM"), Label("¹Ê"), Range(0, 1)] float bgmVol;
    [SerializeField, Label("fÞ")] List<AudioClip> bgm;
    [SerializeField, Label("Ä¶p")] AudioSource bgmAudioSource;


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
        //  ¹ÊXV
        bgmAudioSource.volume = bgmVol;
        soundAudioSource.volume = soundVol;
    }

    //  øÊ¹Ä¶
    public void PlaySound(SoundID soundID)
    {
        soundAudioSource.PlayOneShot(sound[(int)soundID]);
    }
    
    //  øÊ¹â~
    public void StopSound()
    {
        soundAudioSource.Stop();
    }

    //  BGMÄ¶
    public void PlayBGMLoop(BGMID bGMID)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = true;
        bgmAudioSource.PlayOneShot(bgm[(int)bGMID]);
    }

    //  BGMâ~
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}

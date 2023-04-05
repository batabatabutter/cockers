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

    [SerializeField, Header("å¯â âπ"), Label("âπó "), Range(0, 1)] float soundVol;
    [SerializeField, Label("ëfçﬁ")] List<AudioClip> sound;
    [SerializeField, Label("çƒê∂óp")] AudioSource soundAudioSource;

    [SerializeField, Header("BGM"), Label("âπó "), Range(0, 1)] float bgmVol;
    [SerializeField, Label("ëfçﬁ")] List<AudioClip> bgm;
    [SerializeField, Label("çƒê∂óp")] AudioSource bgmAudioSource;


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
        //  âπó çXêV
        bgmAudioSource.volume = bgmVol;
        soundAudioSource.volume = soundVol;
    }

    //  å¯â âπçƒê∂
    public void PlaySound(SoundID soundID)
    {
        soundAudioSource.PlayOneShot(sound[(int)soundID]);
    }
    
    //  å¯â âπí‚é~
    public void StopSound()
    {
        soundAudioSource.Stop();
    }

    //  BGMçƒê∂
    public void PlayBGMLoop(BGMID bGMID)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = true;
        bgmAudioSource.PlayOneShot(bgm[(int)bGMID]);
    }

    //  BGMí‚é~
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}

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

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("効果音"), Label("音量"), Range(0, 1)] float soundVol;
    [SerializeField, Label("素材")] List<AudioClip> sound;
    [SerializeField, Label("再生用")] AudioSource soundAudioSource;

    [SerializeField, Header("BGM"), Label("音量"), Range(0, 1)] float bgmVol;
    [SerializeField, Label("素材")] List<AudioClip> bgm;
    [SerializeField, Label("再生用")] AudioSource bgmAudioSource;

    //-------------------------------------------------------------------------------------
    // 初期化処理（継承先で必ず記述）
    //-------------------------------------------------------------------------------------
    protected override void Initialize()
    {

    }

    //  Update
    private void Update()
    {
        //  音量更新
        bgmAudioSource.volume = bgmVol;
        soundAudioSource.volume = soundVol;
    }

    //  効果音再生
    public void PlaySound(SoundID soundID)
    {
        soundAudioSource.PlayOneShot(sound[(int)soundID]);
    }
    
    //  効果音停止
    public void StopSound()
    {
        soundAudioSource.Stop();
    }

    //  BGM再生
    public void PlayBGMLoop(BGMID bGMID)
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = true;
        bgmAudioSource.PlayOneShot(bgm[(int)bGMID]);
    }

    //  BGM停止
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}

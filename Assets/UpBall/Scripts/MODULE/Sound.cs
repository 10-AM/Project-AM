using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUND
{
    S_JUMP = 0,
    S_DIE,
    S_BGM
}

public class Sound : MonoBehaviour
{
    public static Sound instance = null;

    public AudioSource[] effSource;
    public AudioSource bgmSource;
    public AudioClip[] audioClips;

    private Dictionary<SOUND, AudioClip> _soundDictionary = new Dictionary<SOUND, AudioClip>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < audioClips.Length; ++i)
        {
            _soundDictionary.Add((SOUND)i, audioClips[i]);
        }
        //SetEffMute(PlayerPrefs.GetInt("EffSound", 1));
        //SetBGMMute(PlayerPrefs.GetInt("BGMSound", 1));

        PlayBGMSound(SOUND.S_BGM);
    }

    private void Start()
    {
        PlayBGMSound(SOUND.S_BGM);

    }

    public void SetEffMute(int mute)
    {
        bool isMute = mute == 0 ? true : false;
        for (int i = 0; i < effSource.Length; ++i)
        {
            effSource[i].mute = isMute;
        }
        PlayerPrefs.SetInt("EffSound", mute);
    }

    public void SetBGMMute(int mute)
    {
        bool isMute = mute == 0 ? true : false;
        bgmSource.mute = isMute;

        PlayerPrefs.SetInt("BGMSound", mute);
    }

    public void PlayEffSound(SOUND idx)
    {
        for (int i = 0; i < effSource.Length; ++i)
        {
            if (!effSource[i].isPlaying)
            {
                effSource[i].clip = _soundDictionary[idx];
                effSource[i].Play();
            }
        }
    }

    public void PlayBGMSound(SOUND idx)
    {
        bgmSource.clip = _soundDictionary[idx];
        bgmSource.loop = true;
        bgmSource.Play();
    }




}

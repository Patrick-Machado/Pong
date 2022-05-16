using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioSource speaker;
    public AudioSource speaker2;
    public AudioSource backgroun_speaker;

    public List<AudioClip> ballpong;
    public AudioClip metalball;
    public AudioClip brickhit1;
    public AudioClip brickhit2;
    public AudioClip concretebreak;
    public AudioClip concretecrack;
    public AudioClip funnybubbles;
    public AudioClip soaring;
    public AudioClip powerup;

    public Slider BackgroundSongSlider;
    public Slider SoundsEffectsSlider;

    #region MainSoundMethods
    public void PlayBackgroundSong(AudioClip clip)
    {
        backgroun_speaker.clip = clip;
        backgroun_speaker.Play();

    }
    public void PlaySound(AudioClip clip)
    {
        speaker.clip = clip;
        speaker.Play();
    }
    public void PlaySound2(AudioClip clip)
    {
        speaker2.clip = clip;
        speaker2.Play();
    }
    #endregion

    #region SecundarySoundMethods
    public void PlayAnyPong()
    {
        int id_pongsound = Random.Range(0, ballpong.Count - 1);
        speaker.clip = ballpong[id_pongsound];
        speaker.Play();
    }


    public void PlayTitleSong()
    {
        PlayBackgroundSong(soaring);
    }

    public void PlayGameSong()
    {
        PlayBackgroundSong(funnybubbles);
    }
    #endregion


    #region AudioSlidersUpdaters
    public void updateVolumeBackground()
    {
        backgroun_speaker.volume = BackgroundSongSlider.value;
    }
    public void updateVolumeSoundEffects()
    {
        speaker.volume = SoundsEffectsSlider.value;
        speaker2.volume = SoundsEffectsSlider.value;
    }
    #endregion
}

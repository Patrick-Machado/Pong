using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void PlayAnyPong()
    {
        int id_pongsound = Random.RandomRange(0, ballpong.Count - 1);
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

}

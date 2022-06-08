using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioHandler : MonoBehaviour
{
    public AudioSource WinSound;
    public AudioSource DestroySound;
    public AudioSource MoveSound;
    private float AudioVolume = 1f;
    private void Start()
    {
        UpdateGameVolume();
        WinSound.volume = AudioVolume;
        DestroySound.volume = AudioVolume;
        MoveSound.volume = AudioVolume;
    }
    public void PlayWinSound()
    {
        PlayClip(WinSound);
    }
    public void PlayDestroySound()
    {
        PlayClip(DestroySound);
    }
    public void PlayMoveSound()
    {
        PlayClip(MoveSound);
    }
    void PlayClip(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void UpdateGameVolume()
    {
        AudioVolume = PlayerPrefs.GetFloat("volume", AudioVolume);
    }
}

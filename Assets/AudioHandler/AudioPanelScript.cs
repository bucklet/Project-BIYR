using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPanelScript : MonoBehaviour
{
    public GameObject AudioButton;
    public GameObject AudioObject;
    public Slider AudioSlider;
    public Sprite AudioButtonPlay;
    public Sprite AudioButtonPause;

    private AudioSource MusicAudioSource;
    private AudioSource ButtonClickAudioSource;
    private AudioSource ButtonSelectAudioSource;
    private float AudioVolume = 1f;
    private int musicIsPlaying = 1;
    void Start()
    {
        AudioButton.GetComponent<Button>().interactable = true;
        AudioSlider.interactable = true;
        AudioObject = GameObject.FindWithTag("MusicAudioSource");
        if (AudioObject != null)
        {
            MusicAudioSource = AudioObject.GetComponent<AudioSourceScript>().getMusicSource;
            ButtonClickAudioSource = AudioObject.GetComponent<AudioSourceScript>().getButtonClick;
            ButtonSelectAudioSource = AudioObject.GetComponent<AudioSourceScript>().getButtonSelect;
            AudioVolume = PlayerPrefs.GetFloat("volume", AudioVolume);
            musicIsPlaying = PlayerPrefs.GetInt("musicIsPlaying");
            AudioSlider.value = AudioVolume;
            if (musicIsPlaying == 1)
            {
                if (!MusicAudioSource.isPlaying)
                    MusicAudioSource.Play();
                AudioButton.GetComponent<Image>().sprite = AudioButtonPause;
            }
            else
            {
                MusicAudioSource.Pause();
                AudioButton.GetComponent<Image>().sprite = AudioButtonPlay;
            }
        }
        else
        {
            AudioButton.GetComponent<Button>().interactable = false;
            AudioSlider.interactable = false;
        }
    }
    private void Update()
    {
        if (AudioObject != null)
        {
            ButtonClickAudioSource.volume = AudioVolume;
            ButtonSelectAudioSource.volume = AudioVolume;
            MusicAudioSource.volume = AudioVolume;
            PlayerPrefs.SetFloat("volume", AudioVolume);
        }
    }
    public void ButtonClickPlayAudio()
    {
        if (ButtonClickAudioSource)
            ButtonClickAudioSource.Play();
    }
    public void ButtonSelectPlayAudio()
    {
        if (ButtonSelectAudioSource && !ButtonClickAudioSource.isPlaying)
            ButtonSelectAudioSource.Play();
    }
    public void AudioButtonPress()
    {
        if (musicIsPlaying == 0)
        {
            musicIsPlaying = 1;
            MusicAudioSource.Play();
            AudioButton.GetComponent<Image>().sprite = AudioButtonPause;
        }
        else
        {
            musicIsPlaying = 0;
            MusicAudioSource.Pause();
            AudioButton.GetComponent<Image>().sprite = AudioButtonPlay;
        }
        PlayerPrefs.SetInt("musicIsPlaying", musicIsPlaying);
    }
    public void EmergencyStop()
    {
        MusicAudioSource.Stop();
        AudioButton.GetComponent<Button>().interactable = false;
        AudioSlider.interactable = false;
    }
    public void UpdateVolume( float slideValue)
    {
        AudioVolume = slideValue;
    }
}

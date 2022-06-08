using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    public AudioSource MusicSourse;
    public AudioSource ButtonClick;
    public AudioSource ButtonSelect;
    public AudioSource getMusicSource
    {
        get { return MusicSourse; }
    }
    public AudioSource getButtonClick
    {
        get { return ButtonClick; }
    }
    public AudioSource getButtonSelect
    {
        get { return ButtonSelect; }
    }
    void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MusicAudioSource");
        if (musicObj.Length>1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

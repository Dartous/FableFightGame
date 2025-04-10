using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SoundScript : MonoBehaviour
{
    //public List<AudioSource> Tracks;
    public static SoundScript instance;
    public Sound[] sounds;
    public AudioSource soundSource;
    bool Test = true;
    public List<AudioClip> Tracks;
    public AudioSource musicSource;
    int MusicVal = 0;
    void Start()
    {
        musicSource.PlayOneShot(Tracks[MusicVal]);
        //AudioSource Starter = Tracks[1].PlayOneShot()
        //Starter
    }
    private void MusicIncrement(int Val)
    {
        if (Val < Tracks.Count)
        {
            musicSource.PlayOneShot(Tracks[Val]);
            print("We are now on soundtrack: " + Val);
        }
        else
        {
            MusicVal = 0;
            musicSource.PlayOneShot(Tracks[MusicVal]);
            print("It seems we reached the end of the queue: " + Val);
            print("Resetting the playlist");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying && Test)
        {
            print("Hello");
            Test = !Test;
            MusicVal += 1;
            MusicIncrement(MusicVal);
        }
        Test = !Test;
    }

    //function to play any sound
    public void Play(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
        soundSource.PlayOneShot(s.clip, volume);
    }
}

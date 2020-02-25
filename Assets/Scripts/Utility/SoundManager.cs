using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct SoundWithAName
    {
        public string name;
        public AudioClip[] sound;
    }

    public SoundWithAName[] SoundsWithNames;
    private Dictionary<string, AudioClip[]> SoundLib = new Dictionary<string, AudioClip[]>();

    public void Start()
    {
        foreach (SoundWithAName soundWithAName in SoundsWithNames)
        {
            SoundLib[soundWithAName.name] = soundWithAName.sound;
        }
    }

    public void Play(string sound)
    {
        // Pick a random index corrisonding to a sound
        int index = Random.Range(0, SoundLib[sound].Length);

        // Create a new game object to play the sound
        GameObject obj = new GameObject();
        obj.AddComponent<AudioSource>();
        AudioSource src = obj.GetComponent<AudioSource>();

        // play the sound
        src.clip = SoundLib[sound][index];
        src.Play();
    }
}

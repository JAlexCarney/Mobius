using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider slider;

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
        GameObject obj = new GameObject("PlayingSound");
        obj.AddComponent<AudioSource>();
        AudioSource src = obj.GetComponent<AudioSource>();

        // play the sound
        src.volume = Util.volume;
        src.clip = SoundLib[sound][index];
        src.Play();

        // Destroy object after sound ends
        Destroy(obj, src.clip.length);
    }

    public void SetVolume()
    {
        Util.volume = slider.value;
        //Play("click");
    }
}

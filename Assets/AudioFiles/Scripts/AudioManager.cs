using UnityEngine.Audio;
using System;
using UnityEngine;

// can add in sounds to other classes with the following line:
//      FindObjectOfType<AudioManager>().Play("soundName")


// list of sounds that we can easily adjust
// go through list and add audio source
// call play method with name of sound to play the sound

public class AudioManager : MonoBehaviour
{
    // list of sounds that we will be using
    public Sound[] sounds;

    public static AudioManager instance;
        
    // array with the names of the zombie sfx sounds
    private static readonly string[] ZOMBIE_SFX = { "Zombie1", "Zombie2", "Zombie3"};
    int currentClipIndex = 0;

    // Used for initialization
    void Awake()
    {
        // check to see if there is already an audio manager in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // This line ensures that the audio manager is preserved throughout all scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }
    }
    
    void Start ()
    {
        Play("Theme");
    }

    // this method plays a sound with the matching name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " could not be found");
            return;
        }

        s.source.Play();
    }

    // This method uses round robin w/ pitch randomization for zombie sfx
    public void PlayZombie()
    {
        currentClipIndex++;
        currentClipIndex %= ZOMBIE_SFX.Length;
     
        Sound s = Array.Find(sounds, sounds => sounds.name == ZOMBIE_SFX[currentClipIndex]);
        s.source.pitch = UnityEngine.Random.Range(0.9f, 1.3f);
        
        s.source.Play();
    }

    public void adjustVolume(float newVolume)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == "Theme");
        s.source.volume = newVolume;
    }
}

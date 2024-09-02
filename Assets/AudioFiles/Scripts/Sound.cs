using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

// This class controls the data that each sound stores
public class Sound
{
    public string name;    
    public AudioClip clip; // reference to audioclip file

    // volume and pitch with associated ranges
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool loop; 

    [HideInInspector]
    public AudioSource source;
    
}

using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    #region variables
    public string name;

    public AudioClip clip;
    public AudioSource source;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    #endregion
}
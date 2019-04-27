using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string clipName;
    [SerializeField] private AudioClip clip;

    [SerializeField] private bool loop;

    [Range(0,1)]
    [SerializeField] private float volume;

    [Range(0,1)]
    [SerializeField] private float spatialBlend;

    private AudioSource source;
    
    public string ClipName
    {
        get { return clipName; }
        set { clipName = value; }
    }
    public AudioClip Clip
    {
        get { return clip; }
        set { clip = value; }
    }
    public AudioSource Source
    {
        get { return source; }
        set { source = value; }
    }
    public float Volume
    {
        get { return volume; }
        set { volume = value; }
    }
    public bool Loop
    {
        get { return loop; }
        set { loop = value; }
    }
    public float SpatialBlend
    {
        get { return spatialBlend; }
        set { spatialBlend = value; }
    }
}

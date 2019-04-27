using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
			DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.loop = sound.Loop;
            sound.Source.spatialBlend = sound.SpatialBlend;
        }
    }

    public void PlaySound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.ClipName == soundName)
            {
                sound.Source.Play();
                return;
            }
        }
        Debug.LogWarning(string.Format("El clip {0} no corresponde con los clips almacenados", soundName));
    }

    public void StopSound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.ClipName == soundName)
            {
                sound.Source.Stop();
                return;
            }
        }
        Debug.LogWarning(string.Format("El clip {0} no corresponde con los clips almacenados", soundName));
    }
}

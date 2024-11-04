using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    public void SetMusic(float volume)
    {
        foreach (var source in sources)
        {
            source.volume = volume;
        }
    }
}

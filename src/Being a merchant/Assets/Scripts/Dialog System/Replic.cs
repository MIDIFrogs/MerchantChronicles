using System;
using UnityEngine;

namespace SibGameJam.DialogSystem
{
    [Serializable]
    public class Replic
    {
        [SerializeField] private ReplicAuthor author;
        [SerializeField][Multiline] private string message;
        [SerializeField] private AudioClip voice;
        [SerializeField] private Sprite frameSplash;

        public ReplicAuthor Author => author;

        public AudioClip Voice => voice;

        public string Message => message;

        public Sprite FrameSplash => frameSplash;
    }
}

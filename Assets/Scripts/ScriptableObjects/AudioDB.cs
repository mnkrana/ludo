using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ludo.ScriptableObjects
{
    [Serializable]
    public enum AudioName
    {
        TURN,
        ROLL,
        MOVE,
        DICE,
        KILL,
        GOAL,
        TAP,
        GOTI_TAP
    }

    [Serializable]
    public class Audio
    {
        public AudioName Name;
        public AudioClip Clip;
        public float Volume;
    }

    [CreateAssetMenu(menuName = "ludo/AudioDB")]
    public class AudioDB : ScriptableObject
    {
        public List<Audio> audios;

        public Audio GetAudio(AudioName audioName)
        {
            return audios.Find( x => x.Name == audioName);
        }
    }
}
// Code by Graham Tattersall from https://habr.com/ru/articles/452168/
using UnityEngine;

namespace SibGameJam.Minigames
{
	public class Conductor : MonoBehaviour
	{
        //The number of seconds for each song beat
        [SerializeField] private float secPerBeat;

        //Current song position, in seconds
        [SerializeField] private float songPosition;

        //Current song position, in beats
        [SerializeField] private float songPositionInBeats;

        //How many seconds have passed since the song started
        [SerializeField] private float dspSongTime;

        //Song beats per minute
        //This is determined by the song you're trying to sync up to
        public float SongBPM { get; set; }

        //The offset to the first beat of the song in seconds
        public float FirstBeatOffset { get; set; }

        public float SongPosition => songPosition;

        public float SongPositionInBeats => songPositionInBeats;

        public void Begin()
        {
            //Calculate the number of seconds in each beat
            secPerBeat = 60f / SongBPM;

            Clear();
        }

        public void Clear()
        {
            dspSongTime = (float)AudioSettings.dspTime;
            songPosition = songPositionInBeats = 0;
        }

        void Update()
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - FirstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;
        }
    }
}
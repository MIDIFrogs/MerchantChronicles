using System;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
	[Serializable]
	public struct BeatTick
	{
		[SerializeField] private float tickTime;
		[SerializeField] private bool isRight;

		public readonly float TickTime => tickTime;
		public readonly bool IsRight => isRight;
	}

	[CreateAssetMenu(fileName = "New Beatmap", menuName = "Minigames/Anvil beatmap")]
	public class Beatmap : MinigameTask
	{
		[SerializeField] private AudioClip audioWin;
		[SerializeField] private AudioClip audioLose;
		[SerializeField] private AudioClip audioBegin;
		[SerializeField] private float approachRate;
		[SerializeField] private List<BeatTick> beatTicks;
		[SerializeField] private float songBPM;
		[SerializeField] private float firstBeatOffset;

		public AudioClip AudioWin => audioWin;
		public AudioClip AudioLose => audioLose;
		public float ApproachRate => approachRate;
		public AudioClip AudioBegin => audioBegin;
		public float SongBPM => songBPM;
		public float FirstBeatOffset => firstBeatOffset;
		public IReadOnlyList<BeatTick> BeatTicks => beatTicks;
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
	[Serializable]
	public struct BeatTick
	{
		[SerializeField] private TimeSpan tickTime;
		[SerializeField] private bool isRight;

		public readonly TimeSpan TickTime => tickTime;
		public readonly bool IsRight => isRight;
	}

	public class Beatmap : MinigameTask
	{
		[SerializeField] private AudioClip audioWin;
		[SerializeField] private AudioClip audioLose;
		[SerializeField] private AudioClip audioBegin;
		[SerializeField] private float approachRate;
		[SerializeField] private List<BeatTick> beatTicks;

		public AudioClip AudioWin => audioWin;
		public AudioClip AudioLose => audioLose;
		public float ApproachRate => approachRate;
		public AudioClip AudioBegin => audioBegin;
		public IReadOnlyList<BeatTick> BeatTicks => beatTicks;
	}
}
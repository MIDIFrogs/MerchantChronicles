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
		[SerializeField] private AudioClip audio;
		[SerializeField] private float soundSpeed;
		[SerializeField] private List<BeatTick> beatTicks;
	}
}
using UnityEngine;

namespace SibGameJam.Minigames
{
    public class OsuAnvilGame : Minigame
    {
        [SerializeField] private BeatmapRunner beatmapRunner;

        private Beatmap beatmap;

        protected override void OnGameFinished(bool success)
        {
            // TODO
        }

        protected override void OnGameStarted()
        {
            try
            {
                beatmap = (Beatmap)Level;
                MinigameMusic.clip = beatmap.AudioBegin;
                beatmapRunner.StartGame(beatmap, MinigameMusic);
                beatmapRunner.GameEnd += OnGameEnd;
            }
            catch
            {
                EndGame(false);
            }
        }

        private void OnGameEnd(object sender, bool e)
        {
            EndGame(e);
        }
    }
}
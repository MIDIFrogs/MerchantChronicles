namespace SibGameJam.Minigames
{
    public class OsuAnvilGame : Minigame
    {
        protected override void OnGameFinished(bool success)
        {
            // TODO
        }

        protected override void OnGameStarted()
        {
            // HACK
            EndGame(true);
        }
    }
}
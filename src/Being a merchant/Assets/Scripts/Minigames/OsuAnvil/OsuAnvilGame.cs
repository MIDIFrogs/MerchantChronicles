namespace SibGameJam.Minigames
{
    public class OsuAnvilGame : Minigame
    {
        protected override void OnGameFinished()
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
using SibGameJam.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SibGameJam
{
	public enum GameResult
	{
		Fail = -2,
		Bad = -1,
		Neutral = 0,
		Good = 1
	}

	public class GameSession
	{
		public static readonly SceneList SceneList = Resources.Load<SceneList>("PlayableSceneList");

		public static bool SessionExists => PlayerPrefs.HasKey(nameof(Reputation));

		public int Reputation { get; private set; }

		public int CurrentScene { get; private set; }

		public int Coins { get; private set; }

		public int TargetCoins { get; set; }

		public PlayerInventory Inventory { get; set; }

		public void LevelCompleted(GameResult result, int coinsEarned)
		{
			Debug.Log($"Navigating between scenes. New scene: {CurrentScene}.");
			Debug.Log($"Earned reputation: {(int)result}, coins: {coinsEarned}.");
			CurrentScene++;
			Reputation += (int)result;
			Coins += coinsEarned;
			Save();
			SceneManager.LoadScene(SceneList.SceneNames[CurrentScene]);
		}

		public void Save()
		{
			PlayerPrefs.SetInt(nameof(Reputation), Reputation);
			PlayerPrefs.SetInt(nameof(CurrentScene), CurrentScene);
			PlayerPrefs.SetInt(nameof(Coins), Coins);
		}

		public static GameSession LoadOrCreate()
		{
			if (PlayerPrefs.HasKey(nameof(Reputation)))
			{
				int reputation = PlayerPrefs.GetInt(nameof(Reputation));
				int currentScene = PlayerPrefs.GetInt(nameof(CurrentScene));
				int money = PlayerPrefs.GetInt(nameof(Coins));
				return new GameSession()
				{
					Reputation = reputation,
					CurrentScene = currentScene
				};
			}
			else
			{
				return new GameSession();
			}
		}

		public static void DeleteSession()
		{
			PlayerPrefs.DeleteKey(nameof(Reputation));
			PlayerPrefs.DeleteKey(nameof(CurrentScene));
			PlayerPrefs.DeleteKey(nameof(Coins));
		}
	}
}
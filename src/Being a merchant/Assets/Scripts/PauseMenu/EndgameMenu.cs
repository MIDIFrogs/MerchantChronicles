using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SibGameJam
{
	public class EndgameMenu : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI rep;
		[SerializeField] private TextMeshProUGUI coins;

		[SerializeField] private GameObject win;
		[SerializeField] private GameObject lose;

		[SerializeField] private AudioSource aourse;
		[SerializeField] private AudioClip goodSound;
		[SerializeField] private AudioClip badSound;

		public void Start()
		{
			rep.text = PlayerStats.Session.Reputation.ToString();
			coins.text = PlayerStats.Session.Coins.ToString();
			if (PlayerStats.Session.Coins > 100 && PlayerStats.Session.Reputation > 0)
			{
				win.SetActive(true);
				aourse.clip = goodSound;
			}
			else
			{
				lose.SetActive(true);
				aourse.clip = badSound;
			}
			aourse.Play();
			GameSession.DeleteSession();
			StartCoroutine(BackToMenu());
		}

		public IEnumerator BackToMenu()
		{
			yield return new WaitForSeconds(20);
			SceneManager.LoadScene("Credits");
		}
	}
}
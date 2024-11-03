using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam
{
	public class SceneList : ScriptableObject
	{
		[SerializeField] private List<string> sceneNames = new();

		public List<string> SceneNames => sceneNames;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public Inventory.ItemInfo item;
        public AudioSource clickSound;
        public void OnClick()
        {
            clickSound.Play();
            Debug.Log(PlayerStats.Inventory.TryAddItem(item));
            Destroy(gameObject);
        }
    }
}

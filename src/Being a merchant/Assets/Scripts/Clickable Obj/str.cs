using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class str : MonoBehaviour
{
    [SerializeField] private GameObject Newpage;
    [SerializeField] private GameObject Oldpage;

    public void OnClick()
    {
        Oldpage.SetActive(false);
        Newpage.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private List<GameObject> pages;

    public void Start()
    {
        panel.SetActive(false);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
    }
    public void OnClick()
    {
        panel.SetActive(true);
        pages[0].SetActive(true);
    }

    public void Exit() 
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        panel.SetActive(false);
    }
}

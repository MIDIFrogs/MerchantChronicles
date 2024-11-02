using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Point : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float animationDuration;
    public float size;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Animate(size));
        Debug.Log("Навелись!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Animate(-size));
        Debug.Log("Не навелись");
    }

    public IEnumerator Animate(float c)
    {
        float timeCounter = 0;
        while (timeCounter < animationDuration)
        {
            transform.localScale += Vector3.one * c / animationDuration * Time.deltaTime;
            timeCounter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
     }
}
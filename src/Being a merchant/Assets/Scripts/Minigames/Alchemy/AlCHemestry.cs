using SibGameJam.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SibGameJam
{
    public class AlCHemestry : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Transform gridPanel;
        [SerializeField] private Transform dropPanel;
        [SerializeField] private GameObject panel;
        [SerializeField] private Button Result;
        private Vector3 originalPosition;
        public Canvas canvas;
        private GameObject draggedImage;
        private GameObject newImage;
        private List<ItemInfo> result = new List<ItemInfo>();
        private List<GameObject> all= new List<GameObject>();
        public List<Poison> poisons;
        public ItemInfo XerZnaetChto; 

        public void Start()
        {
            panel.SetActive(false);
        }


        public void AddImages()
        {
            foreach (var item in PlayerStats.Session.Inventory)
            {
                newImage = Instantiate(imagePrefab, gridPanel);
                newImage.GetComponent<Image>().sprite = item.Icon;
                newImage.GetComponent<ImageDragHandler>().imageManager = this;
                newImage.GetComponent<ImageDragHandler>().canvas = canvas;
                newImage.GetComponent<ImageDragHandler>().itemInfo = item;
                all.Add(newImage);
    }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBegin");
            if (eventData.pointerDrag != null)
            {
                Debug.Log("OnBegin2");
                originalPosition = transform.position;
                draggedImage = eventData.pointerDrag;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggedImage != null)
            {
                Debug.Log("OnDrag");
                RectTransform rt = draggedImage.GetComponent<RectTransform>();
                rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (draggedImage != null)
            {
                Debug.Log("EndDrag");
                if (RectTransformUtility.RectangleContainsScreenPoint(dropPanel.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
                {
                    result.Add(draggedImage.GetComponent<ImageDragHandler>().itemInfo);
                    PlayerStats.Session.Inventory.TryRemoveItem(draggedImage.GetComponent<ImageDragHandler>().itemInfo);
                    Destroy(draggedImage);
                    
                }
                else
                {
                    Debug.Log(Input.mousePosition);
                    Debug.Log(dropPanel.GetComponent<RectTransform>());
                    transform.position = originalPosition;
                }
                draggedImage = null;
            }
        }

        public void ResultOnClick()
        {
            bool flag = false; 
            if (result.Count == 3) {
                foreach (var burda in poisons)
                {
                    if (result[0] == burda.First && result[1] == burda.Second && result[2] == burda.Third)
                    {
                        PlayerStats.Session.Inventory.TryAddItem(burda.PoisonR);
                        FinishGame();
                        flag = true; 
                        break;
                    }
                }
                if(!flag)
                {
                    FinishGame();
                    PlayerStats.Session.Inventory.TryAddItem(XerZnaetChto);
                }
            }
            else
            {
                FinishGame();
                PlayerStats.Session.Inventory.TryAddItem(XerZnaetChto);
            }

            foreach (var prf in all)
            {
                Destroy(prf);
            }
        }

        public void OnClick()
        {
            panel.SetActive(true);
            AddImages();

        }

        public void FinishGame()
        {
            panel.SetActive(false);
        }
    }
}
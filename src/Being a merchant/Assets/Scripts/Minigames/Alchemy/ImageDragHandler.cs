using UnityEngine;
using UnityEngine.EventSystems;
using SibGameJam.Inventory;

namespace SibGameJam
{
    public class ImageDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Canvas canvas;
        public AlCHemestry imageManager;
        public ItemInfo itemInfo;

        public void OnBeginDrag(PointerEventData eventData)
        {
            imageManager.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            imageManager.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            imageManager.OnEndDrag(eventData);
        }
    }
}
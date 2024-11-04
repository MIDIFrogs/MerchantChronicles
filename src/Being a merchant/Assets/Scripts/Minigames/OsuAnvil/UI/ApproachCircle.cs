using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames
{
    public class ApproachCircle : MonoBehaviour
    {
        [SerializeField] Image circleImage;
        [SerializeField] Color leftColor;
        [SerializeField] Color rightColor;

        private float centerPos;

        public float Speed { get; set; }

        public bool IsRightSide { get; set; }

        public Transform From { get; set; }

        public Transform To { get; set; }

        public float TimeTick { get; set; }

        public float PosX
        {
            get => transform.position.x;
            set
            {
                transform.position = new(value, centerPos);
            }
        }

        public float FadeOutTime { get; set; }

        public event System.EventHandler Approached;

        public IEnumerator Approach()
        {
            centerPos = (From.position.y + To.position.y) / 2;
            circleImage.color = IsRightSide ? rightColor : leftColor;
            PosX = From.position.x;
            float distance = Mathf.Abs(PosX - centerPos);
            float time = 0;
            while (IsRightSide ? (PosX > centerPos) : (PosX < centerPos))
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                PosX = Mathf.Lerp(From.position.x, To.position.x, time / Speed);
            }
            time = 0;
            while (time < FadeOutTime)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                circleImage.color = new(1, 1, 1, Mathf.Lerp(1, 0, time / FadeOutTime));
            }
            circleImage.color = default;
            Approached?.Invoke(this, System.EventArgs.Empty);
        }
    }
}

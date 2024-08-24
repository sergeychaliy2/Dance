using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

namespace UGUIAnimationSamples
{
    public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] Image fill;
        [SerializeField] TMP_Text label;

        [Header("Settings")]
        [SerializeField] Color hoverLabelColor = Color.white;
        [SerializeField] Ease ease = Ease.OutSine;
        [SerializeField] float duration = 0.1f; // ��������� �������� ��� ��������� ��������

        private Color initialLabelColor;
        private CompositeMotionHandle motionHandles = new(2);
        private bool isHovered = false;
        private bool isPointerInside = false;

        private RectTransform rectTransform;

        void Awake()
        {
            initialLabelColor = label.color;
            rectTransform = GetComponent<RectTransform>();

            // ������������� ��������� ���������
            fill.transform.localScale = Vector3.zero; // ��� Vector3.one, ���� ��������� ������� ����� 1
            label.color = initialLabelColor;
        }

        void OnDestroy()
        {
            motionHandles.Cancel();
        }

        void Update()
        {
            // ���������, ��������� �� ������ ������ ������
            if (isPointerInside && !RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main))
            {
                OnPointerExit(null);
            }
            else if (!isPointerInside && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main))
            {
                OnPointerEnter(null);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isHovered) return;
            isHovered = true;
            isPointerInside = true;

            // ������������� ����� ������� ��������
            motionHandles.Cancel();

            // ������� ���������� �������
            LMotion.Create(fill.transform.localScale, Vector3.one, duration)
                .WithEase(ease)
                .BindToLocalScale(fill.transform)
                .AddTo(motionHandles);

            // ������� ��������� ����� ������
            LMotion.Create(label.color, hoverLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isHovered) return;
            isHovered = false;
            isPointerInside = false;

            // ������������� ����� ������� ��������
            motionHandles.Cancel();

            // ������� ���������� �������
            LMotion.Create(fill.transform.localScale, Vector3.zero, duration)
                .WithEase(ease)
                .BindToLocalScale(fill.transform)
                .AddTo(motionHandles);

            // ������� ��������� ����� ������
            LMotion.Create(label.color, initialLabelColor, duration)
                .WithEase(ease)
                .BindToColor(label)
                .AddTo(motionHandles);
        }
    }
}

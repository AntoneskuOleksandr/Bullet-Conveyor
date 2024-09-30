using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ScaleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float scaleFactor = 0.9f;
    [SerializeField] private float animationDuration = 0.1f;

    private Vector3 originalScale;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null && button.interactable)
            transform.DOScale(originalScale * scaleFactor, animationDuration);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (button != null)
            transform.DOScale(originalScale, animationDuration);
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TouchBounce : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public float _startScale = 1f;
    [SerializeField] public float _endScale = 1.2f;
    [SerializeField] public float _duration = 0.5f;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        _rectTransform.localScale = new Vector3(_startScale, _startScale, 1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.DOScale(new Vector3(_endScale, _endScale, 1f), _duration)
            .SetUpdate(true)
            .SetEase(Ease.OutBounce);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.DOScale(new Vector3(_startScale, _startScale, 1f), _duration)
            .SetUpdate(true)
            .SetEase(Ease.OutBounce);
    }
}

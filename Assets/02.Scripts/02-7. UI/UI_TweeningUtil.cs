using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_TweeningUtil
{
    public void Slide(ref RectTransform target, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        target.DOAnchorPosX(target.anchoredPosition.x + scalarSlideAnimation, slideTime)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }
    public void SlideIn(ref RectTransform left, ref RectTransform right, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        left.DOAnchorPosX(left.anchoredPosition.x + scalarSlideAnimation, slideTime);
        right.DOAnchorPosX(right.anchoredPosition.x - scalarSlideAnimation, slideTime)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }
    public void SlideOut(ref RectTransform left, ref RectTransform right, float slideTime, float scalarSlideAnimation, params System.Action[] onCompleteActions)
    {
        left.DOAnchorPosX(left.anchoredPosition.x - scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad);
        right.DOAnchorPosX(right.anchoredPosition.x + scalarSlideAnimation, slideTime).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                foreach (var action in onCompleteActions)
                {
                    action?.Invoke();
                }
            });
    }

    public void Fade(ref TextMeshProUGUI tmp, float targetFadeValue, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(targetFadeValue, fadeTime).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
    public void FadeIn(ref TextMeshProUGUI tmp, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(1f, fadeTime).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
    public void FadeOut(ref TextMeshProUGUI tmp, float fadeTime, params System.Action[] onCompleteActions)
    {
        tmp.DOFade(0f, fadeTime).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
}

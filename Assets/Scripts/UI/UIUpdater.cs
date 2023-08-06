using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUpdater : MonoBehaviour
{
    public Image uiImage;
    public Ease ease = Ease.OutBack;
    public float duration = .1f;

    private Tween _currentTween;

    private void OnValidate()
    {
        if(uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        UpdateImage(f);
    }

    public void UpdateValue( float max, float current)
    {
        UpdateImage((current / max));
    }

    private void UpdateImage(float f)
    {
        if (_currentTween != null) _currentTween.Kill();
        _currentTween = uiImage.DOFillAmount(f, duration).SetEase(ease);
    }
}

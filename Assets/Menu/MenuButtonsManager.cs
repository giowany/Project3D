using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;
    [Header("Animation Setings")]
    public float duration = .5f;
    public float delay = .5f;
    public Ease ease;

    private Tween _curTween;

    private void OnEnable()
    {
        AnimationButtons();
    }

    private void OnDisable()
    {
        HideButtons();
    }

    private void Awake()
    {
        HideButtons();
    }

    private void HideButtons()
    {
        if (_curTween != null) _curTween.Kill();

        foreach (var button in buttons)
        {
            button.transform.localScale = Vector3.zero;
            button.SetActive(false);
        }
    }

    private void AnimationButtons()
    {
        if(_curTween != null) _curTween.Kill();

        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            button.SetActive(true);
            _curTween = button.transform.DOScale(1, duration).SetEase(ease).SetDelay(i * delay);
        }
    }
}

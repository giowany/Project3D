using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCoin : MonoBehaviour
{
    public float timeToAnimation = 1f;
    public float timeBetweenAnimation = 1f;
    public Transform renderCoin;
    public Tween tween;
    public Vector3 rotate;

    public void RotateCoin(Vector3 rotate)
    {
        renderCoin.DORotate(rotate, timeToAnimation);
    }

    public IEnumerator RotateCoinCorotine()
    {
        while (true)
        {
            renderCoin.rotation = new Quaternion(0f, 0f, 0f, 0f);
            RotateCoin(rotate);
            yield return new WaitForSeconds(timeBetweenAnimation);
        }
    }

    private void Start()
    {
        StartCoroutine(RotateCoinCorotine());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIEffect : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool isAnimating = false;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        OpenUI();
    }

    private void OnDisable()
    {
        if (isAnimating)
        {
            // 애니메이션이 완료되지 않았다면 애니메이션 중지 및 크기 초기화
            DOTween.Kill(rectTransform);
            rectTransform.localScale = Vector3.zero;

            
            isAnimating = false;
        }

        GameManager.instance.isUI = false;
    }

    public void OpenUI()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;

        if (isAnimating) return;
        isAnimating = true;

        rectTransform.localScale = Vector3.zero;
        rectTransform.DOScale(Vector3.one, 1.0f)
                     .SetEase(Ease.OutBack)
                     .OnComplete(() => {
                         GameManager.instance.isUI = true;
                         isAnimating = false;
                     });
    }

    public void CloseUI()
    {
        if (isAnimating) return;
        isAnimating = true;

        rectTransform.DOScale(Vector3.zero, 1.0f)
                     .OnComplete(() => {
                          // UI가 닫힐 때 isUI를 false로 변경
                     isAnimating = false;
                         gameObject.SetActive(false);
                     });
    }
}
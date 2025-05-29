using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public static UIEffectManager Instance;

    public GameObject pointa20Prefab;
    public GameObject pointe20Prefab;
    public Canvas canvas; // UI içinde instantiatelenmesi için

    public GameObject plus5Prefab;  // +5 saniye efekti için prefab
    public RectTransform timeTextUI; // Süreyi gösteren Text UI elemaný (Inspector'dan atanacak)

    private void Awake()=>Instance = this;

    public void ShowFloatingUI(bool isPositive, Vector3 screenPos)
    {
        GameObject prefab = isPositive ? pointa20Prefab : pointe20Prefab;
        GameObject uiObject = Instantiate(prefab, canvas.transform);

        uiObject.transform.position = screenPos;

        // CanvasGroup varsa daha iyi kontrol ederiz
        CanvasGroup cg = uiObject.GetComponent<CanvasGroup>();
        if (cg == null) cg = uiObject.AddComponent<CanvasGroup>();
        cg.alpha = 1;

        // Baþlangýç scale
        uiObject.transform.localScale = Vector3.one;

        // Animasyonlar
        Sequence seq = DOTween.Sequence();
        seq.Append(uiObject.transform.DOMoveY(screenPos.y + 50f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(uiObject.transform.DOScale(1.7f, 0.3f).SetLoops(2, LoopType.Yoyo));
        seq.Join(cg.DOFade(0, 0.7f));

        // Bitince yok et
        seq.OnComplete(() => Destroy(uiObject));
    }
    public void ShowPlus5Seconds()
    {
        GameObject uiObject = Instantiate(plus5Prefab, canvas.transform);

        // Zaman UI'sinin hemen altýna pozisyonla
        Vector3 worldPos = timeTextUI.position - new Vector3(0, 50f, 0); // 50px aþaðýsý
        uiObject.transform.position = worldPos;

        CanvasGroup cg = uiObject.GetComponent<CanvasGroup>();
        if (cg == null) cg = uiObject.AddComponent<CanvasGroup>();
        cg.alpha = 1;
        uiObject.transform.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();
        seq.Append(uiObject.transform.DOMoveY(worldPos.y + 40f, 0.8f).SetEase(Ease.OutQuad));
        seq.Join(uiObject.transform.DOScale(1.2f, 0.3f).SetLoops(2, LoopType.Yoyo));
        seq.Join(cg.DOFade(0, 0.7f));
        seq.OnComplete(() => Destroy(uiObject));
    }

   
}

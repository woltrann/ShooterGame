using UnityEngine;
using DG.Tweening;

public class Vawe : MonoBehaviour
{
    public Transform[] wavePieces; // 4 tane parçayý sýrayla at

    public float moveAmount = 0.5f;
    public float duration = 0.5f;

    void Start()
    {
        for (int i = 0; i < wavePieces.Length; i++)
        {
            int direction = (i < wavePieces.Length / 2) ? 1 : -1; // Ýlk 2 saða, diðer 2 sola

            wavePieces[i].DOLocalMoveX(wavePieces[i].localPosition.x + direction * moveAmount, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetDelay(i * 0.05f); // Hafif gecikme efekti (opsiyonel)
        }
    }
}

using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Audio;

public class Rotate : MonoBehaviour
{
    private bool rotated = false;
    public int prefabIndex; // Bu objenin dizideki sýrasý

    private void OnMouseDown()
    {
        if (VFXSpawner.Instance.bulletCount > 0 && !rotated)
        {
            if (VFXSpawner.Instance.settingsPanel.activeSelf)
                return;

            float targetY = rotated ? 0f : -180f;
            rotated = !rotated;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DORotate(new Vector3(0, targetY, 0), 0.5f, RotateMode.Fast).SetEase(Ease.InOutSine));        // 1. önce dönüþ
            seq.Append(transform.DOMoveY(-1.5f, 0.4f).SetEase(Ease.InQuad));        // 2. sonra aþaðý inme


            Vector3 mousePos = Input.mousePosition;

            if (prefabIndex > 2 && prefabIndex<14)
            {
                VFXSpawner.Instance.AddScore(20f);
                VFXSpawner.Instance.AddProgress(0.23f - (VFXSpawner.Instance.currentLevel / 50f));

                UIEffectManager.Instance.ShowFloatingUI(true, mousePos);
            }
            else if(prefabIndex<=2)
            {
                VFXSpawner.Instance.AddScore(-20f);

                UIEffectManager.Instance.ShowFloatingUI(false, mousePos);
            }
            else
            {
                GameTimer.Instance.StartTme();
                TargetSpawner.Instance.StartGame();
                VFXSpawner.Instance.ShowSettingsButton();
            }
        }
    }
}

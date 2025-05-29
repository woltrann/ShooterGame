using UnityEngine;
using DG.Tweening;

public class WaveMove : MonoBehaviour
{
    public float moveDistance = 10f;
    public float moveDuration = 5f;
    public float waveAmplitude = 1f;
    public float waveFrequency = 2f;

    private float startY;
    private float moveStartTime;

    void Start()
    {
        startY = transform.position.y;
        moveStartTime = Time.time;

        // Sadece saða doðru hareket
        transform.DOMoveX(transform.position.x + moveDistance, moveDuration).SetEase(Ease.Linear);
    }

    void Update()
    {
        // Yalnýzca yukarý-aþaðý salýným
        float elapsed = Time.time - moveStartTime;
        float newY = startY + Mathf.Sin(elapsed * waveFrequency * Mathf.PI * 2f) * waveAmplitude;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;
    }
}

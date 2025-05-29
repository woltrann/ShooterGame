using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && VFXSpawner.Instance.bulletCount > 0) // Sol t�k
        {
            audioSource.Play();
        }
    }
}

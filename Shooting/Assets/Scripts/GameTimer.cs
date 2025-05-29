using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    public GameObject GOPanel;
    public float totalTime = 60f; // Baþlangýç süresi
    public float currentTime;
    private bool isRunning = true;

    public TextMeshProUGUI timerText; // UI Text nesnesi atanacak (isteðe baðlý)

    private void Awake()=> Instance = this;
    public void StartTme()
    {
        currentTime = totalTime;
        UpdateTimerUI();
        InvokeRepeating(nameof(DecreaseTime), 1f, 1f); // Her saniye bir azalt
    }

    void DecreaseTime()
    {
        if (!isRunning) return;

        currentTime -= 1f;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            CancelInvoke(nameof(DecreaseTime));
            OnTimeOver();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString(); // UI'ya yaz
        }
    }

    void OnTimeOver()
    {
        Debug.Log("Zaman bitti!");
        GOPanel.SetActive(true);
        GamoOverPanel.Instance.GameOver();
        // Buraya oyun bitince ne olacaksa onu yaz (panel aç, sahne deðiþtir, vs.)
    }

    public void StopTimer()
    {
        isRunning = false;
        CancelInvoke(nameof(DecreaseTime));

    }

    public void ResumeTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            InvokeRepeating(nameof(DecreaseTime), 1f, 1f);
        }
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
        UpdateTimerUI();
        Debug.Log("süre eklendi0");
    }
}

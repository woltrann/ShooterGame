using TMPro;
using UnityEngine;

public class GamoOverPanel : MonoBehaviour
{
    public static GamoOverPanel Instance;
    public float Score;
    public float HighScore;
    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI HighScoreText;
    private void Awake() => Instance = this;

    public void GameOver()
    {
        Score = VFXSpawner.Instance.score;
        // 1. Eski high score'u �ek
        HighScore = PlayerPrefs.GetFloat("HighScore", 0);

        // 2. Yeni skor daha y�ksekse kaydet
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetFloat("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        // 3. UI'ya yazd�r
        Scoretext.text = "Skor: " + Score.ToString("0");
        HighScoreText.text = "Y�ksek Skor: " + HighScore.ToString("0");
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

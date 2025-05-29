using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VFXSpawner : MonoBehaviour
{
    public static VFXSpawner Instance;
    public GameObject vfxPrefab;  // Inspector'dan atayacaksýn
    public int bulletCount = 7;

    public GameObject[] images;  // 6 Image GameObject'ini inspector'a sýrayla ekle
    private int currentIndex = 0;

    public RectTransform crosshairRect;

    public Image fillImage; // Radial image atanacak
    public float currentProgress = 0.0f;
    public float currentLevel = 1;
    public float score = 0f; // Skor
    public TextMeshProUGUI levelText;       // (Ýsteðe baðlý) Level gösteren text
    public TextMeshProUGUI scoreText;


    public AudioMixer audioMixer;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public GameObject settingsPanel;
    public GameObject settingsButton;



    private void Awake()=> Instance = this;      
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        crosshairRect.position = mousePos;
        fillImage.fillAmount = currentProgress;

        MusicSlider.onValueChanged.AddListener((value) => audioMixer.SetFloat("BGMusicVolume", Mathf.Log10(value) * 20));
        SFXSlider.onValueChanged.AddListener((value) => audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20));

        if (Input.GetMouseButtonDown(0))
        {
            if (VFXSpawner.Instance.settingsPanel.activeSelf)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (bulletCount > 0)
                {
                    Instantiate(vfxPrefab, hit.point, Quaternion.identity);


                    if (currentIndex < images.Length)
                    {
                        images[currentIndex].SetActive(false);
                        currentIndex++;
                    }
                    bulletCount--;
                }

            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    public void ShowSettingsButton()
    {
        settingsButton.SetActive(!settingsButton.activeSelf);
    }
    public void AddProgress(float amount)
    {
        currentProgress += amount;
        if (currentProgress >= 1f && GameTimer.Instance.currentTime>=1)
        {
            LevelUp();
        }
        else
        {
            fillImage.fillAmount = currentProgress;
        }
    }
    void LevelUp()
    {
        currentLevel++;
        currentProgress = 0f;
        fillImage.fillAmount = 0f;
        UIEffectManager.Instance.ShowPlus5Seconds();
        GameTimer.Instance.AddTime(5f); // +5 saniye ekler

        UpdateUI();
    }
    void UpdateUI()
    {
        if (levelText != null)
            levelText.text = currentLevel.ToString();

        scoreText.text= score.ToString();
    }

    public void AddScore(float puan)
    {
        score+= puan;
        UpdateUI();
    }

    public void Reload()
    {
        if (VFXSpawner.Instance.settingsPanel.activeSelf)
            return;
        bulletCount = 7;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(true);
        }
        currentIndex = 0;
    }
}

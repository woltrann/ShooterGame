using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public static TextUpdater Instance;

    public InputField[] inputFields; // 12 input field

    private string[] savedTexts;

    private const string PlayerPrefsKeyPrefix = "InputFieldText_";

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        savedTexts = new string[inputFields.Length];

        // PlayerPrefs'ten yükle
        for (int i = 0; i < inputFields.Length; i++)
        {
            string key = PlayerPrefsKeyPrefix + i;
            string savedValue = PlayerPrefs.GetString(key, "");
            savedTexts[i] = savedValue;

            inputFields[i].text = savedValue; // InputField'ý da güncelle

            int index = i; // closure için local deðiþken

            inputFields[i].onValueChanged.AddListener((value) =>
            {
                savedTexts[index] = value;
                PlayerPrefs.SetString(PlayerPrefsKeyPrefix + index, value);
                PlayerPrefs.Save();
                Debug.Log($"InputField {index} text updated and saved: {value}");
            });
        }
    }

    public void SetTextForInput(int index, string text)
    {
        if (index >= 0 && index < inputFields.Length)
        {
            inputFields[index].text = text;
            savedTexts[index] = text;

            PlayerPrefs.SetString(PlayerPrefsKeyPrefix + index, text);
            PlayerPrefs.Save();
        }
    }

    public string GetSavedText(int index)
    {
        if (index >= 0 && index < savedTexts.Length)
            return savedTexts[index];
        else
            return string.Empty;
    }
}

using UnityEngine;
using UnityEngine.UI;
public class SignText : MonoBehaviour
{
    public Text textComponent; // UI Text bile�eni

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }
}

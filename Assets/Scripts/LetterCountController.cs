using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterCountController : MonoBehaviour
{
    public Button increaseButton;
    public Button decreaseButton;
    public TMP_Text numberText;
    public TMP_Text letterText;

    private int currentValue = 0;
    private int minValue = 0;
    private int maxValue = 9;

    public int CurrentValue => currentValue; // Expose currentValue

    private void Start()
    {
        UpdateText();
        increaseButton.onClick.AddListener(IncreaseValue);
        decreaseButton.onClick.AddListener(DecreaseValue);
    }

    private void IncreaseValue()
    {
        if (currentValue < maxValue)
        {
            currentValue++;
            UpdateText();
        }
    }

    private void DecreaseValue()
    {
        if (currentValue > minValue)
        {
            currentValue--;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        numberText.text = currentValue.ToString();
    }
}

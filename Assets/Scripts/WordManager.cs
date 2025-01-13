using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform cellsContainer;
    public float verticalSpacing = 7f;
    public TextMeshProUGUI wordText;
    public Button checkButton;
    public Button nextButton;
    public TextMeshProUGUI resultText;
    public string word; 

    private void Start()
    {
        word = GetRandomWord().ToUpper();
        GlobalVariables.word = word;
        GenerateCells(word);
        checkButton.onClick.AddListener(GatherLetterValues);
        nextButton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Stage2");
    }

    private string GetRandomWord()
    {
        string randomWord = GlobalVariables.words[UnityEngine.Random.Range(0, GlobalVariables.words.Length)];

        return randomWord;
    }

    public void GenerateCells(string word)
    {
        foreach (Transform child in cellsContainer)
        {
            Destroy(child.gameObject);
        }

        UpdateWordText(word);

        HashSet<char> uniqueLetters = new HashSet<char>(word.ToUpper());

        float yOffset = -125f;

        foreach (char letter in uniqueLetters)
        {
            GameObject newCell = Instantiate(cellPrefab, cellsContainer);
            LetterCountController controller = newCell.GetComponent<LetterCountController>();

            if (controller != null)
            {
                controller.letterText.text = letter.ToString();

                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(0f, -yOffset);
                    yOffset += rectTransform.rect.height + verticalSpacing;
                }
            }
        }

        RectTransform containerRect = cellsContainer.GetComponent<RectTransform>();
        if (containerRect != null)
        {
            float totalHeight = yOffset + verticalSpacing;
            containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, totalHeight);
        }
    }

    private void UpdateWordText(string word)
    {
        if (wordText != null)
        {
            wordText.text = word;
        }
    }

    public void GatherLetterValues()
    {
        GlobalVariables.letterValues.Clear();

        foreach (Transform child in cellsContainer)
        {
            LetterCountController controller = child.GetComponent<LetterCountController>();
            if (controller != null)
            {
                char letter = controller.letterText.text[0];
                int value = controller.CurrentValue;
                GlobalVariables.letterValues[letter] = value;
            }
        }

        Debug.Log("Letter Values:");
        foreach (var pair in GlobalVariables.letterValues)
        {
            Debug.Log($"{pair.Key}: {pair.Value}");
        }

        Debug.Log(CheckLetterValuesAgainstWord());

        bool isMatch = CheckLetterValuesAgainstWord();

        string word = wordText.text;

        if (isMatch)
        {
            resultText.text = "CORRECT!";
            resultText.color = Color.green;
            nextButton.interactable = true;
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = Color.red;
            nextButton.interactable = false;
        }
    }

    public bool CheckLetterValuesAgainstWord()
    {
        Dictionary<char, int> wordLetterCounts = new Dictionary<char, int>();

        foreach (char letter in this.word.ToUpper())
        {
            if (wordLetterCounts.ContainsKey(letter))
            {
                wordLetterCounts[letter]++;
            }
            else
            {
                wordLetterCounts[letter] = 1;
            }
        }

        if (GlobalVariables.letterValues.Count != wordLetterCounts.Count)
        {
            return false;
        }

        foreach (var pair in wordLetterCounts)
        {
            if (!GlobalVariables.letterValues.ContainsKey(pair.Key) || GlobalVariables.letterValues[pair.Key] != pair.Value)
            {
                return false;
            }
        }

        return true;
    }
}

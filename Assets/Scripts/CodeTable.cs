using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CodeTable : MonoBehaviour
{
    public GameObject entryPrefab;
    private Dictionary<char, string> codes;
    public TextMeshProUGUI word;
    public TMP_InputField inputField;
    public TextMeshProUGUI result;
    private string encodedWord;
    public GameObject endButton;


    void Start()
    {
        word.text = GlobalVariables.word;

        codes = GlobalVariables.letterCodes;

        int i = 0;
        foreach (var entry in codes)
        {
            GameObject entryInstance = Instantiate(entryPrefab, this.transform);
            entryInstance.transform.position += new Vector3(0, (float)-i*40, 0);

            // Find and assign the column values in the instantiated row.
            TextMeshProUGUI[] columns = entryInstance.GetComponentsInChildren<TextMeshProUGUI>();
            if (columns.Length >= 2)
            {
                columns[0].text = entry.Key.ToString(); // Set the first column to the char.
                columns[1].text = entry.Value.ToString(); // Set the second column to the int.
            }
            i++;
        }
    }

    public void CheckSolution()
    {
        encodedWord = "";
        foreach (char letter in word.text)
        {
            encodedWord += codes[letter];
        }

        if (inputField.text == encodedWord)
        {
            result.color = Color.green;
            result.text = "CORRECT!\nScore: " + GlobalVariables.score.ToString();
            endButton.SetActive(true);
        }
        else
        {
            result.color = Color.red;
            result.text = "WRONG!";
        }
    }

    public void ChangeScene()
    {
        GlobalVariables.score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}

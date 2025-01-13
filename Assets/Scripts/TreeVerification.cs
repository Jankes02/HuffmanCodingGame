using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class TreeVerification : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public CodeTree tree;
    private Dictionary<char, int> counts;
    private string word = "";
    private int optimalLength;
    private string optimalHuffman;
    private int toleratedLength;
    private Dictionary<char, string> codes;
    private int solutionLength;
    private int score;
    public Button nextButton;

    void Start()
    {
        counts = GlobalVariables.letterValues;

        foreach (var entry in counts)
            for (int i = 0; i < entry.Value; i++)
                word += entry.Key;
    }

    public void CheckSolution()
    {
        optimalHuffman = GetHuffmanFromString(word);
        optimalLength = optimalHuffman.Length;
        toleratedLength = 2 * optimalLength;
        codes = new Dictionary<char, string>();
        try
        {
            GetCodesFromTree(tree.root, "", codes);
        }
        catch (Exception e)
        {
            resultText.text = "TREE STRUCTURE WRONG";
            resultText.color = Color.red;
            nextButton.interactable = false;
            return;
        }

        if (codes.Count < counts.Count)
        {
            resultText.text = "CHARACTERS MISSING";
            resultText.color = Color.red;
            nextButton.interactable = false;
            return;
        }

        SetSolutionLength();

        if (solutionLength <= toleratedLength)
        {
            resultText.text = "CORRECT!";
            resultText.color = Color.green;
            score = toleratedLength - solutionLength;

            GlobalVariables.letterCodes = codes;
            nextButton.interactable = true;
        }
        else
        {
            resultText.text = "ENCODED STRING TOO LONG";
            resultText.color = Color.red;
            nextButton.interactable = false;
        }
    }

    public void ChangeScene()
    {
        GlobalVariables.score += GetUserScore();
        SceneManager.LoadScene("Stage3");
    }

    public char GetTextFromNode(NodeObj node)
    {
        var textMesh = node.Obj.transform.Find("Canvas/InputField (Legacy)/Text (Legacy)").GetComponent<Text>();
        if (textMesh != null)
        {
            if (textMesh.text == "")
                return '\0';

            return char.Parse(textMesh.text.ToUpper());
        }
        return '\0';
    }

    public void SetSolutionLength()
    {
        solutionLength = 0;
        foreach (var entry in counts)
            solutionLength += entry.Value * codes[entry.Key].Length;
    }

    public void GetCodesFromTree(NodeObj node, string currentCode, Dictionary<char, string> codes)
    {
        if (node == null)
            return;

        if (GetTextFromNode(node) != '\0')
        {
            if (!(ValidateNode(node.Left) && ValidateNode(node.Right)))
                throw new Exception("Leaf node has non-null children");

            if (codes.ContainsKey(GetTextFromNode(node)))
                throw new Exception("Key already exists");

            codes[GetTextFromNode(node)] = currentCode;
        }

        GetCodesFromTree(node.Left, currentCode + "0", codes);
        GetCodesFromTree(node.Right, currentCode + "1", codes);
    }

    public bool ValidateNode(NodeObj node)
    {
        if (node == null) 
            return true;

        if (GetTextFromNode(node) != '\0')
            return false;

        if (node.Left != null && node.Right != null)
            return ValidateNode(node.Left) && ValidateNode(node.Right);

        return true;
    }

    private class Node
    {
        public char? Character;
        public int Frequency;
        public Node Left, Right;
        public Node(char? c = null, int freq = 0) { Character = c; Frequency = freq; }
    }

    public string GetHuffmanFromString(string word)
    {
        var nodes = word.GroupBy(c => c).Select(g => new Node(g.Key, g.Count())).ToList();

        while (nodes.Count > 1)
        {
            nodes.Sort((x, y) => x.Frequency.CompareTo(y.Frequency));
            var left = nodes[0]; var right = nodes[1];
            nodes.RemoveRange(0, 2);
            nodes.Add(new Node(freq: left.Frequency + right.Frequency) { Left = left, Right = right });
        }

        var root = nodes[0];
        var codes = new Dictionary<char, string>();
        void GenerateCodes(Node node, string code)
        {
            if (node == null) return;
            if (node.Character.HasValue) codes[node.Character.Value] = code;
            GenerateCodes(node.Left, code + "0");
            GenerateCodes(node.Right, code + "1");
        }
        GenerateCodes(root, "");

        return string.Concat(word.Select(c => codes[c]));
    }

    public int GetUserScore()
    {
        int difference = Math.Abs(optimalLength - solutionLength);

        int userScore = Math.Max(0, 100 - (5 * difference));

        return userScore;
    }
}

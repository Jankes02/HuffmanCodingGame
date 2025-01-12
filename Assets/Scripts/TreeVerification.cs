using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private int mistakes = 0;
    private int score;

    void Start()
    {
        // counts = Variables.letterCounts;
        counts = new Dictionary<char, int>()
        {
            { 'a', 1 },
            { 'b', 2 },
            { 'c', 3 },
            { 'd', 4 }
        };

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
            Debug.Log(e.Message);
            mistakes++;
            resultText.text = "TREE STRUCTURE WRONG";
            resultText.color = Color.red;
            return;
        }

        if (codes.Count < counts.Count)
        {
            mistakes++;
            resultText.text = "CHARACTERS MISSING";
            resultText.color = Color.red;
            return;
        }

        SetSolutionLength();

        if (solutionLength <= toleratedLength)
        {
            resultText.text = "CORRECT!";
            resultText.color = Color.green;
            score = toleratedLength - solutionLength;

            // Variables.score += score;
            // yield return new WaitForSeconds(3);
            // SceneManager.LoadScene(Stage3);
            // obliczyc punkty, zapisac kody, przeniesc do etapu 3
        }
        else
        {
            mistakes++;
            resultText.text = "ENCODED STRING TOO LONG";
            resultText.color = Color.red;
        }
    }

    public char GetTextFromNode(NodeObj node)
    {
        var textMesh = node.Obj.transform.Find("Canvas/InputField (Legacy)/Text (Legacy)").GetComponent<Text>();
        if (textMesh != null)
        {
            if (textMesh.text == "")
                return '\0';

            return char.Parse(textMesh.text);
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
}

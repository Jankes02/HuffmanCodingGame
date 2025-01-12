using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class GlobalVariables : MonoBehaviour
{
    public static Dictionary<string, string> wordsWithOptimalCoding = new Dictionary<string, string>
    {
        { "Gargamel", "0011100001111010101100" },
        { "Halabarda", "110011110111001011000" },
        { "Jazgarz", "0011100111101010" },
        { "Kajakarz", "11001111011100101100" },
        { "Papiernia", "1101011110111100010000110" },
        { "Barabara", "10101101000110" },
        { "Tatarka", "101010001111100" },
        { "Narwana", "101010011101100" },
        { "Tarapaty", "11001111011100101100" },
        { "Galama", "10011101100" },
        { "Papaja", "10011101100" },
        { "Kokos", "1001110110" },
        { "Czekolada", "110111111100010000111001010" },
        { "Ramka", "1001111100" },
        { "Babcia", "10101001111100" }
    };

    public static Dictionary<char, int> letterValues = new Dictionary<char, int>();
}

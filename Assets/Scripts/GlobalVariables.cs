using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class GlobalVariables : MonoBehaviour
{
    public static string[] words = new string[]
        {
            "szarlatan", "papuga", "kajak", "kocur", "sos", "kokos", "zupa", "babka", "mamrot", "cyrk",
            "rabarbar", "dawid", "rzeszot", "abecadlo", "szklo", "szafa", "darlok", "lalka", "babcia", "alibaba",
            "karakol", "babarak", "tarapaty", "papieros", "barabar", "tartak", "kukurydza", "nadarz", "rondel", "torba",
            "latajacy", "kajakarz", "malpa", "kaka", "tarzan", "kukuryku", "kaszak", "szklanka", "parapet", "kookabura",
            "baba", "zazdrosc", "szarlotka", "rownosc", "tartak", "romantyzm", "szczur", "papiez", "babunio", "turysta",
            "sprzataczka", "pomidor", "komputer", "biblioteka", "malowanie", "teatr", "wiosna", "rower", "zima", "lato",
            "wakacje", "plama", "mama", "papaja", "kawka", "wioslowac", "rozmowa", "morze", "przemoc", "gazeta",
            "obiad", "dama", "paczek", "maslo", "piano", "samochod", "krab", "skorka", "mis", "sprinter",
            "kocur", "raport", "wielblad", "rabata", "szkarlat", "krolik", "drzwi", "blekit", "wozek", "tata",
            "olbrzym", "duzo", "zaklad", "naroznik", "mapa", "kosz", "pilka", "stol", "szachy", "klocki",
            "kompakt", "hamak", "pomarancza", "dziewczyna", "zatrzymac", "szopa", "glos", "ser", "hustler", "okulary"
        };

    public static Dictionary<char, int> letterValues = new();
    public static int score = 0;
    public static Dictionary<char, string> letterCodes = new();
    public static string word = "";
}

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
            "rabarbar", "dawid", "rzeszot", "abecad³o", "szk³o", "szafa", "dar³ok", "lalka", "babcia", "alibaba",
            "karakol", "babarak", "tarapaty", "papieros", "barabar", "tartak", "kukurydza", "nadarz", "rondel", "torba",
            "lataj¹cy", "kajakarz", "ma³pa", "kaka", "tarzan", "kukuryku", "kaszak", "szklanka", "parapet", "kookabura",
            "baba", "zazdroœæ", "szarlotka", "równoœæ", "tartak", "romantyzm", "szczur", "papie¿", "babunio", "turysta",
            "sprz¹taczka", "pomidor", "komputer", "biblioteka", "malowanie", "teatr", "wiosna", "rower", "zima", "lato",
            "wakacje", "plama", "mama", "papaja", "kawka", "wios³owaæ", "rozmowa", "morze", "przemoc", "gazeta",
            "obiad", "dama", "p¹czek", "mas³o", "piano", "samochód", "krab", "skórka", "miœ", "sprinter",
            "kocur", "raport", "wielb³¹d", "rabata", "szkar³at", "królik", "drzwi", "b³êkit", "wózek", "tata",
            "olbrzym", "du¿o", "zak³ad", "naro¿nik", "mapa", "kosz", "pi³ka", "stó³", "szachy", "klocki",
            "kompakt", "hamak", "pomarañcza", "dziewczyna", "zatrzymaæ", "szopa", "g³os", "ser", "hustler", "okulary"
        };

    public static Dictionary<char, int> letterValues = new Dictionary<char, int>();
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CountTable : MonoBehaviour
{
    public GameObject entryPrefab;
    private Dictionary<char, int> counts;
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

        var sortedCounts = counts.OrderByDescending(entry => entry.Value);

        int i = 0;
        foreach (var entry in sortedCounts)
        {
            GameObject entryInstance = Instantiate(entryPrefab, this.transform);
            entryInstance.transform.position += new Vector3(0, (float)-i/2, 0);

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
}

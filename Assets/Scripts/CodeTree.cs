using UnityEngine;

public class CodeTree : MonoBehaviour
{
    public int treeDepth; // Number of levels (N)
    public float horizontalSpacing; // Space between nodes horizontally
    public float verticalSpacing; // Space between levels vertically
#nullable enable
    public NodeObj? root;
#nullable disable

    public GameObject nodePrefab; // Prefab for visualization (e.g., a sphere with text)

    void Start()
    {
        if (nodePrefab == null)
        {
            return;
        }

        root = CreateTree(null, 0, this.transform.position[0], this.transform.position[1], treeDepth);
    }

#nullable enable
    public NodeObj? CreateTree(Transform parent, int currentLevel, float xPos, float yPos, int maxDepth)
    {
        if (currentLevel >= maxDepth)
            return null;

        GameObject node = Instantiate(nodePrefab, new Vector3(xPos, yPos, -1), Quaternion.identity, this.transform);
        node.name = $"Node_Level{currentLevel}_Pos({xPos},{yPos})";
        NodeObj nodeObj = new();
        nodeObj.Obj = node;

        var textMesh = node.GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = $"L{currentLevel}";
        }

        // If there's a parent, draw a line (connection)
        if (parent != null)
        {
            DrawEdge(parent.position + new Vector3(0, 0, 0.5f), node.transform.position + new Vector3(0, 0, 0.5f));
        }

        // Recursive creation of left and right children
        float offset = Mathf.Pow(2, (maxDepth - currentLevel - 1)) * horizontalSpacing;

        nodeObj.Left = CreateTree(node.transform, currentLevel + 1, xPos - offset, yPos - verticalSpacing, maxDepth);
        nodeObj.Right = CreateTree(node.transform, currentLevel + 1, xPos + offset, yPos - verticalSpacing, maxDepth);

        return nodeObj;
    }
#nullable disable

    public void DrawEdge(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject("Edge");
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}

public class NodeObj
{
    public char? Value;
    public NodeObj? Left, Right;
    public GameObject? Obj;
}

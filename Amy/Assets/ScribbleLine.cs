using System.Collections.Generic;
using UnityEngine;

public class ScribbleLine : MonoBehaviour
{
    public LineRenderer lineRendererPrefab; // Reference to a LineRenderer prefab
    public float lineWidth = 0.1f;

    private List<Vector3> currentLine = new List<Vector3>();
    private List<LineRenderer> allLines = new List<LineRenderer>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartNewScribble();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Set the distance to the camera

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            currentLine.Add(worldPos);

            UpdateLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndScribble();
        }
    }

    void StartNewScribble()
    {
        currentLine = new List<Vector3>();
    }

    void EndScribble()
    {
        CreateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        if (currentLine.Count > 1)
        {
            LineRenderer lineRenderer = GetLineRenderer();
            lineRenderer.positionCount = currentLine.Count;
            lineRenderer.SetPositions(currentLine.ToArray());
        }
    }

    void CreateLineRenderer()
    {
        LineRenderer newLineRenderer = Instantiate(lineRendererPrefab, transform.position, Quaternion.identity);
        newLineRenderer.startWidth = lineWidth;
        newLineRenderer.endWidth = lineWidth;
        newLineRenderer.positionCount = currentLine.Count;
        newLineRenderer.SetPositions(currentLine.ToArray());

        allLines.Add(newLineRenderer);
    }

    LineRenderer GetLineRenderer()
    {
        if (allLines.Count == 0)
        {
            CreateLineRenderer();
        }

        return allLines[allLines.Count - 1];
    }
}

using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    public Camera drawingCamera;
    public GameObject brush;

    private LineRenderer currentLineRenderer;
    private Vector2 lastPos;

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = drawingCamera.ScreenToWorldPoint(Input.mousePosition);
            if (lastPos != mousePos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
                CheckDeviation(mousePos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastPos = Vector2.zero;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = drawingCamera.ScreenToWorldPoint(Input.mousePosition);
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void CheckDeviation(Vector2 currentPos)
    {
        // Implement your logic here to check if the currentPos is off the word
        // This is a complex task and would depend on how you define the "path" of the word
    }
}

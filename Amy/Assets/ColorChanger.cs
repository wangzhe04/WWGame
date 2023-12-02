using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material topMaterial; // Reference to the "Top" material
    public Material botMaterial; // Reference to the "Bot" material
    public Material lineMaterial { get; private set; } // Material for the line (initialized to "Bot" material)

    private Vector2 lastMousePosition;
    private bool isDragging = false;
    private Vector2 accumulatedDrag = Vector2.zero;
    public float sensitivityThreshold = 0.1f; // Adjust as needed

    public void SetMaterialBasedOnDirection(Vector3 start, Vector3 end)
    {
        Vector3 distance = end - start;

        if (isDragging || accumulatedDrag.sqrMagnitude >= sensitivityThreshold)
        {
            if (distance.y > 0)
            {
                Debug.Log("Direction: Up");
                lineMaterial = topMaterial;
                // Reset accumulated drag to allow a change in direction
                accumulatedDrag = Vector2.zero;
            }
            else if (distance.y < 0)
            {
                Debug.Log("Direction: Down");
                lineMaterial = botMaterial;
                // Reset accumulated drag to allow a change in direction
                accumulatedDrag = Vector2.zero;
            }
        }
    }

    void Update()
    {
        Vector2 currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            // Mouse button was pressed, start tracking movement
            lastMousePosition = currentMousePosition;
            isDragging = true;
            accumulatedDrag = Vector2.zero;
        }

        if (isDragging)
        {
            // Continue tracking mouse position during dragging
            Vector2 delta = currentMousePosition - lastMousePosition;
            accumulatedDrag += delta;
            lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Mouse button was released, stop tracking movement
            isDragging = false;
            accumulatedDrag = Vector2.zero;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragDetector : MonoBehaviour
{
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float yChange = currentMousePosition.y - lastMousePosition.y;

            if (yChange > 0)
            {
                Debug.Log("Drag Up");
                // Perform actions for dragging up
            }
            else if (yChange < 0)
            {
                Debug.Log("Drag Down");
                // Perform actions for dragging down
            }

            lastMousePosition = currentMousePosition;
        }
    }
}


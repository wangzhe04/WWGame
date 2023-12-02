using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator2 : MonoBehaviour
{
    public GameObject linePrefab;
    //public ColorChanger colorChanger; // Reference to the ColorChanger script
    private Vector3 dragStartPosition;
    private bool isDragging = false;
    private bool hasLoggedDirectionChange = false;

    Line activeLine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
            hasLoggedDirectionChange = false; // Reset the flag when the drag stops
        }

        if (isDragging)
        {
            Vector3 dragCurrentPosition = Input.mousePosition;
            Vector3 dragDelta = dragCurrentPosition - dragStartPosition;

            // Check if the drag is predominantly vertical
            if (Mathf.Abs(dragDelta.y) > Mathf.Abs(dragDelta.x))
            {
                if (!hasLoggedDirectionChange)
                {
                    if (dragDelta.y > 0)

                    {
                        GameObject newLine = Instantiate(linePrefab);
                        activeLine = newLine.GetComponent<Line>();
                        // Debug.Log("Dragged Up");
                    }
                    else
                    {
                        //Debug.Log("Dragged Down");
                        
                    }
                    hasLoggedDirectionChange = true; // Set the flag to prevent continuous logging
                }
            }
        }




        void StartDragging()
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }

        void StopDragging()
        {
            isDragging = false;
        }



        // if (Input.GetMouseButtonDown(0)&&)
        //  {
        //   GameObject newLine = Instantiate(linePrefab);
        //   activeLine = newLine.GetComponent<Line>();
        //}

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
            //colorChanger.SetMaterialBasedOnDirection(activeLine.transform.position, mousePos);
            //activeLine.lineRenderer.material = colorChanger.lineMaterial;
        }
    }
}











using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    private Vector3 m_Position;
    public float zoomSpeed;
    public float camSpeed;
    public float rotSpeed;
    public Transform pivot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 9)
        {
            if (transform.position.y > 5)
            {
               ZoomCamera();
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                ZoomCamera();
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            ZoomCamera();
        }

        Vector3 movementMouse = m_Position - Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Vector3 mouvement = ((pivot.right * movementMouse.x)+(pivot.forward * movementMouse.y)) * Time.deltaTime * camSpeed; 
            pivot.position += mouvement;
        }

        if(Input.GetMouseButton(2))
        {
            pivot.Rotate(0, -movementMouse.x * Time.deltaTime * rotSpeed, 0);
        }

    m_Position = Input.mousePosition;

    }

    void ZoomCamera()
    {
        transform.position += transform.forward * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
        transform.Rotate(-2.36f * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, 0, 0);
    }
}

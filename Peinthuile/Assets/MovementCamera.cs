using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    private Vector3 m_Position;
    public float zoomSpeed;
    public float camSpeed;
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
                transform.position += transform.forward * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                transform.position += transform.forward * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            transform.position += transform.forward * Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
        }


        if(Input.GetMouseButton(1))
        {
            Vector3 movementMouse = m_Position - Input.mousePosition;
            transform.position += new Vector3(movementMouse.x,0,movementMouse.y)*Time.deltaTime;
        }








    m_Position = Input.mousePosition;
    }
}

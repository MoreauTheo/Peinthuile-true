using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementCamera : MonoBehaviour
{
    private Vector3 m_Position;
    public float zoomSpeed;
    public float camSpeed;
    public float rotSpeed;
    public Transform pivot;
    public Vector3 spawn;
    public Color sombre;
    public float alphaSpeed;
    public TextMeshProUGUI spacetext;
    void Start()
    {
        pivot.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            pivot.position = spawn;
        }
        if (Vector3.Distance(spawn, pivot.position) > 8)
        {
            if (sombre.a < 1)
            {
                sombre.a += alphaSpeed * Time.deltaTime;
                if (sombre.a > 1)
                    sombre.a = 1;

            }
        }
        else
        {
            if (sombre.a > 0)
            {
                sombre.a -= alphaSpeed * Time.deltaTime;

            }
        }
        spacetext.faceColor = sombre;
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

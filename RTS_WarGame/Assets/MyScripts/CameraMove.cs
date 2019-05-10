using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private float panSpeed;
    private float rotSpeed;
    private float panBorderTicc;
    private float scrollSpeed;
    private float scroll;
    private float minY, MaxY;
    private float rotX, rotY;
    private float minRotX, maxRotX;
    private Vector3 cameraPos;
    private Vector2 panLimit;
    public Transform CamHolder;

	void Start ()
    {
        panSpeed = 20f;
        rotSpeed = 10f;
        panBorderTicc = 20f;
        scrollSpeed = 1000f;
        minY = -40f;
        MaxY = 60f;
        minRotX = 30f;
        maxRotX = 70f;
        panLimit = new Vector2(150, 150);
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraPos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraPos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraPos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraPos.x -= panSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.y >= (Screen.height - panBorderTicc)) && !Input.GetKey(KeyCode.Mouse1))//up
        {
            cameraPos.z += panSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.y <= panBorderTicc) && !Input.GetKey(KeyCode.Mouse1))//down
        {
            cameraPos.z -= panSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.x >= (Screen.width - panBorderTicc)) && !Input.GetKey(KeyCode.Mouse1))//right
        {
            cameraPos.x += panSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.x <= panBorderTicc) && !Input.GetKey(KeyCode.Mouse1))//left
        {
            cameraPos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(Input.mousePosition.x != rotX)
            {
                float tempY = (Input.mousePosition.x - rotX) * rotSpeed * Time.deltaTime;
                CamHolder.Rotate(0, tempY, 0);
            }
            if(Input.mousePosition.y != rotY)
            {
                float tempX = (rotY - Input.mousePosition.y) * rotSpeed * Time.deltaTime;
                float desiredRot = transform.eulerAngles.x + tempX;
                if(desiredRot >= minRotX && desiredRot <= maxRotX)
                {
                    transform.Rotate(tempX, 0, 0);
                }
            }
            
        }

        scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraPos.y -= scroll * scrollSpeed * Time.deltaTime;

        rotX = Input.mousePosition.x;
        rotY = Input.mousePosition.y;

        cameraPos.x = Mathf.Clamp(cameraPos.x, -panLimit.x, panLimit.x);
        cameraPos.y = Mathf.Clamp(cameraPos.y, minY, MaxY);
        cameraPos.z = Mathf.Clamp(cameraPos.z, -panLimit.y, panLimit.y);

        transform.position = cameraPos;
    }
}

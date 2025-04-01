using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public GameObject mainCamera;
    public float rotateSpeed;
    float rotationX;
    float rotationY;
    public float moveSpeed = 5f;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        CharacterRotate();
        Move();        
    }

    void CharacterRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(0, rotationY, 0);
        mainCamera.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

    }

    void Move()
    {
        float x_pos = Input.GetAxis("Horizontal") * moveSpeed;
        float y_pos = Input.GetAxis("Vertical") * moveSpeed;

        rigid.velocity = transform.forward * x_pos + transform.right * y_pos;
        mainCamera.transform.position = transform.position + new Vector3(0,0.5f,0);
    }
}

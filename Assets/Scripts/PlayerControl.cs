using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float currenthp = 10;
    public float maxhp = 10;
    public float currentAir = 100;
    public float maxAir = 100;
    public Slider hp_Slider;
    public Slider air_Slider;
    public bool isAttack = true;
    public GameObject knife;
    public bool isJump = false;
    Rigidbody rigid;
    float rotationX;
    float rotationY;
    public float rotateSpeed;
    public float speed;
    public float jumpForce;
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = true;    // 중력 적용
        rigid.isKinematic = false;  // 물리 적용 가능하게 설정
        knife.transform.SetParent(mainCamera.transform);
        knife.transform.localPosition = new Vector3(0.3f, -0.2f, 0.5f);
        knife.transform.rotation = Quaternion.identity;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            currenthp = maxhp;
            currentAir = maxAir;
        }
        else if(SceneManager.GetActiveScene().buildIndex >= 1)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isJump && Input.GetKeyDown(KeyCode.Space)) 
        {
            isJump = false;
            rigid.AddForce(transform.up *jumpForce, ForceMode.Impulse);
        }

        if(isAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = false;
            StartCoroutine(Attack());
        }
        Rotate();
        
    }

    IEnumerator Attack()
    {
        
        knife.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isAttack");
        yield return new WaitForSeconds(1);
        isAttack = true;
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(0,rotationY,0);
        mainCamera.transform.rotation = Quaternion.Euler(rotationX,rotationY,0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;

        }
    }
    private void Move()
    {
        float x_pos = Input.GetAxis("Horizontal");
        float z_pos = Input.GetAxis("Vertical");
        mainCamera.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 direction = transform.forward * z_pos + transform.right * x_pos;
        direction = direction.normalized;
        rigid.velocity = new Vector3(direction.x * speed,rigid.velocity.y,direction.z * speed);

        
    }
}

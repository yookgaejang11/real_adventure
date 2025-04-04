using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public bool isRotate = true;
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
    bool isAir= true;
    float rotationX;
    float rotationY;
    public float rotateSpeed;
    public float speed;
    public float jumpForce;
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        hp_Slider = GameObject.Find("hp_bar").GetComponent<Slider>();
        air_Slider = GameObject.Find("Air_bar").GetComponent<Slider>();
        mainCamera = GameObject.Find("Main Camera");
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = true;    // 중력 적용
        rigid.isKinematic = false;  // 물리 적용 가능하게 설정
        knife.transform.SetParent(mainCamera.transform);
        knife.transform.localPosition = new Vector3(0.3f, -0.2f, 0.5f);
        knife.transform.rotation = Quaternion.identity;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            switch(DataManager.Instance.airLv)
            {
                case 1:
                    {
                        maxAir = 150;
                    }
                    break;
                case 2:
                    {
                        maxAir = 200;
                    }
                    break;
                case 3:
                    {
                        maxAir = 250;
                    }
                    break;
            } 
            currenthp = maxhp;
            currentAir = maxAir;
        }
        else if(SceneManager.GetActiveScene().buildIndex >= 1)
        {
            switch (DataManager.Instance.airLv)
            {
                case 1:
                    {
                        maxAir = 150;
                    }
                    break;
                case 2:
                    {
                        maxAir = 200;
                    }
                    break;
                case 3:
                    {
                        maxAir = 250;
                    }
                    break;
            }
            currenthp = DataManager.Instance.playerHp;
            currentAir = DataManager.Instance.playerair;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotate)
        {
            if (isJump && Input.GetKeyDown(KeyCode.Space))
            {
                isJump = false;
                rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            if (isAttack && Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttack = false;
                StartCoroutine(Attack());
            }
            Rotate();
        }
        

        hp_Slider.value = (float) currenthp / maxhp;
        air_Slider.value = (float)currentAir / maxAir;

        if(isAir )
        {
            StartCoroutine(AirMin());
        }
    }

    IEnumerator AirMin()
    {
        isAir = false;
        currentAir -= GameManager.Instance.AirCost;
        yield return new WaitForSeconds(1);
        isAir = true;
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

        if (collision.gameObject.CompareTag("Gate_In"))
        {
            DataManager.Instance.playerHp = currenthp;
            DataManager.Instance.playerair = currentAir;
            GameManager.Instance.AirCost++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            

            
        }
        if (collision.gameObject.CompareTag("Gate_Out"))
        {
            DataManager.Instance.playerHp = currenthp;
            DataManager.Instance.playerair = currentAir;
            GameManager.Instance.AirCost--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
        }

        if(collision.gameObject.CompareTag("Treasure"))
        {

            collision.gameObject.SetActive(false);
            Inventory.Instance.SetItem(collision.gameObject);
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

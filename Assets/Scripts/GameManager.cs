using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int AirCost = 0;
    private static GameManager instance;
    public bool inGame;
    public List<GameObject> Items;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex >=1)
        {
            inGame = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            inGame = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}

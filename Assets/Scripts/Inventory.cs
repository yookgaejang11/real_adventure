using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    bool isSelected = false;
    GameObject SpawnObj;
    public bool isOpen = false;
    public GameObject inventory;
    private static Inventory instance;
    public List<Button> invens;
    public int maxSlot;
    public Text weightTxt;
    public float currentWeight = 0;
    public float maxWeight = 100;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < invens.Count; i++)
        {
            invens[i].interactable = false;
        }
        switch(DataManager.Instance.bagLv)
        {
            case 1:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        invens[i].interactable = true;
                        invens[i].transform.GetChild(0).gameObject.SetActive(false);
                        maxSlot = 4;
                        maxWeight = 100;
                    }
                }
                break;
            case 2:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        invens[i].interactable = true;
                        invens[i].transform.GetChild(0).gameObject.SetActive(false);
                        maxSlot = 6;
                        maxWeight = 150;
                    }
                }
                break;
            case 3:
                {
                    for (int i = 0; i < 8; i++)
                    {
                        invens[i].interactable = true;
                        invens[i].transform.GetChild(0).gameObject.SetActive(false);
                        maxSlot = 8;
                        maxWeight = 200;
                    }
                }
                break ;
        }
    }

    public void SetItem(GameObject item)
    {
        for(int i = 0; i < maxSlot; i++)
        {
            if( invens[i].transform.childCount <2 && !isSelected)
            {
                SpawnObj = Instantiate(item.GetComponent<Item>().itemObj);
                RectTransform SpawnObjRt = SpawnObj.GetComponent<RectTransform>();
                SpawnObjRt.SetParent(invens[i].GetComponent<RectTransform>());
                Debug.Log("아이템 추가");
                SpawnObjRt.anchoredPosition = Vector3.zero;
                SpawnObjRt.anchorMax = Vector3.one;
                SpawnObjRt.anchorMin = Vector3.zero;
                
                isSelected = true;
            }
            Debug.Log(invens[i].transform.childCount);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            GameObject.Find("Player").GetComponent<PlayerControl>().isRotate = false;
            inventory.SetActive(true);
            isOpen = true;
            GameManager.Instance.inGame = false;
        }
        else if((Input.GetKeyDown(KeyCode.I) && isOpen))
        {
            GameObject.Find("Player").GetComponent<PlayerControl>().isRotate = true;
            inventory.SetActive(false);
            isOpen = false;
            GameManager.Instance.inGame = true;
        }
    }

    public static Inventory Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            else
            {
                return instance;
            }
        }
    }
}

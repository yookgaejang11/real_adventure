using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float playerHp;
    public float playerair;
    public int gold;
    public float playTime;
    public int bagLv = 1;
    public int airLv = 1;
    private static DataManager instance;
    public List<float> playRanking;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void RankSort(float Result)
    {
        playRanking.Add(Result);
        playRanking.Sort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static DataManager Instance
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


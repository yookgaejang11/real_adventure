using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text goldTxt;
    public List<Text> ranktxts = new List<Text>();
    public GameObject rankPage;
    public GameObject shopPage;
    public Text bagTxt;
    public Text airTxt;
    public Text errorTxt;
    int BagPrice = 50;
    int AirPrice = 50;
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(shopPage != null)
        {
            goldTxt.text = DataManager.Instance.gold.ToString();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void RankPage()
    {
        DataManager.Instance.playRanking.Sort();
        rankPage.SetActive(true);

        if(DataManager.Instance.playRanking.Count >= 5 )
        {
            for(int i = 0; i < 5; i++)
            {
                ranktxts[i].text = DataManager.Instance.playRanking[i].ToString("0.##");
            }
        }
        else
        {
            for(int i = 0;i < DataManager.Instance.playRanking.Count; i++)
            {
                ranktxts[i].text = DataManager.Instance.playRanking[i].ToString("0.##");
            }
        }

    }
    public void BagBuy()
    {

        StartCoroutine(BuyBag());
    }

    IEnumerator BuyBag()
    {
        if (DataManager.Instance.gold >= BagPrice)
        {
            if (DataManager.Instance.bagLv < 3)
            {
                DataManager.Instance.gold -= BagPrice;
                DataManager.Instance.bagLv += 1;
                BagPrice += 50;
                bagTxt.text = "����" + BagPrice + "��\n" + "���緹��" + DataManager.Instance.bagLv.ToString();
                if (DataManager.Instance.bagLv == 3)
                {
                    bagTxt.text = "����:MAX".ToString();
                }
            }
            else
            {
                errorTxt.text = "�� �̻� ������ �ø� �� �����ϴ�!".ToString();
                yield return new WaitForSeconds(1);
                errorTxt.text = "";
            }
        }
        else
        {
            errorTxt.text = "���� �����մϴ�";
            yield return new WaitForSeconds(1);
            errorTxt.text = "";
        }
    }

    public void AirBuy()
    {
        StartCoroutine(BuyAir());
    }
    IEnumerator BuyAir()
    {
            if (DataManager.Instance.gold >= AirPrice)
            {
                if (DataManager.Instance.airLv < 3)
                {
                    DataManager.Instance.gold -= AirPrice;
                    DataManager.Instance.airLv += 1;
                    AirPrice += 50;
                    airTxt.text = "����" + AirPrice + "��\n" + "���緹��" + DataManager.Instance.airLv.ToString();
                    if (DataManager.Instance.airLv == 3)
                    {
                        airTxt.text = "����:MAX".ToString();
                    }
                }
                else
                {
                    errorTxt.text = "�� �̻� ������ �ø� �� �����ϴ�!".ToString();
                    yield return new WaitForSeconds(1);
                    errorTxt.text = "";
                }
            }
            else
            {
                errorTxt.text = "���� �����մϴ�";
                yield return new WaitForSeconds(1);
                errorTxt.text = "";
            }
        }
    
    public void ShopPage()
    {
        shopPage.SetActive(true);
    }

    public void BackBtn(int num)
    {
        switch(num)
        {
            case 0:
                {
                    shopPage.SetActive (false);
                }
                break;
            case 1:
                {
                    rankPage.SetActive(false);
                }
                break;
        }
    }
}

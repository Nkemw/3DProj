using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EmploymentController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] TextMeshProUGUI nameAndStarValue;
    [SerializeField] TextMeshProUGUI speciesAndGender;
    [SerializeField] TextMeshProUGUI battleForce;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI shield;
    [SerializeField] TextMeshProUGUI skill;
    [SerializeField] TextMeshProUGUI recruit;
    [SerializeField] Image portrait;

    [SerializeField] GameObject employmentSuccessImg;

    [SerializeField] GameObject selectCategoryFrame;

    [SerializeField] GameObject NPCTextFrame;
    public void InitUI()
    {
        gameObject.SetActive(true);
        employmentSuccessImg.SetActive(false);

        if (GameManager.playerdata.isEmployed)
        {
            employmentSuccessImg.SetActive(true);
        }
        if (string.IsNullOrEmpty(GameManager.playerdata.employableMercenary.mercenaryID))
        {
            GameManager.playerdata.Gold += 100;
            Refresh();
        } else
        {
            AllObjectChange();
        }

        NPCTextFrame.SetActive(false);
    }

    public void ExitUI()
    {
        gameObject.SetActive(false);
        selectCategoryFrame.SetActive(true);

        NPCTextFrame.SetActive(false);
    }

    public void Refresh()
    {
        if (GameManager.playerdata.Gold >= 100)
        {
            //int randomIndex = Random.Range(0, GameManager.TableDB.MercenaryDB.Count);
            int randomIndex = Random.Range(0, 4);
            if (randomIndex == 3)
            {
                randomIndex = 4;
            }
            EmployableMercenaryDataChange(randomIndex);
        
            GameManager.playerdata.Gold -= 100;

            GoldTextUpdate();
            employmentSuccessImg.SetActive(false);
            GameManager.playerdata.isEmployed = false;
        }

        AllObjectChange();
        NPCTextFrame.SetActive(false);
    }

    public void EmployableMercenaryDataChange(int index)
    {
        GameManager.playerdata.employableMercenary.mercenaryID = GameManager.TableDB.MercenaryDB[index].mercenaryID;
        GameManager.playerdata.employableMercenary.mercenaryName = GameManager.TableDB.MercenaryDB[index].mercenaryName;
        GameManager.playerdata.employableMercenary.sex = GameManager.TableDB.MercenaryDB[index].sex;
        GameManager.playerdata.employableMercenary.species = GameManager.TableDB.MercenaryDB[index].species;
        GameManager.playerdata.employableMercenary.starValue = GameManager.TableDB.MercenaryDB[index].starValue;
        GameManager.playerdata.employableMercenary.baseHP = GameManager.TableDB.MercenaryDB[index].baseHP;
        GameManager.playerdata.employableMercenary.baseDefensive = GameManager.TableDB.MercenaryDB[index].baseDefensive;
        GameManager.playerdata.employableMercenary.baseCP = GameManager.TableDB.MercenaryDB[index].baseCP;
        GameManager.playerdata.employableMercenary.skill = GameManager.TableDB.MercenaryDB[index].skill;
        GameManager.playerdata.employableMercenary.recruitMoney = GameManager.TableDB.MercenaryDB[index].recruitMoney;
        GameManager.playerdata.employableMercenary.recruitRanValue = GameManager.TableDB.MercenaryDB[index].recruitRanValue;
    }

    public void AllObjectChange()
    {
        PortraitChange();
        AllTextChange();
    }

    public void PortraitChange()
    {
        switch (int.Parse(GameManager.playerdata.employableMercenary.mercenaryID))
        {
            case 1:
                portrait.sprite = Resources.Load<Sprite>("Dwarp1");
                break;
            case 2:
                portrait.sprite = Resources.Load<Sprite>("Elf1");
                break;
            case 3:
                portrait.sprite = Resources.Load<Sprite>("Human1");
                break;
            case 4:
                portrait.sprite = Resources.Load<Sprite>("Elf2");
                break;
            case 5:
                portrait.sprite = Resources.Load<Sprite>("Elf2");
                break;
            case 6:
                portrait.sprite = Resources.Load<Sprite>("Human1");
                break;
            case 7:
                portrait.sprite = Resources.Load<Sprite>("Dwarp1");
                break;
            case 8:
                portrait.sprite = Resources.Load<Sprite>("Elf1");
                break;
            case 9:
                portrait.sprite = Resources.Load<Sprite>("Human1");
                break;
        }
    }
    public void AllTextChange()
    {
        nameAndStarValue.text = GameManager.playerdata.employableMercenary.mercenaryName + "\t<color=yellow>★</color> " + GameManager.playerdata.employableMercenary.starValue;
        speciesAndGender.text = GameManager.playerdata.employableMercenary.species + "\t\t" + GameManager.playerdata.employableMercenary.sex;
        battleForce.text = GameManager.playerdata.employableMercenary.baseCP.ToString();
        health.text = GameManager.playerdata.employableMercenary.baseHP.ToString();
        shield.text = GameManager.playerdata.employableMercenary.baseDefensive.ToString();

        string skillTempText = "";

        if(GameManager.playerdata.employableMercenary.skill.Substring(0, GameManager.playerdata.employableMercenary.skill.Length - 1).Equals("mining"))
        {
            skillTempText = "- 채광 ";
        } else if (GameManager.playerdata.employableMercenary.skill.Substring(0, GameManager.playerdata.employableMercenary.skill.Length - 1).Equals("gathering"))
        {
            skillTempText = "- 채집 ";
        } else
        {
            skillTempText = "- 해체 ";
        }

        if (GameManager.playerdata.employableMercenary.skill[GameManager.playerdata.employableMercenary.skill.Length-1].ToString().Equals("1"))
        {
            skillTempText += "Lv. 1";
        }
        else if (GameManager.playerdata.employableMercenary.skill[GameManager.playerdata.employableMercenary.skill.Length - 1].ToString().Equals("2"))
        {
            skillTempText += "Lv. 2";
        }
        else
        {
            skillTempText += "Lv. 3";
        }

        skill.text = skillTempText;

        recruit.text = "-" + GameManager.playerdata.employableMercenary.recruitMoney.ToString();
    }

    public void Employ()
    {
        GameManager.playerdata.Gold -= GameManager.playerdata.employableMercenary.recruitMoney;
        GoldTextUpdate();

        employmentSuccessImg.SetActive(true);
        GameManager.playerdata.isEmployed = true;

        GameManager.AddMecenaryData(GameManager.playerdata.employableMercenary.mercenaryID);

        NPCTextFrame.SetActive(true);
        if (NPCTextFrame.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI npcText))
        {
            npcText.text = GameManager.TableDB.NPC_DB[1].conver2;
        }
        //GameManager.playerdata.employedMercenary[0] = new ~
    }



    public void GoldTextUpdate()
    {
        goldText.text = string.Format("{0:#,##0}", GameManager.playerdata.Gold);
    }
}

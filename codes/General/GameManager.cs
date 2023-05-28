using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

[System.Serializable]
public class PlayerData {

    public string playerName = "default";

    public int Gold;
    //public string playerPW;
    public AdditiveData[] additiveData = new AdditiveData[FileModule.MAXINVENTORYCOUNT];

    public MetalData[] metalData = new MetalData[FileModule.MAXINVENTORYCOUNT];

    public IngotData[] ingotData = new IngotData[FileModule.MAXINVENTORYCOUNT];

    public BladeData[] bladeData = new BladeData[FileModule.MAXINVENTORYCOUNT];

    public HandleData[] handleData = new HandleData[FileModule.MAXINVENTORYCOUNT];

    public WeaponData[] weaponData = new WeaponData[FileModule.MAXINVENTORYCOUNT];


    //첨가제
    //광석
    //철괴
    //칼날
    //손잡이
    //완성품

}

[System.Serializable]
public class AdditiveData       //첨가제
{
    public string UID;
    public int Amount;

    public AdditiveData(string UID, int Amount)
    {
        this.UID = UID;
        this.Amount = Amount;
    }
}

[System.Serializable]
public class MetalData          //광석
{
    public string UID;
    public int Amount;

    public MetalData(string UID, int Amount)
    {
        this.UID = UID;
        this.Amount = Amount;
    }
}

[System.Serializable]
public class IngotData          //주괴
{
    public string UID;
    public int Amount;
    public string Additive_UID;

    public IngotData(string UID, int Amount, string Additive_UID)
    {
        this.UID = UID;
        this.Amount = Amount;
        this.Additive_UID = Additive_UID;
    }
}
[System.Serializable]
public class BladeData          //칼날
{
    public string IngotUID;
    public int BladeType;
    public string Rating;
    public string Additive_UID;
    public int polishingBonus;

    public BladeData(string UID, int BladeType, string Rating, string Additive_UID)
    {
        this.IngotUID = UID;
        this.BladeType = BladeType;
        this.Rating = Rating;
        this.Additive_UID = Additive_UID;
    }
}
[System.Serializable]
public class HandleData         //손잡이
{
    public string Additive_UID_First;
    public string Additive_UID_Second;

    public HandleData(string Additive_UID_First, string Additive_UID_Second)
    {
        this.Additive_UID_First = Additive_UID_First;
        this.Additive_UID_Second = Additive_UID_Second;
    }
}

[System.Serializable]
public class WeaponData         //완성품
{
    public string IngotUID;
    public int BladeType;
    public string Rating;
    public string Additive_UID_First;
    public string Additive_UID_Second;
    public string Additive_UID_Third;

    public string weaponName;
    public int weaponValue;
    public int weaponCP; //무기 전투력
    public int weaponAtk;
    public int weaponDurability;


    public WeaponData(HandleData handleData, BladeData bladeData)
    {
        this.IngotUID = bladeData.IngotUID;
        this.BladeType = bladeData.BladeType;
        this.Rating = bladeData.Rating;
        this.Additive_UID_First = bladeData.Additive_UID;
        this.Additive_UID_Second = handleData.Additive_UID_First;
        this.Additive_UID_Third = handleData.Additive_UID_Second;
    }
}


public class GameManager : Singleton<GameManager>
{
    public static PlayerData playerdata = new PlayerData();

    public static DataBase TableDB;

    private void Awake()
    {
        TableDB = Resources.Load<DataBase>("DataBase");
    }
    private void Start()
    {

        Debug.Log(Application.persistentDataPath);

        
        
        FileModule.FileModuleInit();

        if (File.Exists(FileModule.filePath))
        {
            playerdata = FileModule.reader.ReadData<PlayerData>();
        }

        //Reseta();
        /*for (int i = 0; i < 10; i++)
        {
            AdditiveDataChange("p003");
        }*/

        AdditiveDataChange("p003");
        AdditiveDataChange("p004");
        FileModule.writer.WriteData(playerdata);
        
    }

    public TableEntity_Material FindMaterialRow(string UID)
    {
        TableEntity_Material temp = new TableEntity_Material();

        for(int i = 0; i < TableDB.MaterialDB.Count; i++)
        {
            if (TableDB.MaterialDB[i].itemID.Equals(UID))
            {
                temp = TableDB.MaterialDB[i];
                break;
            }
        }

        return temp;
    }

    public TableEntity_Effect FindEffectRow(string UID)
    {
        TableEntity_Effect temp = new TableEntity_Effect();

        for (int i = 0; i < TableDB.EffectDB.Count; i++)
        {
            if (TableDB.EffectDB[i].effectID.Equals(UID))
            {
                temp = TableDB.EffectDB[i];
                break;
            }
        }

        return temp;
    }

    public TableEntity_IngotDB FindIngotRow(string UID)
    {
        TableEntity_IngotDB temp = new TableEntity_IngotDB();

        for (int i = 0; i < TableDB.IngotDB.Count; i++)
        {
            if (TableDB.IngotDB[i].ingotID.Equals(UID))
            {
                temp = TableDB.IngotDB[i];
                break;
            }
        }

        return temp;
    }
    public void Reseta()
    {
        playerdata.Gold = 1000;

        //playerdata.additiveData[0] = new AdditiveData("1", 1);

        playerdata.metalData[0] = new MetalData("m003", 20);
        playerdata.metalData[1] = new MetalData("m004", 10);
        playerdata.metalData[2] = new MetalData("m005", 1);
        playerdata.additiveData[0] = new AdditiveData("p004", 10);
    }
    public static void MetalDataChange(string UID)      //광석 데이터 추가 메소드
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if(!string.IsNullOrEmpty(playerdata.metalData[i].UID) && playerdata.metalData[i].UID.Equals(UID))
            {
                playerdata.metalData[i].Amount++;
                break;
            }

            if (playerdata.metalData[i].Amount < 1)
            {
                playerdata.metalData[i] = new MetalData(UID, 1);
                break;
            }
        }

        FileModule.writer.WriteData(playerdata);
    }

    public static void AdditiveDataChange(string additive_UID)  //첨가제 데이터 추가 메소드
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(playerdata.additiveData[i].UID) && playerdata.additiveData[i].UID.Equals(additive_UID))
            {
                playerdata.additiveData[i].Amount++;
                break;
            }

            if (playerdata.additiveData[i].Amount < 1)
            {
                playerdata.additiveData[i] = new AdditiveData(additive_UID, 1);
                break;
            }
        }

        FileModule.writer.WriteData(playerdata);
    }

    public static void MetalUse(string UID)             //광석 데이터 삭제 메소드
    {
        for(int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(playerdata.metalData[i].UID) && playerdata.metalData[i].UID.Equals(UID))
            {
                playerdata.metalData[i].Amount--;
                if (playerdata.metalData[i].Amount < 1)
                {
                    MetalDataPush(i);
                }
                break;
            }
        }
    }
   
    public static void AdditiveUse(string additive_UID)     //첨가제 데이터 삭제 메소드
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(playerdata.additiveData[i].UID) && playerdata.additiveData[i].UID.Equals(additive_UID))
            {
                playerdata.additiveData[i].Amount--;
                if (playerdata.additiveData[i].Amount < 1)
                {
                    AdditiveDataPush(i);
                }
                break;
            }
        }
    }

    public static void MetalDataPush(int index)         
    {
        for(int i = index; i < FileModule.MAXINVENTORYCOUNT-1; i++)
        {
          
            
            playerdata.metalData[i].Amount = playerdata.metalData[i + 1].Amount;
            playerdata.metalData[i].UID = playerdata.metalData[i + 1].UID;
            

            if(i == FileModule.MAXINVENTORYCOUNT-2 && !string.IsNullOrEmpty(playerdata.metalData[i + 1].UID))
            {
                playerdata.metalData[i + 2] = new MetalData("", 0);
            }
        }
    }

    public static void AdditiveDataPush(int index)
    {
        for (int i = index; i < FileModule.MAXINVENTORYCOUNT - 1; i++)
        {

            
            playerdata.additiveData[i].Amount = playerdata.additiveData[i + 1].Amount;
            playerdata.additiveData[i].UID = playerdata.additiveData[i + 1].UID;
            

            if (i == FileModule.MAXINVENTORYCOUNT - 2 && !string.IsNullOrEmpty(playerdata.additiveData[i + 1].UID))
            {
                playerdata.additiveData[i + 2] = new AdditiveData("", 0);
            }
        }
    }

    public static void PopMetalByMakeIngot(string UID)
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (TableDB.IngotDB[i].ingotID.Equals(UID))
            {
                MetalUse(TableDB.IngotDB[i].baseMetal);
                MetalUse(TableDB.IngotDB[i].subMetal);
                break;
            }
        }
    }


    public static void IngotDataChange(string UID, string additive_UID)
    {
        for(int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(playerdata.ingotData[i].UID) && playerdata.ingotData[i].UID.Equals(UID)){
                if (!string.IsNullOrEmpty(playerdata.ingotData[i].Additive_UID))    //저장된 주괴 데이터에 첨가제가 들어가 있는 경우
                {
                    if (playerdata.ingotData[i].Additive_UID.Equals(additive_UID))
                    {
                        playerdata.ingotData[i].Amount++;
                        break;
                    }
                }
                else    //저장된 주괴 데이터에 첨가제가 들어가 있지 않은 경우
                {
                    if (string.IsNullOrEmpty(additive_UID)) //새로 만들어진 주괴에 첨가제가 없다면 UID만 비교하여 추가, 첨가제가 있으면 새로 만듬
                    {
                        playerdata.ingotData[i].Amount++;
                        break;
                    }
                }
                
            }

            if(playerdata.ingotData[i].Amount < 1) {
                playerdata.ingotData[i] = new IngotData(UID, 1, additive_UID);
                break;
            }
        }

        PopMetalByMakeIngot(UID);
        AdditiveUse(additive_UID);

        FileModule.writer.WriteData(playerdata);
    }

    public static void IngotUse(string UID, string Additive_UID)
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(playerdata.ingotData[i].UID) && playerdata.ingotData[i].UID.Equals(UID) && playerdata.ingotData[i].Additive_UID.Equals(Additive_UID))
            {
                playerdata.ingotData[i].Amount--;
                if (playerdata.ingotData[i].Amount < 1)
                {
                    IngotDataPush(i);
                }
                break;
            }
        }
    }
    public static void IngotDataPush(int index)
    {
        for (int i = index; i < FileModule.MAXINVENTORYCOUNT - 1; i++)
        {
            playerdata.ingotData[i].Amount = playerdata.ingotData[i + 1].Amount;
            playerdata.ingotData[i].UID = playerdata.ingotData[i + 1].UID;
            playerdata.ingotData[i].Additive_UID = playerdata.ingotData[i + 1].Additive_UID;

            if (i == FileModule.MAXINVENTORYCOUNT - 2 && !string.IsNullOrEmpty(playerdata.ingotData[i + 1].UID))
            {
                playerdata.ingotData[i + 2] = new IngotData("", 0, "");
            }
        }
    }

    

    public static void BladeDataChange(string IngotUID, int BladeType, string Rating, string Additive_UID)
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(playerdata.bladeData[i].IngotUID))
            {
                playerdata.bladeData[i] = new BladeData(IngotUID, BladeType, Rating, Additive_UID);
                break;
            }
        }

        IngotUse(IngotUID, Additive_UID);

        FileModule.writer.WriteData(playerdata);
    }

    public static void AddBladeBonusScore(int index, int score)
    {
        GameManager.playerdata.bladeData[index].polishingBonus = score;

        FileModule.writer.WriteData(playerdata);
    }

    public static void HandleDataChange(string handleAdditive_UID, string gripAdditive_UID)
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(playerdata.handleData[i].Additive_UID_First))
            {
                playerdata.handleData[i] = new HandleData(handleAdditive_UID, gripAdditive_UID);
                break;
            }
        }

        AdditiveUse(handleAdditive_UID);
        AdditiveUse(gripAdditive_UID);

        FileModule.writer.WriteData(playerdata);
    }

}

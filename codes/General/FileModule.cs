using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class FileWriter
{
    protected string filePath;

    public FileWriter(string filePath)
    {
        this.filePath = filePath;
    }

    public abstract void WriteData(object data);

    public static FileWriter CreateWriter(string filePath)
    {
        //파일의 확장자가 .json인지 확인하여 확장자에 맞는 파일을 저장
        //.json 확장자가 아닌 파일들은 모두 TextfileWriter로 저장되므로
        //TextFileWriter의 의미가 모호함
        if (filePath.EndsWith(".json"))
        {
            return new JsonFileWriter(filePath);
        }
        else
        {
            return new TextFileWriter(filePath);
        }
    }
}

public class JsonFileWriter : FileWriter
{
    //파일 경로를 받아 부모 클래스인 FileWriter에 생성자 전달
    public JsonFileWriter(string filePath) : base(filePath) { }

    public override void WriteData(object data)
    {
        string json = JsonUtility.ToJson(data);     //Json형식으로 변환한 뒤 파일 저장
        Debug.Log(json);
        File.WriteAllText(filePath, json);
    }
}

public class TextFileWriter : FileWriter
{
    public TextFileWriter(string filePath) : base(filePath) { }

    public override void WriteData(object data)
    {
        string text = data.ToString();              //문자열로 변환한 뒤 파일 저장
        File.WriteAllText(filePath, text);
    }
}

public abstract class FileReader
{
    protected string filePath;

    public FileReader(string filePath)
    {
        this.filePath = filePath;
    }

    public abstract T ReadData<T>(); //객체의 타입으로 파일을 불러오고 이를 반환

    public static FileReader CreateReader(string filePath)
    {
        if (filePath.EndsWith(".json"))
        {
            return new JsonFileReader(filePath);
        }
        else
        {
            return new TextFileReader(filePath);
        }
    }
}

public class JsonFileReader : FileReader
{
    public JsonFileReader(string filePath) : base(filePath) { }

    public override T ReadData<T>()
    {
        T data = JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        return data;
    }
}

public class TextFileReader : FileReader
{
    public TextFileReader(string filePath) : base(filePath) { }

    public override T ReadData<T>()
    {
        T data = JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        return data;
    }
}


/*public class Person
{
    public string name;
    public int age;

    public InventoryData inventoryData = new InventoryData();
}*/

/*[System.Serializable]
public class InventoryData
{
    public string name;
    public List<string> itemData = new List<string>();
}*/

public class FileModule : MonoBehaviour
{
    //public static InventoryData inventoryData = new InventoryData();
    public static int MAXINVENTORYCOUNT = 36;

    public static string filePath;
    public static FileWriter writer; //= FileWriter.CreateWriter(filePath);
    public static FileReader reader; //= FileReader.CreateReader(filePath);

    public static void FileModuleInit()
    {
        filePath = Application.persistentDataPath + "/inventoryData.json";
        writer = FileWriter.CreateWriter(filePath);
        reader = FileReader.CreateReader(filePath);
    }
    private void Awake()
    {

        /*Person a = new Person();

        a.name = "Alice";
        a.age = 25;

        a.inventoryData.itemData.Add("aa");
        a.inventoryData.itemData.Add("bb");

        a.inventoryData.name = "asd";
        writer.WriteData(a);

        Person b = reader.ReadData<Person>();

        Debug.Log(b.name + " " + b.age);

        Debug.Log(filePath);
        for(int i = 0; i < b.inventoryData.itemData.Count; i++)
        {
            Debug.Log(b.inventoryData.itemData[i]);
        }*/
        /*string filePath = Application.persistentDataPath + "/data.json";

        //파일 경로 확인
        Debug.Log(filePath);

        FileWriter writer = FileWriter.CreateWriter(filePath);
        FileReader reader = FileReader.CreateReader(filePath);

        // 쓸 데이터 생성
        Person savePeople = new Person("Alice", 25);

        // 데이터 쓰기
        writer.WriteData(savePeople);

        // 불러올 데이터 생성
        Person loadPeople = reader.ReadData<Person>();

        Debug.Log("불러온 사람의 이름: " + loadPeople.name +
                    " 불러온 사람의 나이: " + loadPeople.age);*/

    }

    /*public static void pushItemIntoInventory(string itemPath)
    {
        if (inventoryData.itemData.Count < 36)
        {
            inventoryData.itemData.Add(itemPath);
        }
    }

    public static void saveInventoryData()
    {
        writer.WriteData(inventoryData);
    }

    public static void loadInventoryData()
    {
        inventoryData = reader.ReadData<InventoryData>();
    }*/
}


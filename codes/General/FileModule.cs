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
        //������ Ȯ���ڰ� .json���� Ȯ���Ͽ� Ȯ���ڿ� �´� ������ ����
        //.json Ȯ���ڰ� �ƴ� ���ϵ��� ��� TextfileWriter�� ����ǹǷ�
        //TextFileWriter�� �ǹ̰� ��ȣ��
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
    //���� ��θ� �޾� �θ� Ŭ������ FileWriter�� ������ ����
    public JsonFileWriter(string filePath) : base(filePath) { }

    public override void WriteData(object data)
    {
        string json = JsonUtility.ToJson(data);     //Json�������� ��ȯ�� �� ���� ����
        Debug.Log(json);
        File.WriteAllText(filePath, json);
    }
}

public class TextFileWriter : FileWriter
{
    public TextFileWriter(string filePath) : base(filePath) { }

    public override void WriteData(object data)
    {
        string text = data.ToString();              //���ڿ��� ��ȯ�� �� ���� ����
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

    public abstract T ReadData<T>(); //��ü�� Ÿ������ ������ �ҷ����� �̸� ��ȯ

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

        //���� ��� Ȯ��
        Debug.Log(filePath);

        FileWriter writer = FileWriter.CreateWriter(filePath);
        FileReader reader = FileReader.CreateReader(filePath);

        // �� ������ ����
        Person savePeople = new Person("Alice", 25);

        // ������ ����
        writer.WriteData(savePeople);

        // �ҷ��� ������ ����
        Person loadPeople = reader.ReadData<Person>();

        Debug.Log("�ҷ��� ����� �̸�: " + loadPeople.name +
                    " �ҷ��� ����� ����: " + loadPeople.age);*/

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


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

public class FileModule : MonoBehaviour
{
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
}

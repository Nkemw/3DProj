using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    private static int count = 0;
    public void Awake()
    {
        if (count < 1)
        {
            if (transform.parent != null && transform.root != null)
            {
                DontDestroyOnLoad(this.transform.root.gameObject);
                count++;
            }
            else
            {
                if (!(this.gameObject.tag == "UI") && !(this.gameObject.tag == "Manager"))
                {
                    DontDestroyOnLoad(this.gameObject);
                    count++;
                }
            }
        }


    }
}

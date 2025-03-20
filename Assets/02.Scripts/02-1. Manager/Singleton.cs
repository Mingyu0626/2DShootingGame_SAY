using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Singleton() { }
    private bool _dontDestroy = true;
    private static bool _isQuitting = false;
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_isQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindFirstObjectByType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    _instance = obj.GetComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        else
        {
            _instance = this as T;
        }

        if (_dontDestroy && transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            GameObject rootManagerGO = GameObject.FindGameObjectWithTag("Manager");
            if (rootManagerGO != null)
            {
                transform.SetParent(rootManagerGO.transform);
            }
            else if (_dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}

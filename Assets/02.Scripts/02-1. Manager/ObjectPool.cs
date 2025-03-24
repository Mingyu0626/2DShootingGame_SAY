using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolAble
{
    public void Init();
}

public class ObjectPool<EnumType, ObjectType> : Singleton<ObjectPool<EnumType, ObjectType>> where ObjectType : MonoBehaviour, IProduct
{
    [System.Serializable]
    public class ObjectInfo
    {
        public EnumType ObjectType; // 생성할 오브젝트의 이름
        public ObjectType Prefab; // 생성할 GO 
        public int Count; // 미리 생성할 오브젝트 개수
    }

    [SerializeField] private ObjectInfo[] _objectInfos;
    public ObjectInfo[] ObjectInfos { get => _objectInfos; set => _objectInfos = value; }

    // 오브젝트 리스트 풀링을 위한 Dictionary
    private Dictionary<EnumType, List<ObjectType>> _objectPoolDic = new Dictionary<EnumType, List<ObjectType>>();

    // ObjectType의 GO를 생성하는 팩토리
    [SerializeField] protected Factory<ObjectType> _factory;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        for (int i = 0; i < _objectInfos.Length; i++)
        {
            EnumType objectEnumType = _objectInfos[i].ObjectType;

            if (_objectPoolDic.ContainsKey(objectEnumType))
            {
                Debug.LogFormat("{0}은 이미 등록된 오브젝트입니다.", objectEnumType);
                return;
            }

            // ListPool에서 리스트 가져오기
            List<ObjectType> objectList = ListPool<ObjectType>.Get();
            _objectPoolDic.Add(objectEnumType, objectList);

            // 오브젝트 미리 생성 후 리스트에 추가
            for (int j = 0; j < _objectInfos[i].Count; j++)
            {
                ObjectType obj = _factory.GetProduct(_objectInfos[i].Prefab.gameObject, transform.position);
                obj.transform.SetParent(this.transform);
                obj.gameObject.SetActive(false);
                objectList.Add(obj);
            }
        }
    }

    public ObjectType GetObject(EnumType enumType, Vector3 position)
    {
        if (!_objectPoolDic.ContainsKey(enumType))
        {
            Debug.LogFormat("{0}은 오브젝트풀에 등록되지 않은 오브젝트입니다.", enumType);
            return null;
        }

        List<ObjectType> objectList = _objectPoolDic[enumType];

        // 리스트에서 비활성화된 오브젝트 찾기
        foreach (ObjectType obj in objectList)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                obj.transform.position = position;
                obj.Init();
                return obj;
            }
        }

        // 모든 오브젝트가 사용 중이라면, 객체 하나 생성 후 리스트에 추가.
        ObjectType newObj = _factory.GetProduct(_objectPoolDic[enumType][0].gameObject, transform.position);
        newObj.transform.SetParent(this.transform);
        newObj.gameObject.SetActive(true);
        newObj.transform.position = position;
        newObj.Init();
        objectList.Add(newObj);  
        return newObj;
    }

    public void ReturnObject(ObjectType obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void ReturnAllActiveObjectsToPool()
    {
        foreach (var kvp in _objectPoolDic)
        {
            List<ObjectType> objectList = kvp.Value;

            foreach (ObjectType obj in objectList)
            {
                obj.gameObject.SetActive(false);
            }

            // 리스트를 다시 풀로 반환
            objectList.Clear();
            ListPool<ObjectType>.Release(objectList);
        }

        _objectPoolDic.Clear();
    }
    public bool CheckTypeInPool(EnumType enumType)
    {
        return _objectPoolDic.ContainsKey(enumType);
    }
}

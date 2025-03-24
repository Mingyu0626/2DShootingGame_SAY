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
        public EnumType ObjectType; // ������ ������Ʈ�� �̸�
        public ObjectType Prefab; // ������ GO 
        public int Count; // �̸� ������ ������Ʈ ����
    }

    [SerializeField] private ObjectInfo[] _objectInfos;
    public ObjectInfo[] ObjectInfos { get => _objectInfos; set => _objectInfos = value; }

    // ������Ʈ ����Ʈ Ǯ���� ���� Dictionary
    private Dictionary<EnumType, List<ObjectType>> _objectPoolDic = new Dictionary<EnumType, List<ObjectType>>();

    // ObjectType�� GO�� �����ϴ� ���丮
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
                Debug.LogFormat("{0}�� �̹� ��ϵ� ������Ʈ�Դϴ�.", objectEnumType);
                return;
            }

            // ListPool���� ����Ʈ ��������
            List<ObjectType> objectList = ListPool<ObjectType>.Get();
            _objectPoolDic.Add(objectEnumType, objectList);

            // ������Ʈ �̸� ���� �� ����Ʈ�� �߰�
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
            Debug.LogFormat("{0}�� ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", enumType);
            return null;
        }

        List<ObjectType> objectList = _objectPoolDic[enumType];

        // ����Ʈ���� ��Ȱ��ȭ�� ������Ʈ ã��
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

        // ��� ������Ʈ�� ��� ���̶��, ��ü �ϳ� ���� �� ����Ʈ�� �߰�.
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

            // ����Ʈ�� �ٽ� Ǯ�� ��ȯ
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

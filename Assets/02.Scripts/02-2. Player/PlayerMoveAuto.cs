using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PriorityQueue<T>
{
    private List<Tuple<T, int>> _queue = new List<Tuple<T, int>>();

    public void Enqueue(T item, int priority)
    {
        _queue.Add(Tuple.Create(item, priority));
        _queue.Sort((x, y) => x.Item2.CompareTo(y.Item2)); // �켱���� �������� ����
    }

    public T Dequeue()
    {
        if (_queue.Count == 0) throw new InvalidOperationException("Queue is empty");
        var item = _queue[0].Item1;
        _queue.RemoveAt(0);
        return item;
    }

    public int Count => _queue.Count;
}




public class PlayerMoveAuto : MonoBehaviour
{
    [SerializeField]
    private GameObject _dangerZone; // �÷��̾ �������� �� �� ����

    // �÷��̾���� �Ÿ��� �������� ��������, ������������ ���ĵǴ� 2���� PQ�� ����
    private PriorityQueue<GameObject> _nearPQ = new PriorityQueue<GameObject>();
    private PriorityQueue<GameObject> _farPQ = new PriorityQueue<GameObject>();


    private void Awake()
    {
        
    }

    private void Update()
    {
        // PQ ������� �⺻������ ��Ʈ��

        // Near PQ Top ��ġ�� �ݴ�������� �̵�
        // Top�� null�̶�� Pop

        // Far PQ Top�� DangerZone ���������� ũ�ٸ� Pop
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������, �� PQ�� �־��ֱ�
    }



}

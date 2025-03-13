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
        _queue.Sort((x, y) => x.Item2.CompareTo(y.Item2)); // 우선순위 기준으로 정렬
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
    private GameObject _dangerZone; // 플레이어를 기준으로 한 원 영역

    // 플레이어와의 거리를 기준으로 내림차순, 오름차순으로 정렬되는 2개의 PQ를 관리
    private PriorityQueue<GameObject> _nearPQ = new PriorityQueue<GameObject>();
    private PriorityQueue<GameObject> _farPQ = new PriorityQueue<GameObject>();


    private void Awake()
    {
        
    }

    private void Update()
    {
        // PQ 비었으면 기본적으로 패트롤

        // Near PQ Top 위치의 반대방향으로 이동
        // Top이 null이라면 Pop

        // Far PQ Top이 DangerZone 반지름보다 크다면 Pop
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 들어오면, 양 PQ에 넣어주기
    }



}

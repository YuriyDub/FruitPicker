using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task<Fruit> Task { get; set; }

    [SerializeField] private UnityEvent _wonEvent;

    [SerializeField] private TextMeshProUGUI _fruitCounter;

    [SerializeField] private FruitPool _fruitPool;


    [SerializeField] private Animator _basketAnimator;

    private int pickedCount;

    public void PutFruit(Fruit fruit)
    {
        if (fruit?.Kind == Task.Goal.Kind && pickedCount < Task.Count)
        {
            _basketAnimator.SetTrigger("plusOne");

            pickedCount++;
        }

        if (pickedCount == Task.Count)
        {
            _wonEvent.Invoke();
        }

        return;
    }

    private void Awake()
    {
        var element = _fruitPool.FruitsData.GetRandElement();
        var count = Random.Range(1, 6);

        Task = new Task<Fruit>(element, count);

        _fruitPool.PoolStart(Task);
    }

    private void Update()
    {
        _fruitCounter.text = pickedCount + " / " + Task.Count + " " + Task.Goal.gameObject.name + "s";
    }

}

public class Task<T> where T : MonoBehaviour
{
    public T Goal { get; set; }
    public int Count { get; set; }

    public Task(T targetType, int count)
    {
        Goal = targetType;
        Count = count;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator _animator;

    private static readonly int IsCollected = Animator.StringToHash("isCollected");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Collector"))
        {
            _animator.SetBool(IsCollected, true);
            Debug.Log("Collected");
        }
    }

    [SerializeField] private float rangeToGround = 1.5f;
    private void Disappear()
    {
        Debug.Log("Disappeared");
        this.transform.position += RandomPosition(transform);
        _animator.SetBool(IsCollected, false);
        
    }

    [SerializeField]
    private float maxRange = 20f;
    [SerializeField]
    private float minRange = 10f;
    private Vector3 RandomPosition(Transform transform)
    {
        var range = UnityEngine.Random.Range(minRange, maxRange);
        
        var result = GetRandomDirection(transform.position, range);
        transform.position += result;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit))
            return result;
        if (hit.distance > rangeToGround)
        {
            result += Vector3.down * (hit.distance - rangeToGround);
        }
        else if (hit.distance < rangeToGround)
        {
            result += Vector3.up * (rangeToGround - hit.distance);
        }
        Debug.Log("hit1: " + hit.distance);
        return result;
    }
    [SerializeField]
    private float maxForward = 596f;
    [SerializeField]
    private float maxBack = 74.94f;
    [SerializeField]
    private float maxRight = 929.43f;
    [SerializeField]
    private float maxLeft = 115.8f;

    private Vector3 GetRandomDirection(Vector3 position, float range)
    {
        var rand = new System.Random();
        while (true)
        {
            var dir = rand.Next(0, 3);
            switch (dir)
            {
                case 0:
                {
                    var res = Vector3.forward * range;
                    if ((position += res).z > maxForward)
                    {
                        continue;
                    }
                    return res;
                }
                case 1:
                {
                    var res = Vector3.back * range;
                    if ((position += res).z < maxBack)
                    {
                        continue;
                    }
                    return res;
                }
                case 2:
                {
                    var res = Vector3.right * range;
                    if ((position += res).x > maxRight)
                    {
                        continue;
                    }
                    return res;
                }
                case 3:
                {
                    var res = Vector3.left * range;
                    if ((position += res).x < maxLeft)
                    {
                        continue;
                    }
                    return res;
                }
            }
        }
    }
}

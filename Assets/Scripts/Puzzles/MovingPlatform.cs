using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;
    [SerializeField] private float waitTime;
    private float waitingTime;
    private int i;
    
    private void Start()
    {
        transform.position = points[startingPoint].position;
        waitingTime = waitTime;
    }
    
    private void Update()
    {
        if (waitingTime >= 0.1f)
        {
            waitingTime -= Time.deltaTime;
            return;
        }
        if (Vector3.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            waitingTime = waitTime;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y)
            {
                other.transform.SetParent(transform);
            }
        }
    }
    

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}

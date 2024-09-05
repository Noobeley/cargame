using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestrian : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    [SerializeField]
    private float speed = 5f;

    private bool isMoving = true;

    private void Start()
    {
        StartCoroutine(MoveBetweenPoints());
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

            if (transform.position == endPoint.position)
            {
                isMoving = false;
            }
        }
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (isMoving)
        {
            yield return StartCoroutine(MoveToPoint(startPoint.position));
            yield return StartCoroutine(MoveToPoint(endPoint.position));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPoint)
    {
        while (transform.position != targetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

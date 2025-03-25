using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float stepTime = .5f;
    [SerializeField] private GameObject tailPrefab;
    private float stepTimer;

    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection = Vector2.right;

    private ScoreManager scoreManager;

    private List<Transform> tailSegments = new List<Transform>();
    private List<Vector3> positionsHistory = new List<Vector3>();

    public void Initialize(ScoreManager sm)
    {
        scoreManager = sm;
        positionsHistory.Add(transform.position);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        if (inputVector != Vector2.zero)
        {
            inputVector.x = Mathf.Round(inputVector.x);
            inputVector.y = Mathf.Round(inputVector.y);

            if (inputVector != -direction)
            {
                nextDirection = inputVector.normalized;
            }
        }
    }

    private void Update()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer >= stepTime)
        {
            stepTimer = 0f;
            StepMovement();
        }
    }

    private void StepMovement()
    {
        direction = nextDirection;

        positionsHistory.Insert(0, transform.position);

        transform.position += (Vector3)direction;

        for (int i = 0; i < tailSegments.Count; i++)
        {
            tailSegments[i].position = positionsHistory[i + 1];
        }

        if (positionsHistory.Count > tailSegments.Count + 1)
        {
            positionsHistory.RemoveAt(positionsHistory.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Tail"))
        {
            GameManager.Instance.GameOver();
        }

        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            scoreManager.UpdateScore(1);
            GameManager.Instance.GenerateFood();
            AddTailSegment();
        }
    }

    private void AddTailSegment()
    {
        Vector3 spawnPos;

        if (positionsHistory.Count > 0)
        {
            spawnPos = positionsHistory[positionsHistory.Count - 1];
        }
        else
        {
            spawnPos = transform.position;
        }

        GameObject newSegment = Instantiate(tailPrefab, spawnPos, Quaternion.identity);
        tailSegments.Add(newSegment.transform);
    }
}

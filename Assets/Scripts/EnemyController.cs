using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform visionRayPoint;
    [SerializeField] private float playerHeightOffset = 1f;

    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private GameObject playerObject;  // Reference to the Player object

    private bool isChasing = false;
    [SerializeField] private string moveSpeedName = "MoveSpeed";


    private void Update()
    {
        if (!gameLogic.canMove)
        {
            StopEnemy();
            return;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            PatrolArea();
        }
    }

    private void PatrolArea()
    {
        if (agent.remainingDistance < 0.1f)
        {
            StopEnemy();
        }

        RotateEnemy();
    }

    private void RotateEnemy()
    {
        float rotateAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotateAmount);
    }

    private void StopEnemy()
    {
        animator.SetFloat(moveSpeedName, 0);
        agent.isStopped = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
        animator.SetFloat(moveSpeedName, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            DetectPlayer(playerObject.transform);
        }
    }

    private void DetectPlayer(Transform playerTransform)
    {
        Vector3 playerPosition = playerTransform.position + Vector3.up * playerHeightOffset;
        Vector3 rayDir = playerPosition - visionRayPoint.position;
        Ray ray = new Ray(visionRayPoint.position, rayDir);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject == playerObject)
        {
            Debug.Log("player detected by raycast");
            isChasing = true;
            ChasePlayer();
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private string moveSpeedName = "MoveSpeed";

    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private GameObject goalObject;  // Reference to the Goal object


    private void Update()
    {
        if (!gameLogic.canMove)
        {
            Debug.Log("Cannot move, gameLogic.canMove is false");
            animator.SetFloat(moveSpeedName, 0);
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        Debug.Log("HandleMovement");
        Debug.Log("handle");
        float rotateAmount = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotateAmount);
        Debug.Log("hori");
        float moveAmount = Input.GetAxis("Vertical");
        animator.SetFloat(moveSpeedName, moveAmount);
        Debug.Log("vert");
        Vector3 moveVector = transform.forward * moveAmount * moveSpeed * Time.deltaTime;
        controller.Move(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == goalObject)
        {
            Debug.Log("win game");
            gameLogic.WinGame();
            AudioManager.instance.PlaySFX(AudioManager.instance.winSound);
        }
    }
}
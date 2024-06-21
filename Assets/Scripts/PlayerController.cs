using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // CharacterController component reference
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private CameraContoller CameraController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _groundCheckRotation = 0.2f;
    [SerializeField] private Vector3 _groundCheckOffset;
    [SerializeField] private LayerMask _groundLayerMask;

    // Movement variables
    public float MoveSpeed = 2.0f;
    public float RotationSpeed = 500.0f;
    public bool IsGrounded;
    public float Gravity = -9.81f;
    private Quaternion targetRotation;
    private float yspeed;

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        float Amount = Mathf.Clamp01(Mathf.Abs(moveX) + Mathf.Abs(moveY));
        var moveInput = (new Vector3 (moveX, 0, moveY)).normalized;
        var moveDirection = CameraController.PlannerRotation * moveInput;

        IsGrounded = Physics.CheckSphere(transform.TransformPoint(_groundCheckOffset), _groundCheckRotation);

        if (IsGrounded)
        {
            yspeed = -0.5f;
        }
        else
        {
            yspeed += Gravity * Time.deltaTime;
        } 

        var velocity = moveDirection * MoveSpeed;
        velocity.y = yspeed;

        CharacterController.Move(velocity * Time.deltaTime);

        if (Amount > 0)
        {
            CharacterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
            targetRotation = Quaternion.LookRotation(moveDirection);
          
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 
            RotationSpeed * Time.deltaTime);

        _animator.SetFloat("Amount", Amount, 0.2f, Time.deltaTime);
    }
}

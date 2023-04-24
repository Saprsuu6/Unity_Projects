using UnityEngine;

public class Player : MonoBehaviour
{
    public static int CoinCount;
    public float Stamina;
    private float StaminaRestoreTime = 5;
    private float Speed = 5;
    private float jumpHeight = 1.0f;
    private float gravityValue = -5f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private CharacterController characterController;
    private Animator _animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        playerVelocity = Vector3.zero;
        CoinCount = 0;
        Stamina = 1;
    }

    void Update()
    {
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isRun = isShift && Stamina > 0.01f;
        float factor = Speed;    // * Time.deltaTime;
        if (isRun)
        {
            factor *= 2;
            Stamina -= Time.deltaTime / StaminaRestoreTime;
            if (Stamina < 0) Stamina = 0;
        }
        else if (!isShift)
        {
            Stamina += Time.deltaTime / StaminaRestoreTime;
            if (Stamina > 1) Stamina = 1;
        }
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        Vector3 moveDirection = (
            dx * transform.right +
            dz * transform.forward);
        if (moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized;
        }

        moveDirection *= factor;
        MoveState moveState;
        if (moveDirection.magnitude < .1)
        {
            moveState = MoveState.Idle;
        }
        else if (isRun)
        {
            if (Mathf.Abs(dx) < Mathf.Abs(dz))
                moveState = MoveState.Run;
            else
                moveState = MoveState.SideRun;
        }
        else
        {
            if (Mathf.Abs(dx) < Mathf.Abs(dz))
            {
                if (dz > 0)
                    moveState = MoveState.Walk;
                else
                    moveState = MoveState.Walk;
            }
            else
                moveState = MoveState.SideWalk;
        }
        _animator.SetInteger("PlayerState", (int)moveState);

        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
            Debug.Log("Jump");
            playerVelocity.y +=
                factor *
                Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        playerVelocity.y +=
            Speed * gravityValue * Time.deltaTime;

        moveDirection.y +=
            Speed * playerVelocity.y * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            groundedPlayer = true;
        }
    }

    private enum MoveState
    {
        Idle = 0,
        Walk,
        Run,
        SideWalk,
        SideRun
    }
}
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Vector2 moveInput {  get; private set; }
    public Vector2 camInput {  get; private set; }

    public Vector3 cameraForwardVector { get; private set; }

    public Rigidbody rb {  get; private set; }

    [field: SerializeField] public float groundDrag { get; private set; }

    [field: SerializeField] public float playerHeight {  get; private set; }

    [field: SerializeField] public LayerMask groundMask {  get; private set; }

    [field: SerializeField] public bool grounded { get; private set; }

    [field: SerializeField] public float airMultiplier {  get; private set; }

    AbilityData abilities;
    [SerializeField] bool jumping, canJump;

    [SerializeField]
    float jumpCooldown;

    [field: SerializeField] public Animator bodyAnimator {  get; private set; }

    public bool exitingSlope {  get; private set; }

    public delegate void OpenPauseMenuAction(bool open);
    public static event OpenPauseMenuAction OpenPauseMenuEvent;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Invoke(nameof(StartUp), 0.1f);
        
        rb = GetComponent<Rigidbody>();

        abilities = GameManager.instance.currentWorldData.GetAbilities();

        exitingSlope = false;
    }

    private void StartUp()
    {
        transform.position = GameManager.instance.currentWorldData.GetSpawn();
    }

    private void Update()
    {
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.3f, Vector3.down, out hit, (playerHeight * 0.5f) + 0.2f, groundMask);

        bodyAnimator.SetBool("Grounded", grounded);

        if (jumping && grounded && canJump)
        {
            bodyAnimator.Play(moveInput.magnitude > 0 ? "JumpWhileRunning" : "Jump_Up");
            canJump = false;

            abilities.JumpData.OnHold();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.y > 0)
        {
            bodyAnimator.transform.rotation = Quaternion.Slerp(bodyAnimator.transform.rotation, Quaternion.LookRotation(cameraForwardVector), 0.2f);
        }

        bodyAnimator.SetFloat("xInput", Mathf.Lerp(bodyAnimator.GetFloat("xInput"), moveInput.x, 0.1f));
        bodyAnimator.SetFloat("yInput", Mathf.Lerp(bodyAnimator.GetFloat("yInput"), moveInput.y, 0.1f));
    }

    private void ResetJump()
    {
        if (grounded)
        {
            canJump = true;
            exitingSlope = false;
        }
        else
        {
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    public void SetCamInput(Vector2 camInput)
    {
        this.camInput = camInput;
    }
    public void SetCameraForwardVector(Vector3 cameraForwardVector)
    {
        this.cameraForwardVector = cameraForwardVector.normalized;
    }

    public void CallJump(bool performed)
    {
        exitingSlope = true;
        jumping = performed;
        if (performed)
        {
            abilities.JumpData.OnPress();
        }
        else
        {
            abilities.JumpData.OnRelease();
        }
    }

    [SerializeField]
    float maxSlopeAngle;

    public bool OnSlope(out RaycastHit slopeHit)
    {
        if (Physics.SphereCast(transform.position, 0.3f, Vector3.down, out slopeHit, (playerHeight * 0.5f) + 0.3f, groundMask))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    public bool OnSlope()
    {
        RaycastHit slopeHit;
        if (Physics.SphereCast(transform.position, 0.3f, Vector3.down, out slopeHit, (playerHeight * 0.5f) + 0.3f, groundMask))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    public void OpenPauseMenu()
    {
        OpenPauseMenuEvent(true);
    }

    public void ClosePauseMenu()
    {
        OpenPauseMenuEvent(false);
    }
}

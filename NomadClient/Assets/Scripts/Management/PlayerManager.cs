using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    Vector2 moveInput;
    Vector2 camInput;

    Vector3 cameraForwardVector;

    [SerializeField]
    public static Vector3 gravity { get; private set; }

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {

    }

    public void SetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public void SetCamInput(Vector2 camInput)
    {
        this.camInput = camInput;
    }

    public Vector2 GetCamInput()
    {
        return camInput;
    }

    public void SetCameraForwardVector(Vector3 cameraForwardVector)
    {
        this.cameraForwardVector = cameraForwardVector;
    }

    public Vector3 GetCameraForwardVector()
    {
        return cameraForwardVector;
    }
}

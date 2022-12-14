using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonController : MonoBehaviour
{
    private Vector3 respawnPoint;

    public bool canMove { get; private set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => Input.GetKey(jumpKey) && characterController.isGrounded || Input.GetKey(jumpKey) && canDoubleJump == true;
    public bool canDoubleJump;

    Animator animator1;
    Animator animator2;
    Animator animator3;

    Animator animator4;
    Animator animator5;
    Animator animator6;

    public GameObject winScreen;

    public GameObject movingWall1;
    public GameObject movingWall2;
    public GameObject movingWall3;

    public GameObject movingWall4;
    public GameObject movingWall5;
    public GameObject movingWall6;



    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;

    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float gravity = 30f;

    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [SerializeField] private float jumpForce = 8.0f;


    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector3 currentInput;

    private float rotationX = 0;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }

    private void Update()
    {
        if (canMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
                HandleJump();

            ApplyFinalMovements();
        }

        if (Input.GetKeyDown("r") && winScreen.activeInHierarchy == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (shouldJump)
        {
            if (characterController.isGrounded)
            {
                moveDirection.y = jumpForce;
                canDoubleJump = true;
            }
            else if (canDoubleJump == true && Input.GetKeyDown(jumpKey))
            {
                moveDirection.y = jumpForce;
                canDoubleJump = false;
            }
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        else if (other.CompareTag("Spike"))
        {
            Debug.Log("Hit");
            transform.position = respawnPoint;
        }

        else if(other.CompareTag("checkPoint"))
            
        {
            Debug.Log("Checkpoint");
            respawnPoint = transform.position;
        }

        else if (other.CompareTag("Floor"))
        {

                    Debug.Log("Hit");
                    Destroy(GameObject.FindGameObjectWithTag("Door"));
                
        }

        else if (other.CompareTag("movingWallStart"))
        {
            Debug.Log("WallIsMoving");
            animator1 = movingWall1.GetComponent<Animator>();
            animator2 = movingWall2.GetComponent<Animator>();
            animator3 = movingWall3.GetComponent<Animator>();



            animator1.enabled= true;
            animator2.enabled= true;
            animator3.enabled= true;


        }

        else if (other.CompareTag("movingWallNext"))
        {
            animator4 = movingWall4.GetComponent<Animator>();
            animator5 = movingWall5.GetComponent<Animator>();
            animator6 = movingWall6.GetComponent<Animator>();

            animator4.enabled = true;
            animator5.enabled = true;
            animator6.enabled = true;
        }

        else if (other.CompareTag("cheese"))
        {
            Debug.Log("trigger");
            winScreen.SetActive(true);
            canMove = false;
        }
    }
}





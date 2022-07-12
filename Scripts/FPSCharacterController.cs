using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class FPSCharacterController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    CharacterController characterController;
    public Animator blakeAnimator;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;
    public float playerHealth = 100;
    [SerializeField]
    LayerMask terminalLayer;
    [SerializeField]
    LayerMask audioLayer;
    [SerializeField]
    GameObject gameController;
    terminalController tController;
    cannonQuizScript quizScript;
    bool onTerminal;
    [SerializeField]
    GameObject blueDragon;
    blueDragonScript bdScript;
    PauseMenu pMenu;
    [SerializeField] GameObject PauseUI;
    //[SerializeField] GameObject GameOverUI;
    private float previousTimeScale = 1f;
    private bool paused = false;
    [SerializeField]
    GameObject interactCanvas;
    


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Application.targetFrameRate = 60;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tController = gameController.GetComponent<terminalController>();
        quizScript = gameController.GetComponent<cannonQuizScript>();
        bdScript = blueDragon.GetComponent<blueDragonScript>();
        pMenu = playerCamera.GetComponent<PauseMenu>();
        PauseUI.SetActive(false);
    }

    




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (onTerminal)
            {
                tController.closeTerminal();
                onTerminal = false;
                pMenu.changeTerminalStatus(false);
            }
            else
            {
                Debug.Log("Works");
                paused = !paused;
                if (paused)
                {
                    PauseUI.SetActive(true);
                    previousTimeScale = Time.timeScale;
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (!paused)
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        Ray ray3 = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit3;
        if (Physics.Raycast(ray3, out hit3, 6, terminalLayer) && !paused && !onTerminal || Physics.Raycast(ray3, out hit3, 6, audioLayer) && !paused && !onTerminal)
        {
            interactCanvas.SetActive(true);
        }
        else
        {
            interactCanvas.SetActive(false);
        }



        if (Input.GetKeyDown(KeyCode.E) && !onTerminal && !paused)
        {
            Ray ray2 = playerCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray2, out hit, 6, terminalLayer))
            {
                onTerminal = true;
                pMenu.changeTerminalStatus(true);
                tController.previousTerminalNum = tController.currentTerminalNum;
                tController.currentTerminalNum = hit.transform.gameObject.GetComponent<terminalNumber>().num;
                tController.changeTerminal();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
             


            }
            else if(Physics.Raycast(ray2, out hit, 6, audioLayer))
            {
                AudioSource activeAudio = hit.transform.gameObject.GetComponent<AudioSource>();
                activeAudio.Play();
            }
        }
        

        if(!onTerminal && !quizScript.quizActive && !paused)
        {
     
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            if (Input.GetKey("space"))
            {
                blakeAnimator.SetBool("IsJumping", true);
            }
            else
            {
                blakeAnimator.SetBool("IsJumping", false);
            }

            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                blakeAnimator.SetBool("IsWalking", true);
            }
            else
            {
                blakeAnimator.SetBool("IsWalking", false);
            }

            if (Input.GetKey("a"))
            {
                blakeAnimator.SetBool("IsWalkingLeft", true);
            }
            else
            {
                blakeAnimator.SetBool("IsWalkingLeft", false);
            }

            if (Input.GetKey("d"))
            {
                blakeAnimator.SetBool("IsWalkingRight", true);
            }
            else
            {
                blakeAnimator.SetBool("IsWalkingRight", false);
            }

            if (Input.GetKey("left shift"))
            {
                blakeAnimator.SetBool("IsRunning", true);
            }
            else
            {
                blakeAnimator.SetBool("IsRunning", false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("quiz"))
        {
            quizScript.StartQuiz();
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bdScript.teleportToFinalPos();
        }

        if(other.gameObject.CompareTag("dragon"))
        {
            bdScript.startDragonFlying();
        }
    }


    //Function for the resume button - Resumes time within the game and allows the player to continue playing.
    public void Resume()
    {
        paused = false;
        Time.timeScale = previousTimeScale;
        PauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Function for the restart button - Returns the player to their spawnpoint and resets the level.
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
        //GameOverUI.SetActive(false);
        Debug.Log(Time.timeScale);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    //Function for the main menu button - Returns the player to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    //Function for quit button - Makes the application shut down.
    public void Quit()
    {
        Application.Quit();
    }

}
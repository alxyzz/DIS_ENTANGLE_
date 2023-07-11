using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    #region Combat
    bool isAlive, hasWeapon = true;
    [SerializeReference]GameObject weapon;
    [SerializeReference]Light Flashlight;
    [SerializeReference] Slider healthSlider;
    [SerializeReference] Animation wepAnim;

    public AudioSource loserSource;
    public List<AudioClip> NegativeSelfImageInducingVocalizations = new();

    int _health = 100;
    public int Health { get { return _health; } }


    #endregion
    #region Controls
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    private bool CAN_MOVE
    {
        get
        {
            if (!isAlive)
            {
                return false;
            }
            else return canMove;
        }
    }

    public void PickWeaponUp()
    {
        weapon.SetActive(true);
        hasWeapon = true;
    }


    void MeleeAttack()
    {
        if (wepAnim.isPlaying || !hasWeapon)
        {
            return;
        }
        RaycastHit hit;
        wepAnim.Play();
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Enemy enemyHit = hit.collider.GetComponent<Enemy>();
            if (enemyHit != null)
            {
                Debug.Log("just hit an enemy.");
                enemyHit.ReceiveHit(transform.position);
                
            }else
            { 
            Debug.Log("just hit empty air.");
            }
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && hasWeapon)
        {
            MeleeAttack();
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
    }
   
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {

            case "WolfEscape":
                SceneManager.LoadScene("VideoTest", LoadSceneMode.Single);
                //loads the video
                break;
            default:
                break;
        }
    }

    public void GetHit()
    {
        _health -= 15;

        if (NegativeSelfImageInducingVocalizations.Count > 0)
        {
            int randomIndex = Random.Range(0, NegativeSelfImageInducingVocalizations.Count);
            AudioClip randomSound = NegativeSelfImageInducingVocalizations[randomIndex];
            loserSource.PlayOneShot(randomSound);
        }


        if (_health < 0)
        {
            WolfFPSManager.Instance.Lose();
        }
    }


    






    #endregion



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // To load the new scene

namespace FirstPersonMobileTools.DynamicFirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Joystick m_Joystick;
        [SerializeField] private float m_Acceleration = 1.0f;
        [SerializeField] private float m_WalkSpeed = 4.0f;
        [SerializeField] private Animator m_Animator;

        // Camera settings
        private Camera m_Camera;
        private CharacterController m_CharacterController;
        private CameraLook m_CameraLook;
        private Vector3 m_HeadMovement;

        private Vector3 m_MovementDirection;

        // Raycast for detecting interactions
        [SerializeField] private float interactionRange = 3f;

        private void Start()
        {
            m_Camera = GetComponentInChildren<Camera>();
            m_CharacterController = GetComponent<CharacterController>();
            m_CameraLook = GetComponent<CameraLook>();

            if (m_Animator == null)
            {
                m_Animator = GetComponentInChildren<Animator>();
                if (m_Animator == null)
                {
                    Debug.LogError("Animator component missing on 'Player' GameObject or its children.");
                }
            }

            if (m_Joystick == null)
            {
                Debug.LogError("Joystick component is missing in the scene.");
            }
        }

        private void Update()
        {
            Handle_InputMovement();
            Handle_Animations();

            if (m_CharacterController != null)
            {
                m_CharacterController.Move(m_MovementDirection * Time.deltaTime);
            }

            if (m_Camera != null)
            {
                m_Camera.transform.localPosition += m_HeadMovement;
            }
            m_HeadMovement = Vector3.zero;

            // Handle Interaction with objects tagged as "InteractionUI"
            Handle_Interaction();
        }

        private void Handle_InputMovement()
        {
            if (m_Joystick == null) return;

            float inputX = m_Joystick.Horizontal;
            float inputZ = m_Joystick.Vertical;

            Vector3 moveDirection = new Vector3(inputX, 0, inputZ).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                m_MovementDirection = transform.forward * m_WalkSpeed;
            }
            else
            {
                m_MovementDirection = Vector3.zero;
            }
        }

        private void Handle_Animations()
        {
            float speed = m_MovementDirection.magnitude / m_WalkSpeed;

            if (speed > 0.1f)
            {
                if (speed > 0.5f)
                {
                    m_Animator.SetBool("isRun", true);
                    m_Animator.SetBool("isWalk", false);
                }
                else
                {
                    m_Animator.SetBool("isRun", false);
                    m_Animator.SetBool("isWalk", true);
                }
            }
            else
            {
                m_Animator.SetBool("isRun", false);
                m_Animator.SetBool("isWalk", false);
            }
        }

        private void Handle_Interaction()
        {
            RaycastHit hit;

            // Cast a ray from the camera's position to where the user is looking
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, interactionRange))
            {
                // Check if the object has the "InteractionUI" tag
                if (hit.collider.CompareTag("interactionUI"))
                {
                    // Check for user input (e.g., a button press or interaction)
                    if (Input.GetButtonDown("Interact")) // Replace "Interact" with your own input system if needed
                    {
                        // Switch to the RiddleScene
                        SceneManager.LoadScene("RiddleScene");
                    }
                }
            }
        }
    }
}

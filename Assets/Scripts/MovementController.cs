// using UnityEngine;
// using FirstPersonMobileTools.Utility;

// namespace FirstPersonMobileTools.DynamicFirstPerson
// {

//     [RequireComponent(typeof(AudioSource))]
//     [RequireComponent(typeof(CharacterController))]
//     [RequireComponent(typeof(CameraLook))]
//     public class MovementController : MonoBehaviour
//     {

//         #region Class accessible field

//         [HideInInspector] public float Walk_Speed { private get { return m_WalkSpeed; } set { m_WalkSpeed = value; } }          // Accessed through [Walk speed] slider in the settings
//         [HideInInspector] public float Acceleration { private get { return m_Acceleration; } set { m_Acceleration = value; } }  // Accessed through [Acceleration] slider in the settings
//         #endregion

//         #region Editor accessible field
//         // Input Settings
//         [SerializeField] private Joystick m_Joystick;   // Available joystick mobile in the scene

//         // Ground Movement Settings
//         [SerializeField] private float m_Acceleration = 1.0f;
//         [SerializeField] private float m_WalkSpeed = 1.0f;

//         // Audio Settings
//         [SerializeField] private AudioClip[] m_FootStepSounds;  // list of foot step sfx

//         // Advanced Settings
//         [SerializeField] private Bobbing m_WalkBob = new Bobbing(); // Bobbing for walking
//         [SerializeField] private Bobbing m_IdleBob = new Bobbing(); // Bobbing for idling
//         #endregion

//         // Main reference class
//         private Camera m_Camera;
//         private CharacterController m_CharacterController;
//         private CameraLook m_CameraLook;
//         private AudioSource m_AudioSource;

//         // Main global value
//         private Vector3 m_MovementDirection;                        // Vector3 value for CharacterController.Move()
//         private Vector3 m_HeadMovement;                             // Used for calculating all the head movement before applying to the camera position

//         private float m_MovementVelocity
//         {
//             get { return new Vector2(m_CharacterController.velocity.x, m_CharacterController.velocity.z).magnitude; }
//         }

//         private Vector2 Input_Movement
//         {
//             get { if (m_Joystick != null) return new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical); else return Vector2.zero; }
//         }

//         private bool m_IsWalking
//         {
//             get { return m_MovementVelocity > 0.0f; }
//         }

//         private float m_speed
//         {
//             get
//             {
// #if UNITY_EDITOR
//                 return Input_Movement.magnitude != 0 || External_Input_Movement.magnitude != 0? m_WalkSpeed : 0.0f; 
// #elif UNITY_ANDROID
//                 return Input_Movement.magnitude != 0? m_WalkSpeed : 0.0f; 
// #endif
//             }
//         }

// #if UNITY_EDITOR
//         public Vector2 External_Input_Movement;
// #endif


//         private void Start()
//         {

//             m_Camera = GetComponentInChildren<Camera>();
//             m_AudioSource = GetComponent<AudioSource>();
//             m_CharacterController = GetComponent<CharacterController>();
//             m_CameraLook = GetComponent<CameraLook>();

//             m_CharacterController.height = m_CharacterController.height;
//             // m_OriginalScale = transform.localScale;

//             m_WalkBob.SetUp();
//             m_IdleBob.SetUp();

//         }

//         private void Update()
//         {

//             Handle_InputMovement();
//             Handle_Step();

//             UpdateWalkBob();

//             m_CharacterController.Move(m_MovementDirection * Time.deltaTime);

//             m_Camera.transform.localPosition += m_HeadMovement;
//             m_HeadMovement = Vector3.zero;
//         }

//         private void Handle_InputMovement()
//         {

//             Vector2 Input;
// #if UNITY_EDITOR
//             Input.x = Input_Movement.x == 0? External_Input_Movement.x : Input_Movement.x;
//             Input.y = Input_Movement.y == 0? External_Input_Movement.y : Input_Movement.y;
// #elif UNITY_ANDROID
//             Input.x = Input_Movement.x;
//             Input.y = Input_Movement.y;
// #endif
//             Vector3 WalkTargetDirection =
//                 Input.y * transform.forward * m_speed +
//                 Input.x * transform.right * m_speed;

//             WalkTargetDirection = WalkTargetDirection == Vector3.zero ? transform.forward * m_speed : WalkTargetDirection;

//             m_MovementDirection.x = Mathf.MoveTowards(m_MovementDirection.x, WalkTargetDirection.x, m_Acceleration * Time.deltaTime);
//             m_MovementDirection.z = Mathf.MoveTowards(m_MovementDirection.z, WalkTargetDirection.z, m_Acceleration * Time.deltaTime);
//         }

//         private void Handle_Step()
//         {

//             if (m_FootStepSounds.Length == 0) return;
//             if (m_WalkBob.OnStep) PlaySound(m_FootStepSounds[UnityEngine.Random.Range(0, m_FootStepSounds.Length - 1)]);

//         }

//         private void UpdateWalkBob()
//         {

//             if ((m_IsWalking) || !m_WalkBob.BackToOriginalPosition)
//             {
//                 float speed = m_MovementVelocity == 0 ? m_WalkSpeed : m_MovementVelocity;
//                 m_HeadMovement += m_WalkBob.UpdateBobValue(speed, m_WalkBob.BobRange);
//             }
//             else if (!m_IsWalking || !m_IdleBob.BackToOriginalPosition)
//             {
//                 m_HeadMovement += m_IdleBob.UpdateBobValue(1, m_IdleBob.BobRange);
//             }

//         }

//         // Utility function
//         private void PlaySound(AudioClip audioClip)
//         {
//             m_AudioSource.clip = audioClip;
//             if (m_AudioSource.clip != null) m_AudioSource.PlayOneShot(m_AudioSource.clip);
//         }

//     }

// }

//part2
// using UnityEngine;
// using FirstPersonMobileTools.Utility;

// namespace FirstPersonMobileTools.DynamicFirstPerson
// {
//     [RequireComponent(typeof(AudioSource))]
//     [RequireComponent(typeof(CharacterController))]
//     [RequireComponent(typeof(CameraLook))]
//     public class MovementController : MonoBehaviour
//     {
//         #region Class accessible field

//         [HideInInspector] public float Walk_Speed { get { return m_WalkSpeed; } private set { m_WalkSpeed = value; } }          // Accessed through [Walk speed] slider in the settings
//         [HideInInspector] public float Acceleration { get { return m_Acceleration; } private set { m_Acceleration = value; } }  // Accessed through [Acceleration] slider in the settings

//         #endregion

//         #region Editor accessible field

//         // Input Settings
//         [SerializeField] private Joystick m_Joystick;   // Available joystick mobile in the scene

//         // Ground Movement Settings
//         [SerializeField] private float m_Acceleration = 1.0f;
//         [SerializeField] private float m_WalkSpeed = 1.0f;

//         // Animation Settings
//         [SerializeField] private Animator m_Animator; // Animator component for handling animations

//         // Audio Settings
//         [SerializeField] private AudioClip[] m_FootStepSounds;  // list of foot step sfx

//         // Advanced Settings
//         [SerializeField] private Bobbing m_WalkBob = new Bobbing(); // Bobbing for walking
//         [SerializeField] private Bobbing m_IdleBob = new Bobbing(); // Bobbing for idling

//         #endregion

//         // Main reference class
//         private Camera m_Camera;
//         private CharacterController m_CharacterController;
//         private CameraLook m_CameraLook;
//         private AudioSource m_AudioSource;

//         // Main global value
//         private Vector3 m_MovementDirection;                        // Vector3 value for CharacterController.Move()
//         private Vector3 m_HeadMovement;                             // Used for calculating all the head movement before applying to the camera position

//         private float m_MovementVelocity
//         {
//             get { return new Vector2(m_CharacterController.velocity.x, m_CharacterController.velocity.z).magnitude; }
//         }

//         private Vector2 Input_Movement
//         {
//             get { if (m_Joystick != null) return new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical); else return Vector2.zero; }
//         }

//         private bool m_IsWalking
//         {
//             get { return m_MovementVelocity > 0.0f; }
//         }

//         private float m_speed
//         {
//             get
//             {
// #if UNITY_EDITOR
//                 return Input_Movement.magnitude != 0 || External_Input_Movement.magnitude != 0 ? m_WalkSpeed : 0.0f;
// #elif UNITY_ANDROID
//                 return Input_Movement.magnitude != 0 ? m_WalkSpeed : 0.0f;
// #endif
//             }
//         }

// #if UNITY_EDITOR
//         public Vector2 External_Input_Movement;
// #endif

//         private void Start()
//         {
//             m_Camera = GetComponentInChildren<Camera>();
//             m_AudioSource = GetComponent<AudioSource>();
//             m_CharacterController = GetComponent<CharacterController>();
//             m_CameraLook = GetComponent<CameraLook>();

//             if (m_Animator == null)
//             {
//                 m_Animator = GetComponentInChildren<Animator>();
//                 if (m_Animator == null)
//                 {
//                     Debug.LogError("Animator component missing on 'Player' GameObject or its children.");
//                 }
//             }

//             if (m_Joystick == null)
//             {
//                 Debug.LogError("Joystick component is missing in the scene.");
//             }

//             m_WalkBob.SetUp();
//             m_IdleBob.SetUp();
//         }

//         private void Update()
//         {
//             if (m_Joystick == null)
//             {
//                 Debug.LogError("Joystick is not assigned.");
//             }

//             Handle_InputMovement();
//             //Handle_Step();
//             UpdateWalkBob();

//             if (m_CharacterController != null)
//             {
//                 m_CharacterController.Move(m_MovementDirection * Time.deltaTime);
//             }

//             if (m_Camera != null)
//             {
//                 m_Camera.transform.localPosition += m_HeadMovement;
//             }
//             m_HeadMovement = Vector3.zero;

//             Handle_Animations();
//         }

//         private void Handle_InputMovement()
//         {
//             Vector2 Input;
// #if UNITY_EDITOR
//             Input.x = Input_Movement.x == 0 ? External_Input_Movement.x : Input_Movement.x;
//             Input.y = Input_Movement.y == 0 ? External_Input_Movement.y : Input_Movement.y;
// #elif UNITY_ANDROID
//             Input.x = Input_Movement.x;
//             Input.y = Input_Movement.y;
// #endif
//             Vector3 WalkTargetDirection =
//                 Input.y * transform.forward * m_speed +
//                 Input.x * transform.right * m_speed;

//             WalkTargetDirection = WalkTargetDirection == Vector3.zero ? transform.forward * m_speed : WalkTargetDirection;

//             m_MovementDirection.x = Mathf.MoveTowards(m_MovementDirection.x, WalkTargetDirection.x, m_Acceleration * Time.deltaTime);
//             m_MovementDirection.z = Mathf.MoveTowards(m_MovementDirection.z, WalkTargetDirection.z, m_Acceleration * Time.deltaTime);
//         }

//         // private void Handle_Step()
//         // {
//         //     if (m_FootStepSounds.Length == 0) return;
//         //     if (m_WalkBob.OnStep) PlaySound(m_FootStepSounds[UnityEngine.Random.Range(0, m_FootStepSounds.Length - 1)]);
//         // }

//         private void UpdateWalkBob()
//         {
//             if ((m_IsWalking) || !m_WalkBob.BackToOriginalPosition)
//             {
//                 float speed = m_MovementVelocity == 0 ? m_WalkSpeed : m_MovementVelocity;
//                 m_HeadMovement += m_WalkBob.UpdateBobValue(speed, m_WalkBob.BobRange);
//             }
//             else if (!m_IsWalking || !m_IdleBob.BackToOriginalPosition)
//             {
//                 m_HeadMovement += m_IdleBob.UpdateBobValue(1, m_IdleBob.BobRange);
//             }
//         }

//         private void Handle_Animations()
//         {
//             float inputX = Input_Movement.x;
//             float inputZ = Input_Movement.y;

//             float speed = new Vector2(inputX, inputZ).magnitude;

//             // Update the speed parameter in the Animator for blending
//             m_Animator.SetFloat("speed", speed);

//             if (speed > 0.1f)
//             {
//                 if (inputZ > 0.5f)
//                 {
//                     m_Animator.SetBool("isRun", true);
//                     m_Animator.SetBool("isWalk", false);
//                 }
//                 else
//                 {
//                     m_Animator.SetBool("isRun", false);
//                     m_Animator.SetBool("isWalk", true);
//                 }
//             }
//             else
//             {
//                 m_Animator.SetBool("isRun", false);
//                 m_Animator.SetBool("isWalk", false);
//             }
//         }

//     }
// }

//part3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}

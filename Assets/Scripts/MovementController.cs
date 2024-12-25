using UnityEngine;
using FirstPersonMobileTools.Utility;

namespace FirstPersonMobileTools.DynamicFirstPerson
{

    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CameraLook))]
    public class MovementController : MonoBehaviour
    {

        #region Class accessible field

        [HideInInspector] public float Walk_Speed { private get { return m_WalkSpeed; } set { m_WalkSpeed = value; } }          // Accessed through [Walk speed] slider in the settings
        [HideInInspector] public float Acceleration { private get { return m_Acceleration; } set { m_Acceleration = value; } }  // Accessed through [Acceleration] slider in the settings
        #endregion

        #region Editor accessible field
        // Input Settings
        [SerializeField] private Joystick m_Joystick;   // Available joystick mobile in the scene

        // Ground Movement Settings
        [SerializeField] private float m_Acceleration = 1.0f;
        [SerializeField] private float m_WalkSpeed = 1.0f;

        // Audio Settings
        [SerializeField] private AudioClip[] m_FootStepSounds;  // list of foot step sfx

        // Advanced Settings
        [SerializeField] private Bobbing m_WalkBob = new Bobbing(); // Bobbing for walking
        [SerializeField] private Bobbing m_IdleBob = new Bobbing(); // Bobbing for idling
        #endregion

        // Main reference class
        private Camera m_Camera;
        private CharacterController m_CharacterController;
        private CameraLook m_CameraLook;
        private AudioSource m_AudioSource;

        // Main global value
        private Vector3 m_MovementDirection;                        // Vector3 value for CharacterController.Move()
        private Vector3 m_HeadMovement;                             // Used for calculating all the head movement before applying to the camera position

        private float m_MovementVelocity
        {
            get { return new Vector2(m_CharacterController.velocity.x, m_CharacterController.velocity.z).magnitude; }
        }

        private Vector2 Input_Movement
        {
            get { if (m_Joystick != null) return new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical); else return Vector2.zero; }
        }

        private bool m_IsWalking
        {
            get { return m_MovementVelocity > 0.0f; }
        }

        private float m_speed
        {
            get
            {
#if UNITY_EDITOR
                return Input_Movement.magnitude != 0 || External_Input_Movement.magnitude != 0? m_WalkSpeed : 0.0f; 
#elif UNITY_ANDROID
                return Input_Movement.magnitude != 0? m_WalkSpeed : 0.0f; 
#endif
            }
        }

#if UNITY_EDITOR
        public Vector2 External_Input_Movement;
#endif


        private void Start()
        {

            m_Camera = GetComponentInChildren<Camera>();
            m_AudioSource = GetComponent<AudioSource>();
            m_CharacterController = GetComponent<CharacterController>();
            m_CameraLook = GetComponent<CameraLook>();

            m_CharacterController.height = m_CharacterController.height;
            // m_OriginalScale = transform.localScale;

            m_WalkBob.SetUp();
            m_IdleBob.SetUp();

        }

        private void Update()
        {

            Handle_InputMovement();
            Handle_Step();

            UpdateWalkBob();

            m_CharacterController.Move(m_MovementDirection * Time.deltaTime);

            m_Camera.transform.localPosition += m_HeadMovement;
            m_HeadMovement = Vector3.zero;
        }

        private void Handle_InputMovement()
        {

            Vector2 Input;
#if UNITY_EDITOR
            Input.x = Input_Movement.x == 0? External_Input_Movement.x : Input_Movement.x;
            Input.y = Input_Movement.y == 0? External_Input_Movement.y : Input_Movement.y;
#elif UNITY_ANDROID
            Input.x = Input_Movement.x;
            Input.y = Input_Movement.y;
#endif
            Vector3 WalkTargetDirection =
                Input.y * transform.forward * m_speed +
                Input.x * transform.right * m_speed;

            WalkTargetDirection = WalkTargetDirection == Vector3.zero ? transform.forward * m_speed : WalkTargetDirection;

            m_MovementDirection.x = Mathf.MoveTowards(m_MovementDirection.x, WalkTargetDirection.x, m_Acceleration * Time.deltaTime);
            m_MovementDirection.z = Mathf.MoveTowards(m_MovementDirection.z, WalkTargetDirection.z, m_Acceleration * Time.deltaTime);
        }

        private void Handle_Step()
        {

            if (m_FootStepSounds.Length == 0) return;
            if (m_WalkBob.OnStep) PlaySound(m_FootStepSounds[UnityEngine.Random.Range(0, m_FootStepSounds.Length - 1)]);

        }

        private void UpdateWalkBob()
        {

            if ((m_IsWalking) || !m_WalkBob.BackToOriginalPosition)
            {
                float speed = m_MovementVelocity == 0 ? m_WalkSpeed : m_MovementVelocity;
                m_HeadMovement += m_WalkBob.UpdateBobValue(speed, m_WalkBob.BobRange);
            }
            else if (!m_IsWalking || !m_IdleBob.BackToOriginalPosition)
            {
                m_HeadMovement += m_IdleBob.UpdateBobValue(1, m_IdleBob.BobRange);
            }

        }

        // Utility function
        private void PlaySound(AudioClip audioClip)
        {
            m_AudioSource.clip = audioClip;
            if (m_AudioSource.clip != null) m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }

    }

}
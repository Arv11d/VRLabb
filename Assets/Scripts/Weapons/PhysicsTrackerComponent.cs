using Unity.Labs.SuperScience;
using UnityEngine;

public class PhysicsTrackerComponent : MonoBehaviour
{
    [Header("Physics Tracking")]
        [SerializeField] private bool autoUpdate = true;
        [SerializeField] private bool useFixedUpdate = false;
        
        [Header("Initial State")]
        [SerializeField] private Vector3 initialVelocity = Vector3.zero;
        [SerializeField] private Vector3 initialAngularVelocity = Vector3.zero;
        
        [Header("Debug Info (Read Only)")]
        [SerializeField, ReadOnly] private float speed;
        [SerializeField, ReadOnly] private Vector3 velocity;
        [SerializeField, ReadOnly] private Vector3 acceleration;
        [SerializeField, ReadOnly] private float angularSpeed;
        [SerializeField, ReadOnly] private Vector3 angularVelocity;
        [SerializeField, ReadOnly] private Vector3 angularAcceleration;

        private PhysicsTracker tracker;
        private Vector3 lastPosition;
        private Quaternion lastRotation;
        private float lastTime;

        // Public properties to access tracker data
        public float Speed => tracker?.Speed ?? 0f;
        public float AccelerationStrength => tracker?.AccelerationStrength ?? 0f;
        public Vector3 Direction => tracker?.Direction ?? Vector3.forward;
        public Vector3 Velocity => tracker?.Velocity ?? Vector3.zero;
        public Vector3 Acceleration => tracker?.Acceleration ?? Vector3.zero;
        public float AngularSpeed => tracker?.AngularSpeed ?? 0f;
        public Vector3 AngularAxis => tracker?.AngularAxis ?? Vector3.up;
        public Vector3 AngularVelocity => tracker?.AngularVelocity ?? Vector3.zero;
        public float AngularAccelerationStrength => tracker?.AngularAccelerationStrength ?? 0f;
        public Vector3 AngularAcceleration => tracker?.AngularAcceleration ?? Vector3.zero;

        private void Start()
        {
            InitializeTracker();
        }

        private void Update()
        {
            if (autoUpdate && !useFixedUpdate)
            {
                UpdateTracker();
            }
            
            UpdateDebugInfo();
        }

        private void FixedUpdate()
        {
            if (autoUpdate && useFixedUpdate)
            {
                UpdateTracker();
            }
        }

        /// <summary>
        /// Initialize the physics tracker with current transform state
        /// </summary>
        public void InitializeTracker()
        {
            tracker = new PhysicsTracker();
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            lastTime = Time.time;
            
            tracker.Reset(transform.position, transform.rotation, initialVelocity, initialAngularVelocity);
        }

        /// <summary>
        /// Manually update the tracker (called automatically if autoUpdate is true)
        /// </summary>
        public void UpdateTracker()
        {
            if (tracker == null)
            {
                InitializeTracker();
                return;
            }

            float currentTime = useFixedUpdate ? Time.fixedTime : Time.time;
            float deltaTime = currentTime - lastTime;
            
            if (deltaTime > 0f)
            {
                tracker.Update(transform.position, transform.rotation, deltaTime);
                lastTime = currentTime;
            }
        }

        /// <summary>
        /// Reset the tracker to a specific state
        /// </summary>
        /// <param name="position">Starting position</param>
        /// <param name="rotation">Starting rotation</param>
        /// <param name="velocity">Starting velocity</param>
        /// <param name="angularVelocity">Starting angular velocity</param>
        public void ResetTracker(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
        {
            if (tracker == null)
                tracker = new PhysicsTracker();
                
            tracker.Reset(position, rotation, velocity, angularVelocity);
            lastPosition = position;
            lastRotation = rotation;
            lastTime = useFixedUpdate ? Time.fixedTime : Time.time;
        }

        /// <summary>
        /// Reset the tracker using current transform values
        /// </summary>
        public void ResetTrackerToCurrent()
        {
            ResetTracker(transform.position, transform.rotation, initialVelocity, initialAngularVelocity);
        }

        private void UpdateDebugInfo()
        {
            if (tracker != null)
            {
                speed = tracker.Speed;
                velocity = tracker.Velocity;
                acceleration = tracker.Acceleration;
                angularSpeed = tracker.AngularSpeed;
                angularVelocity = tracker.AngularVelocity;
                angularAcceleration = tracker.AngularAcceleration;
            }
        }

        private void OnValidate()
        {
            // Update debug info in editor
            if (Application.isPlaying)
            {
                UpdateDebugInfo();
            }
        }
    }

    /// <summary>
    /// Attribute to make fields read-only in the inspector
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            UnityEditor.EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif

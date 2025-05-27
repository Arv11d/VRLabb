using System.Linq;
using Unity.Labs.SuperScience;
using UnityEngine;

public class StabilizWrapper : MonoBehaviour
{
    [Header("Stabilization Setup")]
        [SerializeField]
        [Tooltip("The object to follow (e.g., VR controller, hand tracker)")]
        private Transform followTarget;
        
        [SerializeField]
        [Tooltip("The point to stabilize against (e.g., tip of a tool, end of object)")]
        private Transform stabilizationPoint;
        
        [Header("Stabilization Options")]
        [SerializeField]
        [Tooltip("Use previous orientation for stabilization (smooth rotation)")]
        private bool usePreviousOrientation = true;
        
        [SerializeField]
        [Tooltip("Use endpoint stabilization (keeps tip steady)")]
        private bool useEndpointStabilization = true;
        
        [Header("Auto Setup")]
        [SerializeField]
        [Tooltip("Automatically find stabilization point in children")]
        private bool autoFindStabilizationPoint = true;
        
        [SerializeField]
        [Tooltip("Name or tag to search for when auto-finding stabilization point")]
        private string stabilizationPointName = "Tip";
        
        // Reference to the actual Stabilizr component
        private Stabilizr stabilizr;
        
        void Awake()
        {
            SetupStabilizr();
        }
        
        void Start()
        {
            // Auto-find stabilization point if enabled and not manually set
            if (autoFindStabilizationPoint && stabilizationPoint == null)
            {
                FindStabilizationPoint();
            }
            
            // Validate setup
            ValidateSetup();
        }
        
        /// <summary>
        /// Sets up the Stabilizr component with current settings
        /// </summary>
        public void SetupStabilizr()
        {
            // Get or add Stabilizr component
            stabilizr = GetComponent<Stabilizr>();
            if (stabilizr == null)
            {
                stabilizr = gameObject.AddComponent<Stabilizr>();
            }
            
            ApplySettings();
        }
        
        /// <summary>
        /// Applies current wrapper settings to the Stabilizr component
        /// </summary>
        public void ApplySettings()
        {
            if (stabilizr == null) return;
            
            // Use reflection to set private fields since they're serialized
            var type = typeof(Stabilizr);
            
            if (followTarget != null)
            {
                var followField = type.GetField("m_FollowSource", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                followField?.SetValue(stabilizr, followTarget);
            }
            
            if (stabilizationPoint != null)
            {
                var stabilizationField = type.GetField("m_StabilizationPoint", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                stabilizationField?.SetValue(stabilizr, stabilizationPoint);
            }
            
            var usePrevField = type.GetField("m_UsePreviousOrientation", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            usePrevField?.SetValue(stabilizr, usePreviousOrientation);
            
            var useEndField = type.GetField("m_UseEndPoint", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            useEndField?.SetValue(stabilizr, useEndpointStabilization);
        }
        
        /// <summary>
        /// Automatically finds a stabilization point in children
        /// </summary>
        private void FindStabilizationPoint()
        {
            // First try to find by name
            Transform found = transform.Find(stabilizationPointName);
            
            // If not found by name, try to find by tag
            if (found == null)
            {
                Transform[] children = GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if (child != transform && 
                        (child.name.Contains(stabilizationPointName) || 
                         child.CompareTag(stabilizationPointName)))
                    {
                        found = child;
                        break;
                    }
                }
            }
            
            // If still not found, use the last child (common pattern for tool tips)
            if (found == null && transform.childCount > 0)
            {
                found = transform.GetChild(transform.childCount - 1);
                Debug.Log($"StabilizrWrapper: Auto-selected last child '{found.name}' as stabilization point");
            }
            
            stabilizationPoint = found;
        }
        
        /// <summary>
        /// Validates the current setup and logs warnings if needed
        /// </summary>
        private void ValidateSetup()
        {
            if (followTarget == null)
            {
                Debug.LogWarning($"StabilizrWrapper on {gameObject.name}: No follow target assigned!");
            }
            
            if (stabilizationPoint == null)
            {
                Debug.LogWarning($"StabilizrWrapper on {gameObject.name}: No stabilization point found or assigned!");
            }
            
            if (!usePreviousOrientation && !useEndpointStabilization)
            {
                Debug.LogWarning($"StabilizrWrapper on {gameObject.name}: Both stabilization methods are disabled!");
            }
        }
        
        /// <summary>
        /// Updates the follow target at runtime
        /// </summary>
        /// <param name="newTarget">New transform to follow</param>
        public void SetFollowTarget(Transform newTarget)
        {
            followTarget = newTarget;
            ApplySettings();
        }
        
        /// <summary>
        /// Updates the stabilization point at runtime
        /// </summary>
        /// <param name="newPoint">New stabilization point</param>
        public void SetStabilizationPoint(Transform newPoint)
        {
            stabilizationPoint = newPoint;
            ApplySettings();
        }
        
        /// <summary>
        /// Enables or disables previous orientation stabilization
        /// </summary>
        /// <param name="enabled">Whether to use previous orientation</param>
        public void SetUsePreviousOrientation(bool enabled)
        {
            usePreviousOrientation = enabled;
            ApplySettings();
        }
        
        /// <summary>
        /// Enables or disables endpoint stabilization
        /// </summary>
        /// <param name="enabled">Whether to use endpoint stabilization</param>
        public void SetUseEndpointStabilization(bool enabled)
        {
            useEndpointStabilization = enabled;
            ApplySettings();
        }
        
        // Editor helper methods
        #if UNITY_EDITOR
        void OnValidate()
        {
            if (Application.isPlaying)
            {
                ApplySettings();
            }
        }
        
        [ContextMenu("Setup Stabilizr Now")]
        void EditorSetupStabilizr()
        {
            SetupStabilizr();
            UnityEditor.EditorUtility.SetDirty(this);
        }
        
        [ContextMenu("Find Stabilization Point")]
        void EditorFindStabilizationPoint()
        {
            FindStabilizationPoint();
            UnityEditor.EditorUtility.SetDirty(this);
        }
        #endif
    
}

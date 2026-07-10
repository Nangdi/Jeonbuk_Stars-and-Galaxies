/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Core logic for Sticky Stick Stuck for 2D.
*******************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

namespace StickyStickStuck
{
    [RequireComponent(typeof(Rigidbody2D)), AddComponentMenu("Physics 2D/Sticky Stick Stuck 2D", -1)]
    public class SSS2D : MonoBehaviour
    {
        #region Classes

        //Manages all filter type properties
        [System.Serializable]
        public class FilterProperties
        {
            //Filter effect types
            public enum EffectType
            {
                Effect,
                DontEffect,
            }

            //Tag filter properties
            [System.Serializable]
            public class TagFilter
            {
                [SerializeField, Tooltip("Effect type for tag filter options.")]
                private EffectType effectType = EffectType.Effect;
                public EffectType _effectType
                {
                    get { return effectType; }
                    set { effectType = value; }
                }

                [SerializeField, Tooltip("Tag name used for filter options.")]
                private string tag = string.Empty;
                public string Tag
                {
                    get { return tag; }
                    set { tag = value; }
                }
            }

            //GameObject filter properties
            [System.Serializable]
            public class GameObjectFilter
            {
                [SerializeField, Tooltip("Effect type for Gameobject filter options.")]
                private EffectType effectType = EffectType.Effect;
                public EffectType _effectType
                {
                    get { return effectType; }
                    set { effectType = value; }
                }

                [SerializeField, Tooltip("Gameobject used for filter options.")]
                private GameObject gameObject;
                public GameObject _gameObject
                {
                    get { return gameObject; }
                    set { gameObject = value; }
                }
            }

            //Collider filter properties
            [System.Serializable]
            public class BoundsFilter
            {
                [SerializeField, Tooltip("Effect type for bounds collider filter options.")]
                private EffectType effectType = EffectType.Effect;
                public EffectType _effectType
                {
                    get { return effectType; }
                    set { effectType = value; }
                }

                [SerializeField, Tooltip("Bound collider used for filter options.")]
                private Collider2D collider;
                public Collider2D _collider
                {
                    get { return collider; }
                    set { collider = value; }
                }
            }

            //Physic material filter properties
            [System.Serializable]
            public class PhysicMaterialFilter
            {
                [SerializeField, Tooltip("Effect type for physic material filter options.")]
                private EffectType effectType = EffectType.Effect;
                public EffectType _effectType
                {
                    get { return effectType; }
                    set { effectType = value; }
                }

                [SerializeField, Tooltip("Physic material used for filter options.")]
                private PhysicsMaterial2D physicMaterial;
                public PhysicsMaterial2D _physicMaterial
                {
                    get { return physicMaterial; }
                    set { physicMaterial = value; }
                }
            }

            // Filter Properties Constructor
            public FilterProperties()
            {
                gameObjectFilter = new List<GameObjectFilter>();
                tagFilter = new List<TagFilter>();
                boundsFilter = new List<BoundsFilter>();
                physicMaterialFilter = new List<PhysicMaterialFilter>();
            }

            [SerializeField, Tooltip("Used to filter out gameobjects, has authority over tags, colliders, physicMaterial, and layermasks.")]
            private List<GameObjectFilter> gameObjectFilter;
            public List<GameObjectFilter> _gameObjectFilter
            {
                get { return gameObjectFilter; }
                set { gameObjectFilter = value; }
            }

            [SerializeField, Tooltip("Used to filter out tags, has authority over colliders, physicMaterial, and layermasks.")]
            private List<TagFilter> tagFilter;
            public List<TagFilter> _tagFilter
            {
                get { return tagFilter; }
                set { tagFilter = value; }
            }

            [SerializeField, Tooltip("Used to filter out collider bounds, has authority over physicMaterial, and layermasks.")]
            public List<BoundsFilter> boundsFilter;
            public List<BoundsFilter> _boundsFilter
            {
                get { return boundsFilter; }
                set { boundsFilter = value; }
            }

            [SerializeField, Tooltip("Used to filter out physic material, has authority over layermasks.")]
            public List<PhysicMaterialFilter> physicMaterialFilter;
            public List<PhysicMaterialFilter> _physicMaterialFilter
            {
                get { return physicMaterialFilter; }
                set { physicMaterialFilter = value; }
            }

            [SerializeField, Tooltip("Used for fildering LayerMasks.")]
            private LayerMask layerMaskFilter = -1;
            public LayerMask _layerMaskFilter
            {
                get { return layerMaskFilter; }
                set { layerMaskFilter = value; }
            }

            //ValidateFilters all filter options
            public bool ValidateFilters(GameObject gameobjectFilter, Collider2D coll)
            {
                bool value = true;

                if (_gameObjectFilter.Count > 0)
                {
                    for (int i = 0; i < _gameObjectFilter.Count; i++)
                    {
                        switch (_gameObjectFilter[i]._effectType)
                        {
                            case EffectType.Effect:
                                if (_gameObjectFilter[i]._gameObject == gameobjectFilter)
                                {
                                    return true;
                                }
                                break;
                            case EffectType.DontEffect:
                                if (_gameObjectFilter[i]._gameObject == gameobjectFilter)
                                {
                                    return false;
                                }
                                break;
                        }
                    }
                }

                if (_tagFilter.Count > 0)
                {
                    for (int i = 0; i < _tagFilter.Count; i++)
                    {
                        switch (_tagFilter[i]._effectType)
                        {
                            case EffectType.Effect:
                                if (gameobjectFilter.transform.gameObject.CompareTag(_tagFilter[i].Tag))
                                {
                                    return true;
                                }
                                break;
                            case EffectType.DontEffect:
                                if (gameobjectFilter.transform.gameObject.CompareTag(_tagFilter[i].Tag))
                                {
                                    return false;
                                }
                                break;
                        }
                    }
                }

                if (_boundsFilter.Count > 0)
                {
                    for (int i = 0; i < _boundsFilter.Count; i++)
                    {
                        switch (_boundsFilter[i]._effectType)
                        {
                            case EffectType.Effect:
                                if (_boundsFilter[i]._collider.bounds.Contains(gameobjectFilter.transform.position))
                                {
                                    return true;
                                }
                                break;
                            case EffectType.DontEffect:
                                if (_boundsFilter[i]._collider.bounds.Contains(gameobjectFilter.transform.position))
                                {
                                    return false;
                                }
                                break;
                        }
                    }
                }

                if (_physicMaterialFilter.Count > 0)
                {
                    for (int i = 0; i < _physicMaterialFilter.Count; i++)
                    {
                        switch (_physicMaterialFilter[i]._effectType)
                        {
                            case EffectType.Effect:
                                if (_physicMaterialFilter[i]._physicMaterial == coll.sharedMaterial)
                                {
                                    return true;
                                }
                                break;
                            case EffectType.DontEffect:
                                if (_physicMaterialFilter[i]._physicMaterial == coll.sharedMaterial)
                                {
                                    return false;
                                }
                                break;
                        }
                    }
                }

                if (((1 << gameobjectFilter.layer) & _layerMaskFilter) != 0)
                {
                    value = true;
                }
                else if (((1 << gameobjectFilter.layer) & _layerMaskFilter) == 0)
                {
                    value = false;
                }

                return value;
            }
        }

        [System.Serializable]
        public class FixedJointProperties
        {
            [SerializeField, Tooltip("Check this box to enable the ability for the two connected objects to collide with each other.")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("The degree to which you want to suppress spring oscillation: In the range 0 to 1, the higher the value, the less movement.")]
            private float dampingRatio = 0f;
            public float DampingRatio
            {
                get { return dampingRatio; }
                set { dampingRatio = value; }
            }

            [SerializeField, Tooltip("The frequency at which the spring oscillates while the objects are approaching the separation distance you want (measured in cycles per second): In the range 1 to 1,000,000, the higher the value, the stiffer the spring. Note that if the frequency is set to 0, the spring will be completely stiff.")]
            private float frequency = 0f;
            public float Frequency
            {
                get { return frequency; }
                set { frequency = value; }
            }

            [SerializeField, Tooltip("Specify the force level needed to break and so delete the joint. Infinity means it is unbreakable.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }

            [SerializeField, Tooltip("Specify the torque level needed to break and so delete the joint. Infinity means it is unbreakable.")]
            private float breakTorque = Mathf.Infinity;
            public float BreakTorque
            {
                get { return breakTorque; }
                set { breakTorque = value; }
            }
        }

        [System.Serializable]
        public class SpringJointProperties
        {
            [SerializeField, Tooltip("Can the two connected objects collide with each other? Check the box for yes.")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("Check this box to automatically set the anchor location for the other object this joint connects to. (Check this instead of completing the Connected Anchor fields.)")]
            private bool autoConfigureDistance = true;
            public bool AutoConfigureDistance
            {
                get { return autoConfigureDistance; }
                set { autoConfigureDistance = value; }
            }

            [SerializeField, Tooltip("The distance that the spring should attempt to maintain between the two objects. (Can be set manually.)")]
            private float distance = 0.005f;
            public float Distance
            {
                get { return distance; }
                set { distance = value; }
            }

            [SerializeField, Tooltip("The degree to which you want to suppress spring oscillation: In the range 0 to 1, the higher the value, the less movement.")]
            private float dampingRatio = 0f;
            public float DampingRatio
            {
                get { return dampingRatio; }
                set { dampingRatio = value; }
            }

            [SerializeField, Tooltip("The frequency at which the spring oscillates while the objects are approaching the separation distance you want (measured in cycles per second): In the range 0 to 1,000,000 - the higher the value, the stiffer the spring.")]
            private float frequency = 1f;
            public float Frequency
            {
                get { return frequency; }
                set { frequency = value; }
            }

            [SerializeField, Tooltip("Specify the force level needed to break and so delete the joint. Infinity means it is unbreakable.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }
        }

        [System.Serializable]
        public class ConnectedObjectData
        {
            [SerializeField, Tooltip("GameObject parent transform.")]
            private Transform parent;
            public Transform Parent
            {
                get { return parent; }
                set { parent = value; }
            }

            [SerializeField, Tooltip("Connected GameObject.")]
            private GameObject gameObject;
            public GameObject _gameObject
            {
                get { return gameObject; }
                set { gameObject = value; }
            }

            [SerializeField, Tooltip("Connected GameObject colliders.")]
            private Collider2D[] colliders;
            public Collider2D[] _colliders
            {
                get { return colliders; }
                set { colliders = value; }
            }

            [SerializeField, Tooltip("Connected GameObject FixedJoint.")]
            private FixedJoint2D fixedJoint;
            public FixedJoint2D _fixedJoint
            {
                get { return fixedJoint; }
                set { fixedJoint = value; }
            }

            [SerializeField, Tooltip("Connected GameObject SpringJoint.")]
            private SpringJoint2D springJoint;
            public SpringJoint2D _springJoint
            {
                get { return springJoint; }
                set { springJoint = value; }
            }
        }

        #endregion

        #region enums

        public enum JointType
        {
            None = 0,
            Fixed = 1,
            Spring = 2,
        }

        #endregion

        #region Properties

        //Events
        static public event Action<SSS2D> OnStick2D;
        static public event Action<SSS2D> OnUnStick2D;
        static public event Action<SSS2D> OnStickBreak2D;

        [SerializeField, Tooltip("Enable/Disables Sticky Stick Stuck, if disabled will cleanup all join mess.")]
        private bool enable = true;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        [SerializeField, Tooltip("Pauses the Sticky Stick Stuck.")]
        private bool paused = false;
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }
        [SerializeField, Tooltip("How the objects should parent when stuck together.")]
        private SSS.ParentType parentType;
        public SSS.ParentType _parentType
        {
            get { return parentType; }
            set { parentType = value; }
        }
        [SerializeField, Tooltip("The joint type when stuck together.")]
        private JointType jointType = JointType.Fixed;
        public JointType _jointType
        {
            get { return jointType; }
            set { jointType = value; }
        }
        [SerializeField, Tooltip("Fixed Joints restricts an object’s movement to be dependent upon another object.")]
        private FixedJointProperties fixedJointProperties;
        public FixedJointProperties _fixedJointProperties
        {
            get { return fixedJointProperties; }
            set { fixedJointProperties = value; }
        }
        [SerializeField, Tooltip("The Spring Joint joins two Rigidbodies together but allows the distance between them to change as though they were connected by a spring.")]
        private SpringJointProperties springJointProperties;
        public SpringJointProperties _springJointProperties
        {
            get { return springJointProperties; }
            set { springJointProperties = value; }
        }
        [SerializeField, Tooltip("Contains all UnityEvent for StickyStickStuck.")]
        private SSS.EventProperties eventProperties;
        public SSS.EventProperties _eventProperties
        {
            get { return eventProperties; }
            set { eventProperties = value; }
        }
        [SerializeField, Tooltip("Filter stick options.")]
        private FilterProperties filterProperties;
        public FilterProperties _filterProperties
        {
            get { return filterProperties; }
            set { filterProperties = value; }
        }
        [SerializeField, Tooltip("Sticks when colliding in trigger area.")]
        private bool stickInTriggerArea = false;
        public bool StickInTriggerArea
        {
            get { return stickInTriggerArea; }
            set { stickInTriggerArea = value; }
        }
        [SerializeField, Tooltip("Sticks to GameObjects that dont have a rigidbody.")]
        private bool stickToNonRigidbody;
        public bool StickToNonRigidbody
        {
            get { return stickToNonRigidbody; }
            set { stickToNonRigidbody = value; }
        }
        [SerializeField, Tooltip("Sticks to GameObjects that have the StickyStickStuck component.")]
        private bool stickOnSticky = false;
        public bool StickOnSticky
        {
            get { return stickOnSticky; }
            set { stickOnSticky = value; }
        }
        [SerializeField, Tooltip("When sticks, this sets the GameObjects transform offset.")]
        private Vector3 stickOffset = Vector3.zero;
        public Vector3 StickOffset
        {
            get { return stickOffset; }
            set { stickOffset = value; }
        }
        [SerializeField, Tooltip("The target impact velocity in order for the object to stick.")]
        private float stickImpactVelocity;
        public float StickImpactVelocity
        {
            get { return stickImpactVelocity; }
            set { stickImpactVelocity = value; }
        }
        [SerializeField, Tooltip("Ignores all colliders that it sticks too.")]
        private bool ignoreColliders = false;
        public bool IgnoreColliders
        {
            get { return ignoreColliders; }
            set { ignoreColliders = value; }
        }
        [SerializeField, Tooltip("Recursively adds the StickyStickStuck component to whatever it touchs.")]
        private bool stickRecursively = false;
        public bool StickRecursively
        {
            get { return stickRecursively; }
            set { stickRecursively = value; }
        }
        [SerializeField, Tooltip("Max amount of objects that can be effected.")]
        private int maxEffectedItems = 1;
        public int MaxEffectedItems
        {
            get { return maxEffectedItems; }
            set { maxEffectedItems = value; }
        }
        [SerializeField, Tooltip("Contains all debug options for StickyStickStuck.")]
        private SSS.DebugProperties debugProperties;
        public SSS.DebugProperties _debugProperties
        {
            get { return debugProperties; }
            set { debugProperties = value; }
        }

        //Used for telling which StickYStickStuck object is the parent
        [SerializeField, Tooltip("Flag for if the StickyStickStuck is the infected parent.")]
        private bool isInfectParent = true;
        public bool IsInfectParent
        {
            get { return isInfectParent; }
            set { isInfectParent = value; }
        }

        [SerializeField, Tooltip("Collected StickyStickStuck data.")]
        private List<ConnectedObjectData> connectedObjectDataLists;
        public List<ConnectedObjectData> ConnectedObjectDataLists
        {
            get { return connectedObjectDataLists; }
            set { connectedObjectDataLists = value; }
        }

        private Transform startParent;
        private Rigidbody2D mainRigidbody;
        private Collider2D[] colliders;
        private float velocity = 0f;
        private bool inTriggerArea = false;

        public SSS2D()
        {
            _fixedJointProperties = new FixedJointProperties();
            _springJointProperties = new SpringJointProperties();

            _eventProperties = new SSS.EventProperties();
            _filterProperties = new FilterProperties();
            _debugProperties = new SSS.DebugProperties();

            ConnectedObjectDataLists = new List<ConnectedObjectData>();
        }

        public void CopyStickyStickStuck(SSS2D copy)
        {
            copy.Enable = this.Enable;
            copy.Paused = this.Paused;
            copy._parentType = this._parentType;
            copy._jointType = this._jointType;

            copy._fixedJointProperties = this._fixedJointProperties;
            copy._springJointProperties = this._springJointProperties;

            copy.StickInTriggerArea = this.StickInTriggerArea;
            copy.StickToNonRigidbody = this.StickToNonRigidbody;
            copy.StickOnSticky = this.StickOnSticky;
            copy.StickOffset = this.StickOffset;
            copy.StickImpactVelocity = this.StickImpactVelocity;
            copy.IgnoreColliders = this.IgnoreColliders;
            copy.StickRecursively = this.StickRecursively;
            copy.MaxEffectedItems = this.MaxEffectedItems;

            copy._eventProperties = this._eventProperties;
            copy._filterProperties = this._filterProperties;
            copy._debugProperties = this._debugProperties;
        }

        #endregion

        #region Unity Functions

#if (UNITY_EDITOR)

        //Creates Gizmos Icons for the StickyStickStuck
        void OnDrawGizmos()
        {
            string icon = "StickyStickStuck Icons/";
            icon = SetupIcons(icon);

            if (ConnectedObjectDataLists != null)
            {
                foreach (var item in ConnectedObjectDataLists)
                {
                    Vector2 anchor = Vector3.zero;
                    Vector2 connectedAnchor = Vector3.zero;
                    if (item._fixedJoint != null)
                    {
                        Gizmos.color = Color.red;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._fixedJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._fixedJoint.connectedAnchor);
                    }
                    if (item._springJoint != null)
                    {
                        Gizmos.color = Color.cyan;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._springJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._springJoint.connectedAnchor);
                    }

                    Gizmos.DrawIcon(anchor, icon, true);

                    if (Vector2.Distance(anchor, connectedAnchor) > .1f && item._gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        Gizmos.DrawLine(anchor, connectedAnchor);
                        Gizmos.DrawIcon(connectedAnchor, icon, true);
                    }
                }
            }
        }

        string SetupIcons(string icon)
        {
            string cgfDir = string.Format("{0}/ResurgamStudios/StickyStickStuck Package/Gizmos/StickyStickStuck Icons/", Application.dataPath);
            string dir = string.Format("{0}/Gizmos/StickyStickStuck Icons/", Application.dataPath);

            if (!Directory.Exists(dir))
            {
                if (Directory.Exists(cgfDir))
                {
                    CopyIcons(cgfDir, dir);

                    AssetDatabase.Refresh();
                }
            }

            icon = icon + "sss_icon";

            switch (_jointType)
            {
                case JointType.Fixed:
                    icon = icon + "_f";
                    break;
                case JointType.Spring:
                    icon = icon + "_s";
                    break;
                case JointType.None:
                    icon = icon + "_n";
                    break;
            }

            icon = icon + ".png";

            return icon;
        }

        //Copys all cgf icons
        void CopyIcons(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir).Where(s => s.EndsWith(".png")))
            {
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));
            }
        }
#endif

        void OnEnable()
        {
            CleanUp();
        }

        void Awake()
        {
            if (this.gameObject.GetComponent<Rigidbody2D>() != null)
                mainRigidbody = this.GetComponent<Rigidbody2D>();

            colliders = this.GetComponentsInChildren<Collider2D>();

            if (IsInfectParent)
                startParent = this.transform;
        }

        void FixedUpdate()
        {
            //Cleans up the mess when enabled equals false
            if (!Enable)
            {
                CleanUp();
            }

            //Checks to see if any of the stuck objects are disabled, and if so cleanup
            if (CheckForNonEnabledObjects())
            {
                CleanUp();
            }

            if (debugProperties.OutputVelocity)
                Debug.Log(string.Format("{0}: {1}", this.name, mainRigidbody.velocity.magnitude));
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = true;
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = false;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            velocity = collision.relativeVelocity.magnitude;

            //Used to add the joint when collision happens
            if (Enable && !Paused)
            {
                AddStickyJoint(collision);
            }

            if (debugProperties.OutputHitVelocity)
                Debug.Log(string.Format("'{0}' Hit Velocity: {1}", this.name, velocity));
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            //Used to add the joint when collision happens
            if (Enable && !Paused)
            {
                AddStickyJoint(collision);
            }
        }

        //For when the joint breaks
        void OnJointBreak2D(Joint2D joint2D)
        {
            _eventProperties.OnStickBreak.Invoke();

            if (OnStickBreak2D != null)
                OnStickBreak2D(this);

            CleanUp();
        }

        #endregion

        #region Physics / Collision

        private void AddStickyJoint(Collision2D whatIHit)
        {
            if (CheckIfOkToStick(whatIHit))
            {
                ConnectedObjectData connectedObjectData = new ConnectedObjectData();
                if (_parentType == SSS.ParentType.ParentOnStick)
                    connectedObjectData.Parent = whatIHit.gameObject.transform.parent;
                connectedObjectData._gameObject = whatIHit.gameObject;
                connectedObjectData._colliders = whatIHit.gameObject.GetComponentsInChildren<Collider2D>();
                Rigidbody2D whatIhitRigidbody = whatIHit.gameObject.GetComponent<Rigidbody2D>();

                Vector2 localAnchor = this.gameObject.transform.worldToLocalMatrix.MultiplyPoint(whatIHit.contacts[0].point);
                Vector2 connectedAncor = whatIHit.gameObject.transform.worldToLocalMatrix.MultiplyPoint(whatIHit.contacts[0].point);
                //Vector2 localAxis = -whatIHit.contacts[0].normal;

                Transform newStartParent = connectedObjectData._gameObject.transform.parent;

                if (IgnoreColliders)
                {
                    foreach (var myCollider in colliders)
                    {
                        foreach (var whatIHitCollider in connectedObjectData._colliders)
                        {
                            Physics2D.IgnoreCollision(myCollider, whatIHitCollider, true);
                        }
                    }
                }

                this.transform.Translate(StickOffset);

                _eventProperties.OnStickHandler.Invoke();

                if (OnStick2D != null)
                    OnStick2D(this);

				connectedObjectData._gameObject.SendMessage("OnStick2D", this, SendMessageOptions.DontRequireReceiver);

                switch (_jointType)
                {
                    case JointType.Fixed:

                        FixedJoint2D fixedJoint = new FixedJoint2D();
                        fixedJoint = this.gameObject.AddComponent<FixedJoint2D>();
                        fixedJoint.anchor = localAnchor;
                        fixedJoint.connectedAnchor = connectedAncor;
                        //fixedJoint.axis = localAxis;

                        fixedJoint.dampingRatio = this._fixedJointProperties.DampingRatio;
                        fixedJoint.frequency = this._fixedJointProperties.Frequency;
                        fixedJoint.enableCollision = this._fixedJointProperties.EnableCollision;
                        if(this._fixedJointProperties.BreakForce != Mathf.Infinity)
                            fixedJoint.breakForce = this._fixedJointProperties.BreakForce;
                        if (this._fixedJointProperties.BreakTorque != Mathf.Infinity)
                            fixedJoint.breakTorque = this._fixedJointProperties.BreakTorque;

                        fixedJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._fixedJoint = fixedJoint;

                        break;

                    case JointType.Spring:

                        SpringJoint2D springJoint = new SpringJoint2D();
                        springJoint = this.gameObject.AddComponent<SpringJoint2D>();
                        springJoint.anchor = localAnchor;
                        springJoint.connectedAnchor = connectedAncor;
                        //fixedJoint.axis = localAxis;

                        springJoint.autoConfigureDistance = this._springJointProperties.AutoConfigureDistance;
                        springJoint.distance = this._springJointProperties.Distance;
                        springJoint.dampingRatio = this._springJointProperties.DampingRatio;
                        springJoint.frequency = this._springJointProperties.Frequency;
                        springJoint.enableCollision = this._springJointProperties.EnableCollision;
                        if (this._springJointProperties.BreakForce != Mathf.Infinity)
                            springJoint.breakForce = this._springJointProperties.BreakForce;

                        springJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._springJoint = springJoint;

                        break;

                    case JointType.None:

                        break;
                }

                //Used for when Recursive Infection is checked
                if (StickRecursively)
                {
                    if (connectedObjectData._gameObject.GetComponent<SSS2D>() == null)
                    {
                        SSS2D stickyStickStuck = connectedObjectData._gameObject.AddComponent<SSS2D>();

                        //Copys all properties to the new StickyStickStuck
                        CopyStickyStickStuck(stickyStickStuck);

                        //Sets the children isInfectParent to false
                        stickyStickStuck.IsInfectParent = false;

                        //Sets the start parent to the new start parent
                        stickyStickStuck.startParent = newStartParent;
                    }
                }

                //Sets up the parent
                switch (_parentType)
                {
                    case SSS.ParentType.None:
                        break;
                    case SSS.ParentType.ChildOnStick:
                        this.transform.SetParent(connectedObjectData._gameObject.transform, true);
                        break;
                    case SSS.ParentType.ParentOnStick:
                        connectedObjectData._gameObject.transform.SetParent(this.transform, true);
                        break;
                }

                ConnectedObjectDataLists.Add(connectedObjectData);
            }
        }

        //Check to see if the joint exists for the given GameObject
        private bool CheckIfJointExistsForGameObject(GameObject gameObject)
        {
            foreach (var item in ConnectedObjectDataLists)
            {
                if (item._gameObject == gameObject)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Functions

        //Checks to see if any of the stuck objects are disabled
        bool CheckForNonEnabledObjects()
        {
            for (int i = 0; i < connectedObjectDataLists.Count; i++)
            {
                if (!connectedObjectDataLists[i]._gameObject.activeInHierarchy)
                {
                    return true;
                }
            }

            return false;
        }

        //This is where it check to see if its ok to add a sticky joint
        private bool CheckIfOkToStick(Collision2D whatIHit)
        {
            //if whatIhit equals null then fail
            if (whatIHit == null)
            {
                return false;
            }

            //if whatIhit collider equals the parents collider then fail
            if (whatIHit.collider == this.gameObject.GetComponent<Collider2D>())
            {
                return false;
            }

            //If stickNonRigidbodys is false
            if (!StickToNonRigidbody)
            {
                //If the whatIhit rigidbody equals null then fail
                if (whatIHit.rigidbody == null)
                {
                    return false;
                }
            }

            //If the connected amount of objects is greater than the max sticked size then fail
            if (ConnectedObjectDataLists.Count >= MaxEffectedItems)
            {
                return false;
            }

            //Fail if joint exists
            if (CheckIfJointExistsForGameObject(whatIHit.gameObject))
            {
                return false;
            }

            //If affect infected is checked
            if (!StickOnSticky)
            {
                //If StickyStickStuck exists then fail
                if (whatIHit.gameObject.GetComponent<SSS2D>() != null)
                {
                    return false;
                }
            }

            //If trigger area is active
            if (StickInTriggerArea)
            {
                if (!inTriggerArea)
                    return false;
            }

            if (mainRigidbody == null)
            {
                return false;
            }

            //Checks the stick impact velocity
            if (velocity < StickImpactVelocity)
            {

                return false;
            }

            var whatIHitGameObject = whatIHit.contacts[0].collider.gameObject;

            if (whatIHitGameObject == null)
                return false;
            
            //Validated by the filter options
            if (!_filterProperties.ValidateFilters(whatIHitGameObject, whatIHit.contacts[0].collider))
                return false;

            return true;
        }

        #endregion

        #region CleanUp

        //Cleans up the mess
        public void CleanUp()
        {
            inTriggerArea = false;

            //Cleans up this objects real parent
            if (startParent == null)
            {
                this.transform.SetParent(null, true);
            }
            else if (startParent.gameObject != null)
            {
                if (startParent.gameObject.activeInHierarchy)
                    this.transform.SetParent(startParent, true);
            }

            //Checks to see of any of the connected objects exist
            if (ConnectedObjectDataLists.Count > 0)
            {
                //Cleans up joins
                CleanupJoints();

                //Cleans up all objects intected by the StickyStickStuck scripted
                InfectCleanUp();
            }

            //Cleans the connected object data list
            ConnectedObjectDataLists.Clear();

            //If not the parent destroy the StickyStickStuck object
            if (!IsInfectParent)
            {
                Destroy(this.GetComponent<SSS2D>());
            }
        }

        //Cleans up all the joints
        private void CleanupJoints()
        {
            foreach (var item in ConnectedObjectDataLists)
            {
                if (item._fixedJoint != null)
                {
                    Destroy(item._fixedJoint);
                }
                if (item._springJoint != null)
                {
                    Destroy(item._springJoint);
                }
            }
        }

        //Cleans up the infected
        private void InfectCleanUp()
        {
            foreach (var item in ConnectedObjectDataLists)
            {
                if (IgnoreColliders)
                {
                    foreach (var myCollider in colliders)
                    {
                        foreach (var whatIHitCollider in item._colliders)
                        {
                            Physics2D.IgnoreCollision(whatIHitCollider, myCollider, false);
                        }
                    }
                }

                if (item._gameObject != null)
                {
                    _eventProperties.OnUnStickHandler.Invoke();

                    if (OnUnStick2D != null)
                        OnUnStick2D(this);

                    item._gameObject.SendMessage("OnUnStick2D", this, SendMessageOptions.DontRequireReceiver);
					
                    SSS2D stickyStickStuck = item._gameObject.GetComponent<SSS2D>();

                    if (stickyStickStuck != null)
                    {
                        if (stickyStickStuck.IsInfectParent == false)
                        {
                            stickyStickStuck.Enable = false;
                        }
                    }

                    if (_parentType == SSS.ParentType.ParentOnStick)
                        item._gameObject.transform.SetParent(item.Parent, true);
                }
            }
        }

        #endregion
    }
}
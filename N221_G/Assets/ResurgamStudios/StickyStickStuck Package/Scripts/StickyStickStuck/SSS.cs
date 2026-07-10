/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Core logic for Sticky Stick Stuck.
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
    [RequireComponent(typeof(Rigidbody)), AddComponentMenu("Physics/Sticky Stick Stuck", -1)]
    public class SSS : MonoBehaviour
    {
        #region Classes

        [System.Serializable]
        public class HingeJointProperties
        {
            [System.Serializable]
            public class JointSpring
            {
                [SerializeField, Tooltip("The spring forces used to reach the target position.")]
                private float spring;
                public float Spring
                {
                    get { return spring; }
                    set { spring = value; }
                }

                [SerializeField, Tooltip("The damper force uses to dampen the spring.")]
                private float damper;
                public float Damper
                {
                    get { return damper; }
                    set { damper = value; }
                }

                [SerializeField, Tooltip("The target position the joint attempts to reach.")]
                private float targetPosition;
                public float TargetPosition
                {
                    get { return targetPosition; }
                    set { targetPosition = value; }
                }
            }

            [System.Serializable]
            public class JointMotor
            {
                [SerializeField, Tooltip("The motor will apply a force up to force to achieve targetVelocity.")]
                private float targetVelocity;
                public float TargetVelocity
                {
                    get { return targetVelocity; }
                    set { targetVelocity = value; }
                }

                [SerializeField, Tooltip("The motor will apply a force.")]
                private float force;
                public float Force
                {
                    get { return force; }
                    set { force = value; }
                }

                [SerializeField, Tooltip("If freeSpin is enabled the motor will only accelerate but never slow down.")]
                private bool freeSpin;
                public bool FreeSpin
                {
                    get { return freeSpin; }
                    set { freeSpin = value; }
                }
            }

            [System.Serializable]
            public class JointLimits
            {
                [SerializeField, Tooltip("The lower angular limit (in degrees) of the joint.")]
                private float min;
                public float Min
                {
                    get { return min; }
                    set { min = value; }
                }

                [SerializeField, Tooltip("The upper angular limit (in degrees) of the joint.")]
                private float max;
                public float Max
                {
                    get { return max; }
                    set { max = value; }
                }

                [SerializeField, Tooltip("Determines the size of the bounce when the joint hits it's limit. Also known as restitution.")]
                private float bounciness;
                public float Bounciness
                {
                    get { return bounciness; }
                    set { bounciness = value; }
                }

                [SerializeField, Tooltip("The minimum impact velocity which will cause the joint to bounce.")]
                private float bounceMinVelocity = .2f;
                public float BounceMinVelocity
                {
                    get { return bounceMinVelocity; }
                    set { bounceMinVelocity = value; }
                }

                [SerializeField, Tooltip("Distance inside the limit value at which the limit will be considered to be active by the solver.")]
                private float contactDistance;
                public float ContactDistance
                {
                    get { return contactDistance; }
                    set { contactDistance = value; }
                }
            }

            [SerializeField, Tooltip("Spring makes the Rigidbody reach for a specific angle compared to its connected body.")]
            private bool useSpring;
            public bool UseSpring
            {
                get { return useSpring; }
                set { useSpring = value; }
            }

            [SerializeField, Tooltip("The force the object asserts to move into the position.")]
            private JointSpring spring;
            public JointSpring _spring
            {
                get { return spring; }
                set { spring = value; }
            }

            [SerializeField, Tooltip("The motor makes the object spin around.")]
            private bool useMotor;
            public bool UseMotor
            {
                get { return useMotor; }
                set { useMotor = value; }
            }

            [SerializeField, Tooltip("Properties of the Motor that are used if Use Motor is enabled.")]
            private JointMotor motor;
            public JointMotor _motor
            {
                get { return motor; }
                set { motor = value; }
            }

            [SerializeField, Tooltip("If enabled, the angle of the hinge will be restricted within the Min & Max values.")]
            private bool useLimits;
            public bool UseLimits
            {
                get { return useLimits; }
                set { useLimits = value; }
            }

            [SerializeField, Tooltip("Properties of the Limits that are used if Use Limits is enabled.")]
            private JointLimits limits;
            public JointLimits _limits
            {
                get { return limits; }
                set { limits = value; }
            }

            [SerializeField, Tooltip("The force that needs to be applied for this joint to break.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }

            [SerializeField, Tooltip("The torque that needs to be applied for this joint to break.")]
            private float breakTorque = Mathf.Infinity;
            public float BreakTorque
            {
                get { return breakTorque; }
                set { breakTorque = value; }
            }

            [SerializeField, Tooltip("When checked, this enables collisions between bodies connected with a joint.")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("Disabling preprocessing helps to stabilize impossible-to-fulfil configurations.")]
            private bool enablePreprocessing = true;
            public bool EnablePreprocessing
            {
                get { return enablePreprocessing; }
                set { enablePreprocessing = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the body prior to solving the constraints.")]
            private float massScale = 1f;
            public float MassScale
            {
                get { return massScale; }
                set { massScale = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the connected body prior to solving the constraints.")]
            private float connectedMassScale = 1f;
            public float ConnectedMassScale
            {
                get { return connectedMassScale; }
                set { connectedMassScale = value; }
            }
        }

        [System.Serializable]
        public class FixedJointProperties
        {
            [SerializeField, Tooltip("The force that needs to be applied for this joint to break.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }

            [SerializeField, Tooltip("The torque that needs to be applied for this joint to break.")]
            private float breakTorque = Mathf.Infinity;
            public float BreakTorque
            {
                get { return breakTorque; }
                set { breakTorque = value; }
            }

            [SerializeField, Tooltip("When checked, this enables collisions between bodies connected with a joint.")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("Disabling preprocessing helps to stabilize impossible-to-fulfil configurations.")]
            private bool enablePreprocessing = true;
            public bool EnablePreprocessing
            {
                get { return enablePreprocessing; }
                set { enablePreprocessing = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the body prior to solving the constraints.")]
            private float massScale = 1f;
            public float MassScale
            {
                get { return massScale; }
                set { massScale = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the connected body prior to solving the constraints.")]
            private float connectedMassScale = 1f;
            public float ConnectedMassScale
            {
                get { return connectedMassScale; }
                set { connectedMassScale = value; }
            }
        }

        [System.Serializable]
        public class SpringJointProperties
        {
            [SerializeField, Tooltip("Strength of the spring.")]
            private float spring = 10f;
            public float Spring
            {
                get { return spring; }
                set { spring = value; }
            }

            [SerializeField, Tooltip("Amount that the spring is reduced when active.")]
            private float damper = .2f;
            public float Damper
            {
                get { return damper; }
                set { damper = value; }
            }

            [SerializeField, Tooltip("Lower limit of the distance range over which the spring will not apply any force.")]
            private float minDistance = 0f;
            public float MinDistance
            {
                get { return minDistance; }
                set { minDistance = value; }
            }

            [SerializeField, Tooltip("Upper limit of the distance range over which the spring will not apply any force.")]
            private float maxDistance = 0f;
            public float MaxDistance
            {
                get { return maxDistance; }
                set { maxDistance = value; }
            }

            [SerializeField, Tooltip("Changes error tolerance. Allows the spring to have a different rest length.")]
            private float tolerance = 0.025f;
            public float Tolerance
            {
                get { return tolerance; }
                set { tolerance = value; }
            }

            [SerializeField, Tooltip("The force that needs to be applied for this joint to break.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }

            [SerializeField, Tooltip("The torque that needs to be applied for this joint to break.")]
            private float breakTorque = Mathf.Infinity;
            public float BreakTorque
            {
                get { return breakTorque; }
                set { breakTorque = value; }
            }

            [SerializeField, Tooltip("Should the two connected objects register collisions with each other?")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("Disabling preprocessing helps to stabilize impossible-to-fulfil configurations.")]
            private bool enablePreprocessing = true;

            public bool EnablePreprocessing
            {
                get { return enablePreprocessing; }
                set { enablePreprocessing = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the body prior to solving the constraints.")]
            private float massScale = 1f;
            public float MassScale
            {
                get { return massScale; }
                set { massScale = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the connected body prior to solving the constraints.")]
            private float connectedMassScale = 1f;
            public float ConnectedMassScale
            {
                get { return connectedMassScale; }
                set { connectedMassScale = value; }
            }
        }

        [System.Serializable]
        public class CharacterJointProperties
        {
            [System.Serializable]
            public class TwistLimitSpring
            {
                [SerializeField, Tooltip("The spring force used to keep the two objects together.")]
                private float spring;
                public float Spring
                {
                    get { return spring; }
                    set { spring = value; }
                }

                [SerializeField, Tooltip("The damper force used to dampen the spring force.")]
                private float damper;
                public float Damper
                {
                    get { return damper; }
                    set { damper = value; }
                }
            }

            [System.Serializable]
            public class LowTwistLimit
            {
                [SerializeField, Tooltip("The limit position/angle of the joint (in degrees).")]
                private float limit = -20f;
                public float Limit
                {
                    get { return limit; }
                    set { limit = value; }
                }

                [SerializeField, Tooltip("When the joint hits the limit, it can be made to bounce off it.")]
                private float bounciness;
                public float Bounciness
                {
                    get { return bounciness; }
                    set { bounciness = value; }
                }

                [SerializeField, Tooltip("Determines how far ahead in space the solver can 'see' the joint limit.")]
                private float contactDistance;
                public float ContactDistance
                {
                    get { return contactDistance; }
                    set { contactDistance = value; }
                }
            }

            [System.Serializable]
            public class HighTwistLimit
            {
                [SerializeField, Tooltip("The limit position/angle of the joint (in degrees).")]
                private float limit = 70f;
                public float Limit
                {
                    get { return limit; }
                    set { limit = value; }
                }

                [SerializeField, Tooltip("When the joint hits the limit, it can be made to bounce off it.")]
                private float bounciness;
                public float Bounciness
                {
                    get { return bounciness; }
                    set { bounciness = value; }
                }

                [SerializeField, Tooltip("Determines how far ahead in space the solver can 'see' the joint limit.")]
                private float contactDistance;
                public float ContactDistance
                {
                    get { return contactDistance; }
                    set { contactDistance = value; }
                }
            }

            [System.Serializable]
            public class SwingLimitSpring
            {
                [SerializeField, Tooltip("The stiffness of the spring limit. When stiffness is zero the limit is hard, otherwise soft.")]
                private float spring = 0;
                public float Spring
                {
                    get { return spring; }
                    set { spring = value; }
                }

                [SerializeField, Tooltip("The damping of the spring limit. In effect when the stiffness of the sprint limit is not zero.")]
                private float damper = 0;
                public float Damper
                {
                    get { return damper; }
                    set { damper = value; }
                }
            }

            [System.Serializable]
            public class Swing1Limit
            {
                [SerializeField, Tooltip("The limit position/angle of the joint (in degrees).")]
                private float limit = 40f;
                public float Limit
                {
                    get { return limit; }
                    set { limit = value; }
                }

                [SerializeField, Tooltip("When the joint hits the limit, it can be made to bounce off it.")]
                private float bounciness;
                public float Bounciness
                {
                    get { return bounciness; }
                    set { bounciness = value; }
                }

                [SerializeField, Tooltip("Determines how far ahead in space the solver can 'see' the joint limit.")]
                private float contactDistance;
                public float ContactDistance
                {
                    get { return contactDistance; }
                    set { contactDistance = value; }
                }
            }

            [System.Serializable]
            public class Swing2Limit
            {
                [SerializeField, Tooltip("The limit position/angle of the joint (in degrees).")]
                private float limit = 40f;
                public float Limit
                {
                    get { return limit; }
                    set { limit = value; }
                }

                [SerializeField, Tooltip("When the joint hits the limit, it can be made to bounce off it.")]
                private float bounciness;
                public float Bounciness
                {
                    get { return bounciness; }
                    set { bounciness = value; }
                }

                [SerializeField, Tooltip("Determines how far ahead in space the solver can 'see' the joint limit.")]
                private float contactDistance;
                public float ContactDistance
                {
                    get { return contactDistance; }
                    set { contactDistance = value; }
                }
            }

            [SerializeField, Tooltip("The configuration of the spring attached to the twist limits of the joint.")]
            private TwistLimitSpring twistLimitSprin;
            public TwistLimitSpring _twistLimitSprin
            {
                get { return twistLimitSprin; }
                set { twistLimitSprin = value; }
            }

            [SerializeField, Tooltip("The lower limit around the primary axis of the character joint.")]
            private LowTwistLimit lowTwistLimit;
            public LowTwistLimit _lowTwistLimit
            {
                get { return lowTwistLimit; }
                set { lowTwistLimit = value; }
            }

            [SerializeField, Tooltip("The upper limit around the primary axis of the character joint.")]
            private HighTwistLimit highTwistLimit;
            public HighTwistLimit _highTwistLimit
            {
                get { return highTwistLimit; }
                set { highTwistLimit = value; }
            }

            [SerializeField, Tooltip("The configuration of the spring attached to the swing limits of the joint.")]
            private SwingLimitSpring swingLimitSpring;
            public SwingLimitSpring _swingLimitSpring
            {
                get { return swingLimitSpring; }
                set { swingLimitSpring = value; }
            }

            [SerializeField, Tooltip("The angular limit of rotation (in degrees) around the primary axis of the character joint.")]
            private Swing1Limit swing1Limit;
            public Swing1Limit _swing1Limit
            {
                get { return swing1Limit; }
                set { swing1Limit = value; }
            }

            [SerializeField, Tooltip("The angular limit of rotation (in degrees) around the primary axis of the character joint.")]
            private Swing2Limit swing2Limit;
            public Swing2Limit _swing2Limit
            {
                get { return swing2Limit; }
                set { swing2Limit = value; }
            }

            [SerializeField, Tooltip("Brings violated constraints back into alignment even when the solver fails.")]
            private bool enableProjection = false;
            public bool EnableProjection
            {
                get { return enableProjection; }
                set { enableProjection = value; }
            }

            [SerializeField, Tooltip("Set the linear tolerance threshold for projection.")]
            private float projectionDistance = .1f;
            public float ProjectionDistance
            {
                get { return projectionDistance; }
                set { projectionDistance = value; }
            }

            [SerializeField, Tooltip("Set the angular tolerance threshold (in degrees) for projection.")]
            private float projectionAngle = 180f;
            public float ProjectionAngle
            {
                get { return projectionAngle; }
                set { projectionAngle = value; }
            }

            [SerializeField, Tooltip("The force that needs to be applied for this joint to break.")]
            private float breakForce = Mathf.Infinity;
            public float BreakForce
            {
                get { return breakForce; }
                set { breakForce = value; }
            }

            [SerializeField, Tooltip("The torque that needs to be applied for this joint to break.")]
            private float breakTorque = Mathf.Infinity;
            public float BreakTorque
            {
                get { return breakTorque; }
                set { breakTorque = value; }
            }

            [SerializeField, Tooltip("Enable collision between bodies connected with the joint.")]
            private bool enableCollision = false;
            public bool EnableCollision
            {
                get { return enableCollision; }
                set { enableCollision = value; }
            }

            [SerializeField, Tooltip("Toggle preprocessing for this joint.")]
            private bool enablePreprocessing = true;
            public bool EnablePreprocessing
            {
                get { return enablePreprocessing; }
                set { enablePreprocessing = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the body prior to solving the constraints.")]
            private float massScale = 1f;
            public float MassScale
            {
                get { return massScale; }
                set { massScale = value; }
            }

            [SerializeField, Tooltip("The scale to apply to the inverse mass and inertia tensor of the connected body prior to solving the constraints.")]
            private float connectedMassScale = 1f;
            public float ConnectedMassScale
            {
                get { return connectedMassScale; }
                set { connectedMassScale = value; }
            }
        }

        [System.Serializable]
        public class EventProperties
        {
            [SerializeField, Tooltip("Event for when on stick.")]
            private UnityEvent onStickHandler;
            public UnityEvent OnStickHandler
            {
                get { return onStickHandler; }
                set { onStickHandler = value; }
            }

            [SerializeField, Tooltip("Event for when on stick.")]
            private UnityEvent onUnStickHandler;
            public UnityEvent OnUnStickHandler
            {
                get { return onUnStickHandler; }
                set { onUnStickHandler = value; }
            }

            [SerializeField, Tooltip("Event for when on break.")]
            private UnityEvent onStickBreak;
            public UnityEvent OnStickBreak
            {
                get { return onStickBreak; }
                set { onStickBreak = value; }
            }
        }

        //GameObject filtering options
        [System.Serializable]
        public class GameobjectFilter
        {
            //GameObject filter options
            public enum GameObjectFilterOptions
            {
                Disabled,
                OnlyEffectListedGameobjects,
                DontEffectListedGameobjects,
            }

            [SerializeField, Tooltip("GameObject filter options.")]
            private GameObjectFilterOptions gameObjectFilterOptions = GameObjectFilterOptions.Disabled;
            public GameObjectFilterOptions _gameobjectFilterOptions
            {
                get { return gameObjectFilterOptions; }
                set { gameObjectFilterOptions = value; }
            }

            [SerializeField, Tooltip("Listed GameObject used for the filter.")]
            private List<GameObject> gameobjectList;
            public List<GameObject> GameobjectList
            {
                get { return gameobjectList; }
                set { gameobjectList = value; }
            }
        }

        //Tag filtering options
        [System.Serializable]
        public class TagFilter
        {
            //Tag filter options
            public enum TagFilterOptions
            {
                Disabled,
                OnlyEffectListedTags,
                DontEffectListedTags,
            }

            [SerializeField, Tooltip("Tag filter options.")]
            private TagFilterOptions tagFilterOptions = TagFilterOptions.Disabled;
            public TagFilterOptions _tagFilterOptions
            {
                get { return tagFilterOptions; }
                set { tagFilterOptions = value; }
            }

            [SerializeField, Tooltip("Listed tags used for the filter.")]
            private List<string> tagsList;
            public List<string> TagsList
            {
                get { return tagsList; }
                set { tagsList = value; }
            }
        }

        //Layer filtering options
        [System.Serializable]
        public class LayerFilter
        {
            //Tag filter options
            public enum LayerFilterOptions
            {
                Disabled,
                OnlyEffectListedLayers,
                DontEffectListedLayers,
            }

            [SerializeField, Tooltip("Layer filter options.")]
            private LayerFilterOptions layerFilterOptions = LayerFilterOptions.Disabled;
            public LayerFilterOptions _layerFilterOptions
            {
                get { return layerFilterOptions; }
                set { layerFilterOptions = value; }
            }

            [SerializeField, Tooltip("Listed layers used for the filter.")]
            private List<string> layerList;
            public List<string> LayerList
            {
                get { return layerList; }
                set { layerList = value; }
            }
        }

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
                private Collider collider;
                public Collider _collider
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
                private PhysicMaterial physicMaterial;
                public PhysicMaterial _physicMaterial
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
            public bool ValidateFilters(GameObject gameobjectFilter, Collider coll)
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
        public class DebugProperties
        {
            [SerializeField, Tooltip("Output velocity.")]
            private bool outputVelocity;
            public bool OutputVelocity
            {
                get { return outputVelocity; }
                set { outputVelocity = value; }
            }

            [SerializeField, Tooltip("Output hit velocity.")]
            private bool outputHitVelocity;
            public bool OutputHitVelocity
            {
                get { return outputHitVelocity; }
                set { outputHitVelocity = value; }
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

            [SerializeField, Tooltip("Connected collision.")]
            private Collision collision;
            public Collision _collision
            {
                get { return collision; }
                set { collision = value; }
            }

            [SerializeField, Tooltip("Connected GameObject colliders.")]
            private Collider[] colliders;
            public Collider[] _colliders
            {
                get { return colliders; }
                set { colliders = value; }
            }

            [SerializeField, Tooltip("Connected GameObject HingeJoint.")]
            private HingeJoint hingeJoint;
            public HingeJoint _hingeJoint
            {
                get { return hingeJoint; }
                set { hingeJoint = value; }
            }

            [SerializeField, Tooltip("Connected GameObject FixedJoint.")]
            private FixedJoint fixedJoint;
            public FixedJoint _fixedJoint
            {
                get { return fixedJoint; }
                set { fixedJoint = value; }
            }

            [SerializeField, Tooltip("Connected GameObject CharacterJoint.")]
            private CharacterJoint characterJoint;
            public CharacterJoint _characterJoint
            {
                get { return characterJoint; }
                set { characterJoint = value; }
            }

            [SerializeField, Tooltip("Connected GameObject SpringJoint.")]
            private SpringJoint springJoint;
            public SpringJoint _springJoint
            {
                get { return springJoint; }
                set { springJoint = value; }
            }
        }

        #endregion

        #region Enums

        public enum JointType
        {
            None = 0,
            Fixed = 1,
            Character = 2,
            Spring = 3,
            Hinge = 4,
        }

        public enum ParentType
        {
            None = 0,
            ChildOnStick = 1,
            ParentOnStick = 2,
        }

        #endregion

        #region Properties

        static public string WarningMessageHingeUnity_5_4 = "Hinge Joints Requires Upgrading Project to Unity 5.4 or Higher.";

        //Events
        static public event Action<SSS> OnStick;
        static public event Action<SSS> OnUnStick;
        static public event Action<SSS> OnStickBreak;

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
        private ParentType parentType;
        public ParentType _parentType
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
        [SerializeField, Tooltip("Character Joints are mainly used for Ragdoll effects.")]
        private CharacterJointProperties characterJointProperties;
        public CharacterJointProperties _characterJointProperties
        {
            get { return characterJointProperties; }
            set { characterJointProperties = value; }
        }
        [SerializeField, Tooltip("The Spring Joint joins two Rigidbodies together but allows the distance between them to change as though they were connected by a spring.")]
        private SpringJointProperties springJointProperties;
        public SpringJointProperties _springJointProperties
        {
            get { return springJointProperties; }
            set { springJointProperties = value; }
        }
        [SerializeField, Tooltip("The Hinge Joint groups together two Rigidbodies, constraining them to move like they are connected by a hinge.")]
        private HingeJointProperties hingeJointProperties;
        public HingeJointProperties _hingeJointProperties
        {
            get { return hingeJointProperties; }
            set { hingeJointProperties = value; }
        }
        [SerializeField, Tooltip("Contains all UnityEvent for StickyStickStuck.")]
        private EventProperties eventProperties;
        public EventProperties _eventProperties
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
        private DebugProperties debugProperties;
        public DebugProperties _debugProperties
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
        private Rigidbody mainRigidbody;
        private Collider[] colliders;
        private float velocity = 0f;
        private bool inTriggerArea = false;

        public SSS()
        {
            _fixedJointProperties = new FixedJointProperties();
            _characterJointProperties = new CharacterJointProperties();
            _characterJointProperties._twistLimitSprin = new CharacterJointProperties.TwistLimitSpring();
            _characterJointProperties._lowTwistLimit = new CharacterJointProperties.LowTwistLimit();
            _characterJointProperties._highTwistLimit = new CharacterJointProperties.HighTwistLimit();
            _characterJointProperties._swingLimitSpring = new CharacterJointProperties.SwingLimitSpring();
            _characterJointProperties._swing1Limit = new CharacterJointProperties.Swing1Limit();
            _characterJointProperties._swing2Limit = new CharacterJointProperties.Swing2Limit();
            _springJointProperties = new SpringJointProperties();
            _hingeJointProperties = new HingeJointProperties();
            _hingeJointProperties._spring = new HingeJointProperties.JointSpring();
            _hingeJointProperties._motor = new HingeJointProperties.JointMotor();
            _hingeJointProperties._limits = new HingeJointProperties.JointLimits();

            _eventProperties = new EventProperties();
            _filterProperties = new FilterProperties();
            _debugProperties = new DebugProperties();

            ConnectedObjectDataLists = new List<ConnectedObjectData>();
        }

        public void CopyStickyStickStuck(SSS copy)
        {
            copy.Enable = this.Enable;
            copy.Paused = this.Paused;
            copy._parentType = this._parentType;
            copy._jointType = this._jointType;

            copy._fixedJointProperties = this._fixedJointProperties;
            copy._characterJointProperties = this._characterJointProperties;
            copy._springJointProperties = this._springJointProperties;
            copy._hingeJointProperties = this._hingeJointProperties;

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
                    Vector3 anchor = Vector3.zero;
                    Vector3 connectedAnchor = Vector3.zero;
                    if (item._hingeJoint != null)
                    {
                        Gizmos.color = Color.gray;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._hingeJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._hingeJoint.connectedAnchor);
                    }
                    if (item._fixedJoint != null)
                    {
                        Gizmos.color = Color.red;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._fixedJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._fixedJoint.connectedAnchor);
                    }
                    if (item._characterJoint != null)
                    {
                        Gizmos.color = Color.green;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._characterJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._characterJoint.connectedAnchor);

                    }
                    if (item._springJoint != null)
                    {
                        Gizmos.color = Color.cyan;
                        anchor = this.transform.localToWorldMatrix.MultiplyPoint(item._springJoint.anchor);
                        connectedAnchor = item._gameObject.transform.localToWorldMatrix.MultiplyPoint(item._springJoint.connectedAnchor);
                    }

                    Gizmos.DrawIcon(anchor, icon, true);

                    if (Vector3.Distance(anchor, connectedAnchor) > .1f && item._gameObject.GetComponent<Rigidbody>() != null)
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
                case JointType.Character:
                    icon = icon + "_c";
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
            if (this.gameObject.GetComponent<Rigidbody>() != null)
                mainRigidbody = this.GetComponent<Rigidbody>();

            colliders = this.GetComponentsInChildren<Collider>();

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
                Debug.Log(string.Format("'{0}' Velocity: {1}", this.name, mainRigidbody.velocity.magnitude));
        }

        void OnTriggerEnter(Collider other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = true;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (StickInTriggerArea)
            {
                inTriggerArea = false;
            }
        }

        void OnCollisionEnter(Collision collision)
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

        void OnCollisionStay(Collision collision)
        {
            //Used to add the joint when collision happens
            if (Enable && !Paused)
            {
                AddStickyJoint(collision);
            }
        }

        //For when the joint breaks
        void OnJointBreak(float breakForce)
        {
            _eventProperties.OnStickBreak.Invoke();

            if (OnStickBreak != null)
                OnStickBreak(this);

            CleanUp();
        }

        #endregion

        #region Physics / Collision

        private void AddStickyJoint(Collision whatIHit)
        {
            if (CheckIfOkToStick(whatIHit))
            {
                ConnectedObjectData connectedObjectData = new ConnectedObjectData();
                if (_parentType == ParentType.ParentOnStick)
                    connectedObjectData.Parent = whatIHit.gameObject.transform.parent;
                connectedObjectData._gameObject = whatIHit.gameObject;
                connectedObjectData._collision = whatIHit;
                connectedObjectData._colliders = whatIHit.gameObject.GetComponentsInChildren<Collider>();
                Rigidbody whatIhitRigidbody = whatIHit.gameObject.GetComponent<Rigidbody>();

                Vector3 localAnchor = this.gameObject.transform.worldToLocalMatrix.MultiplyPoint(whatIHit.contacts[0].point);
                Vector3 connectedAncor = whatIHit.gameObject.transform.worldToLocalMatrix.MultiplyPoint(whatIHit.contacts[0].point);
                Vector3 localAxis = -whatIHit.contacts[0].normal;

                Transform newStartParent = connectedObjectData._gameObject.transform.parent;

                if (IgnoreColliders)
                {
                    foreach (var myCollider in colliders)
                    {
                        foreach (var whatIHitCollider in connectedObjectData._colliders)
                        {
                            Physics.IgnoreCollision(myCollider, whatIHitCollider, true);
                        }
                    }
                }

                this.transform.Translate(StickOffset);
                
                switch (_jointType)
                {
                    case JointType.Fixed:

                        FixedJoint fixedJoint = new FixedJoint();
                        fixedJoint = this.gameObject.AddComponent<FixedJoint>();
                        fixedJoint.anchor = localAnchor;
                        fixedJoint.connectedAnchor = connectedAncor;
                        fixedJoint.axis = localAxis;

                        fixedJoint.breakForce = this._fixedJointProperties.BreakForce;
                        fixedJoint.breakTorque = this._fixedJointProperties.BreakTorque;

                        fixedJoint.enableCollision = this._fixedJointProperties.EnableCollision;
                        fixedJoint.enablePreprocessing = this._fixedJointProperties.EnablePreprocessing;

                        fixedJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._fixedJoint = fixedJoint;

                        break;
                    case JointType.Character:

                        CharacterJoint characterJoint = new CharacterJoint();
                        characterJoint = this.gameObject.AddComponent<CharacterJoint>();
                        characterJoint.anchor = localAnchor;
                        characterJoint.connectedAnchor = connectedAncor;
                        characterJoint.axis = localAxis;

                        SoftJointLimitSpring softJointLimitSpring = new SoftJointLimitSpring();
                        softJointLimitSpring.spring = this._characterJointProperties._twistLimitSprin.Spring;
                        softJointLimitSpring.damper = this._characterJointProperties._twistLimitSprin.Damper;
                        characterJoint.twistLimitSpring = softJointLimitSpring;

                        SoftJointLimit lowTwistLimit = new SoftJointLimit();
                        lowTwistLimit.limit = this._characterJointProperties._lowTwistLimit.Limit;
                        lowTwistLimit.bounciness = this._characterJointProperties._lowTwistLimit.Bounciness;
                        lowTwistLimit.contactDistance = this._characterJointProperties._lowTwistLimit.ContactDistance;
                        characterJoint.lowTwistLimit = lowTwistLimit;

                        SoftJointLimit highTwistLimit = new SoftJointLimit();
                        highTwistLimit.limit = this._characterJointProperties._highTwistLimit.Limit;
                        highTwistLimit.bounciness = this._characterJointProperties._highTwistLimit.Bounciness;
                        highTwistLimit.contactDistance = this._characterJointProperties._highTwistLimit.ContactDistance;
                        characterJoint.highTwistLimit = highTwistLimit;

                        SoftJointLimitSpring swingLimitSpring = new SoftJointLimitSpring();
                        swingLimitSpring.spring = this.characterJointProperties._swingLimitSpring.Spring;
                        swingLimitSpring.damper = this.characterJointProperties._swingLimitSpring.Damper;
                        characterJoint.swingLimitSpring = swingLimitSpring;

                        SoftJointLimit swing1Limit = new SoftJointLimit();
                        swing1Limit.limit = this._characterJointProperties._swing1Limit.Limit;
                        swing1Limit.bounciness = this._characterJointProperties._swing1Limit.Bounciness;
                        swing1Limit.contactDistance = this._characterJointProperties._swing1Limit.ContactDistance;
                        characterJoint.swing1Limit = swing1Limit;

                        SoftJointLimit swing2Limit = new SoftJointLimit();
                        swing2Limit.limit = this._characterJointProperties._swing2Limit.Limit;
                        swing2Limit.bounciness = this._characterJointProperties._swing2Limit.Bounciness;
                        swing2Limit.contactDistance = this._characterJointProperties._swing2Limit.ContactDistance;
                        characterJoint.swing2Limit = swing2Limit;

                        characterJoint.enableProjection = this._characterJointProperties.EnableProjection;
                        characterJoint.projectionDistance = this._characterJointProperties.ProjectionDistance;
                        characterJoint.projectionAngle = this._characterJointProperties.ProjectionAngle;

                        characterJoint.breakForce = this._characterJointProperties.BreakForce;
                        characterJoint.breakTorque = this._characterJointProperties.BreakTorque;

                        characterJoint.enableCollision = this._characterJointProperties.EnableCollision;
                        characterJoint.enablePreprocessing = this._characterJointProperties.EnablePreprocessing;

#if(UNITY_2017_2_OR_NEWER)
                        characterJoint.massScale = this._characterJointProperties.MassScale;
                        characterJoint.connectedMassScale = this._characterJointProperties.ConnectedMassScale;
#endif

                        characterJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._characterJoint = characterJoint;

                        break;
                    case JointType.Spring:

                        SpringJoint springJoint = new SpringJoint();
                        springJoint = this.gameObject.AddComponent<SpringJoint>();
                        springJoint.anchor = localAnchor;
                        springJoint.connectedAnchor = connectedAncor;
                        springJoint.axis = localAxis;

                        springJoint.spring = this._springJointProperties.Spring;
                        springJoint.damper = this._springJointProperties.Damper;
                        springJoint.minDistance = this._springJointProperties.MinDistance;
                        springJoint.maxDistance = this._springJointProperties.MaxDistance;

                        springJoint.tolerance = this._springJointProperties.Tolerance;

                        springJoint.breakForce = this._springJointProperties.BreakForce;
                        springJoint.breakTorque = this._springJointProperties.BreakTorque;

                        springJoint.enableCollision = this._springJointProperties.EnableCollision;
                        springJoint.enablePreprocessing = this._springJointProperties.EnablePreprocessing;

#if (UNITY_2017_2_OR_NEWER)
                        springJoint.massScale = this._springJointProperties.MassScale;
                        springJoint.connectedMassScale = this._springJointProperties.ConnectedMassScale;
#endif

                        springJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._springJoint = springJoint;

                        break;
                    case JointType.Hinge:

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3)
                        HingeJoint hingeJoint = new HingeJoint();
                        hingeJoint = this.gameObject.AddComponent<HingeJoint>();
                        hingeJoint.anchor = localAnchor;
                        hingeJoint.axis = localAxis;

                        hingeJoint.useSpring = this._hingeJointProperties.UseSpring;
                        JointSpring jointSpring = new JointSpring();
                        jointSpring.spring = this._hingeJointProperties._spring.Spring;
                        jointSpring.damper = this._hingeJointProperties._spring.Damper;
                        jointSpring.targetPosition = this._hingeJointProperties._spring.TargetPosition;
                        hingeJoint.spring = jointSpring;

                        hingeJoint.useMotor = this._hingeJointProperties.UseMotor;
                        JointMotor jointMotor = new JointMotor();
                        jointMotor.targetVelocity = this._hingeJointProperties._motor.TargetVelocity;
                        jointMotor.force = this._hingeJointProperties._motor.Force;
                        jointMotor.freeSpin = this._hingeJointProperties._motor.FreeSpin;
                        hingeJoint.motor = jointMotor;

                        hingeJoint.useLimits = this._hingeJointProperties.UseLimits;
                        JointLimits jointLimits = new JointLimits();
                        jointLimits.min = this._hingeJointProperties._limits.Min;
                        jointLimits.max = this._hingeJointProperties._limits.Max;
                        jointLimits.bounciness = this._hingeJointProperties._limits.Bounciness;
                        jointLimits.bounceMinVelocity = this._hingeJointProperties._limits.BounceMinVelocity;
                        jointLimits.contactDistance = this._hingeJointProperties._limits.ContactDistance;
                        hingeJoint.limits = jointLimits;

                        hingeJoint.breakForce = this._hingeJointProperties.BreakForce;
                        hingeJoint.breakTorque = this._hingeJointProperties.BreakTorque;

                        hingeJoint.enableCollision = this._hingeJointProperties.EnableCollision;
                        hingeJoint.enablePreprocessing = this._hingeJointProperties.EnablePreprocessing;

#if (UNITY_2017_2_OR_NEWER)
                        hingeJoint.massScale = this._hingeJointProperties.MassScale;
                        hingeJoint.connectedMassScale = this._hingeJointProperties.ConnectedMassScale;
#endif

                        hingeJoint.connectedBody = whatIhitRigidbody;

                        connectedObjectData._hingeJoint = hingeJoint;
#else
                        Debug.LogWarning(WarningMessageHingeUnity_5_4);
#endif

                        break;
                    case JointType.None:

                        break;
                }

                //Used for when Recursive Infection is checked
                if (StickRecursively)
                {
                    if (connectedObjectData._gameObject.GetComponent<SSS>() == null)
                    {
                        SSS stickyStickStuck = connectedObjectData._gameObject.AddComponent<SSS>();

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
                    case ParentType.None:
                        break;
                    case ParentType.ChildOnStick:
                        this.transform.SetParent(connectedObjectData._gameObject.transform, true);
                        break;
                    case ParentType.ParentOnStick:
                        connectedObjectData._gameObject.transform.SetParent(this.transform, true);
                        break;
                }

                ConnectedObjectDataLists.Add(connectedObjectData);

                //Events
                if (OnStick != null)
                    OnStick(this);
                connectedObjectData._gameObject.SendMessage("OnStick", this, SendMessageOptions.DontRequireReceiver);
                _eventProperties.OnStickHandler.Invoke();
                
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
                if (!connectedObjectDataLists[i]._gameObject.activeInHierarchy || connectedObjectDataLists[i]._collision.collider.isTrigger)
                {
                    return true;
                }
            }

            return false;
        }

        //This is where it check to see if its ok to add a sticky joint
        private bool CheckIfOkToStick(Collision whatIHit)
        {
            //if whatIhit equals null then fail
            if (whatIHit == null)
            {
                return false;
            }

            //if whatIhit collider equals the parents collider then fail
            if (whatIHit.collider == this.gameObject.GetComponent<Collider>())
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
                if (whatIHit.gameObject.GetComponent<SSS>() != null)
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

            var whatIHitGameObject = whatIHit.contacts[0].otherCollider.gameObject;

            if (whatIHitGameObject == null)
                return false;
            
            //Validated by the filter options
            if (!_filterProperties.ValidateFilters(whatIHitGameObject, whatIHit.contacts[0].otherCollider))
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
                Destroy(this.GetComponent<SSS>());
            }
        }

        //Cleans up all the joints
        private void CleanupJoints()
        {
            foreach (var item in ConnectedObjectDataLists)
            {
                if (item._hingeJoint != null)
                {
                    Destroy(item._hingeJoint);
                }
                if (item._fixedJoint != null)
                {
                    Destroy(item._fixedJoint);
                }
                if (item._characterJoint != null)
                {
                    Destroy(item._characterJoint);
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
                            Physics.IgnoreCollision(whatIHitCollider, myCollider, false);
                        }
                    }
                }

                if (item._gameObject != null)
                {
                    //Events
                    if (OnUnStick != null)
                        OnUnStick(this);
                    item._gameObject.SendMessage("OnUnStick", this, SendMessageOptions.DontRequireReceiver);
                    _eventProperties.OnUnStickHandler.Invoke();

                    SSS stickyStickStuck = item._gameObject.GetComponent<SSS>();

                    if (stickyStickStuck != null)
                    {
                        if (stickyStickStuck.IsInfectParent == false)
                        {
                            stickyStickStuck.Enable = false;
                        }
                    }

                    if (_parentType == ParentType.ParentOnStick)
                        item._gameObject.transform.SetParent(item.Parent, true);
                }
            }
        }

        #endregion
    }
}
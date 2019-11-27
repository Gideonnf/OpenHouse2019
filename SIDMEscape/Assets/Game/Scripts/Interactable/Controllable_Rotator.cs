using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRControllables.Base.Rotator
{
    public class Controllable_Rotator : Controllable_Movables
    {
        /// <summary>
        /// The type of rotation that is affecting the object
        /// </summary>
        public enum RotatingType
        {
            /// <summary>
            /// Calculates the angel between the object origin and the attach point for all axis
            /// </summary>
            AttachPointRotation,
            // Calculates the angular velocity of the grabbing object's longitudinal axis (Roll Axis)
            RollAxisRotation,
            /// <summary>
            /// Calculates the angular velocity of the grabbing object's Lateral Axis (Pitch Axis)
            /// </summary>
            PitchAxisRotation,
            /// <summary>
            /// Calculates the angular velocity of the grabbing object's perpendicular Axis (Yaw Axis)
            /// </summary>
            YawAxisRotation
        }

        [Header("Interaection Settings")]
        [Tooltip("The maximum distance that the hand can move from the object before breaking the link")]
        public float detachDistance = 1f;
        [Tooltip("The distance between the grabbing object's attach point and the interactable object's center. If it is grabbed within this range, it'll automatically be ungrabbed.")]
        public float originDeadzone = 0f;

        [Header("Rotation Settings")]
        [Tooltip("Determines how the rotation of the object is calculated based on the grabbing object")]
        public RotatingType rotatingAction = RotatingType.AttachPointRotation;
        [Tooltip("Friction applied when rotating, simulates a tougher rotation")]
        [Range(1f, 32f)]
        public float rotationFriction = 1f;
        [Tooltip("Deaccaleration Damper decides how slow the object's rotation will decrease when released to simulate momentum. The higher the number, the faster it will stop")]
        public float DecelarationDamper = 1f;
        [Tooltip("The speed of the object when rotating back to its origin rotation when released. If set to 0, the rotation will not reset")]
        public float resetToOriginSpeed = 0f;

        [Header("Rotation Limits")]
        [Tooltip("The limits for the rotation allowed on the operating axis")]
        public Limit2D angleLimits = new Limit2D(-180, 180);

        protected Vector3 previousAttachPointPosition;
        protected Vector3 currentRotation;
        protected Vector3 currentRotationSpeed;
        protected Coroutine updateRotationRoutine; // Handles the rotation of the interacted Object
        protected Coroutine decelerateRotationRoutine; // Handles the deceleration(Slow Down) of the interacted object upon release

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


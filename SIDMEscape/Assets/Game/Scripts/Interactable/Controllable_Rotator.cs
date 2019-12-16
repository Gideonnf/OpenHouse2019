using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRControllables.Base.Rotator
{
    // Note edited and based off VRTK
    // Can't use VRTK because it doesnt work that well with the quest
    // But am adjusting and modifying parts of it while removing alot of the complicated parenting calls and event calls

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

        protected OVRGrabber handReference;
        protected Vector3 previousAttachPointPosition;
        protected Vector3 currentRotation;
        protected Vector3 currentRotationSpeed;
        protected Coroutine updateRotationRoutine; // Handles the rotation of the interacted Object
        protected Coroutine decelerateRotationRoutine; // Handles the deceleration(Slow Down) of the interacted object upon release
        protected bool[] limitsReached = new bool[2];

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

        }

        // Update is called once per frame
        void Update()
        {
            // Check if theres a point to follow
            if (grabbedObjectAttachPoint != null)
            {
                float distance = Vector3.Distance(transform.position, controllerAttachPoint.transform.position);
                if (StillTouching() && distance >= originDeadzone)
                {
                    Vector3 newRotation = GetNewRotation();
                    previousAttachPointPosition = controllerAttachPoint.transform.position;
                    currentRotationSpeed = newRotation;
                    UpdateRotation(newRotation, true, true);
                }
                //else if (grabbedo)
            }
        }

        protected override bool CustomGrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            CancelUpdateRotation();
            CancelDecelerateRotation();
            bool result =  base.CustomGrabBegin(hand, grabPoint);
            previousAttachPointPosition = controllerAttachPoint.transform.position;
            limitsReached = new bool[2];
            CheckAngleLimits();
            handReference = hand;

            if (grabbedObjectAttachPoint == null)
            {
                grabbedObjectAttachPoint = new GameObject("AttachPointForObjects").transform;

                grabbedObjectAttachPoint.SetParent(this.grabPoint);
                grabbedObjectAttachPoint.position = this.grabPoint.position;
                grabbedObjectAttachPoint.rotation = this.grabPoint.rotation;

                grabbedObjectAttachPoint.localScale = Vector3.one;
            }

            return result;
        }

        protected override bool CustomGrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            bool result = base.CustomGrabEnd(linearVelocity, angularVelocity);

            if(resetToOriginSpeed > 0f)
            {
                ResetRotation();
            }
            else if (DecelarationDamper > 0f)
            {
                CancelDecelerateRotation();
                decelerateRotationRoutine = StartCoroutine(DecelerateRotation());
            }

            if (grabbedObjectAttachPoint != null)
            {
                Destroy(grabbedObjectAttachPoint);
                grabbedObjectAttachPoint = null;
            }

            return result;
        }

        public void SetRotation(float newAngle, float transitionTime = 0f)
        {
            newAngle = Mathf.Clamp(newAngle, angleLimits.minimum, angleLimits.maximum);
            Vector3 newCurrentRotation = currentRotation;
            switch (operateAxis)
            {
                case OperatingAxis.xAxis:
                    {
                        newCurrentRotation = new Vector3(newAngle, currentRotation.y, currentRotation.z);
                    }
                    break;
                case OperatingAxis.yAxis:
                    {
                        newCurrentRotation = new Vector3(currentRotation.x, newAngle, currentRotation.z);
                    }
                    break;
                case OperatingAxis.zAxis:
                    {
                        newCurrentRotation = new Vector3(currentRotation.x, currentRotation.y, newAngle);
                    }
                    break;
            }

            if (transitionTime > 0f)
            {
                CancelUpdateRotation();
                updateRotationRoutine = StartCoroutine(RotateToAngle(newCurrentRotation, VRControllable_Methods.DividerToMultiplier(transitionTime)));
            }
            else
            {
                UpdateRotation(newCurrentRotation, false, false);
                currentRotation = newCurrentRotation;
            }

        }

        /*
         * IEnumerator function calls are here
         * 
         */
        #region ENUMERATOR FUNCTIONS

        protected IEnumerator RotateToAngle(Vector3 targetAngle, float rotationSpeed)
        {
            Vector3 previousRotation = currentRotation;
            currentRotationSpeed = Vector3.zero;
            while (currentRotation != targetAngle)
            {
                currentRotation = Vector3.Lerp(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
                //UpdateRotation(currentRotation - previousRotation, true, false);
                previousRotation = currentRotation;
                yield return null;
            }

            UpdateRotation(targetAngle, false, false);
            currentRotation = targetAngle;

        }

        protected IEnumerator DecelerateRotation()
        {
            while (currentRotationSpeed != Vector3.zero)
            {
                currentRotationSpeed = Vector3.Slerp(currentRotationSpeed, Vector3.zero, DecelarationDamper * Time.deltaTime);
                UpdateRotation(currentRotationSpeed, true, true);
                yield return null;
            }
        }

        #endregion

        #region RotationCalculations

        protected void UpdateRotation(Vector3 newRotation, bool additive, bool updateCurrentRotation)
        {
            if (WithinRotationLimit(currentRotation + newRotation))
            {
                if(updateCurrentRotation)
                {
                    currentRotation += newRotation;
                }
            }
            transform.localRotation = (additive ? transform.localRotation * Quaternion.Euler(newRotation) : Quaternion.Euler(newRotation));
            // Emit events? idk if we gonna use that in our version
        }

        protected void ResetRotation(bool ignoreTransition = false)
        {
            CancelDecelerateRotation();
            if (resetToOriginSpeed > 0f && !ignoreTransition)
            {
                CancelUpdateRotation();
                updateRotationRoutine = StartCoroutine(RotateToAngle(Vector3.zero, resetToOriginSpeed));
            }
            else
            {
                UpdateRotation(originalRotation.eulerAngles, false, false);
                currentRotation = Vector3.zero;
                currentRotationSpeed = Vector3.zero;
            }
        }

        protected Vector3 GetNewRotation()
        {
            Vector3 grabbingObjectAngularVelocity = Vector3.zero;
            grabbingObjectAngularVelocity = controllerAttachPoint.angularVelocity * VRControllable_Methods.DividerToMultiplier(rotationFriction);

            switch(rotatingAction)
            {
                case RotatingType.AttachPointRotation:
                    return CalculateAngle(transform.position, previousAttachPointPosition, controllerAttachPoint.transform.position);
                case RotatingType.RollAxisRotation:
                    return BuildFollowAxisVector(grabbingObjectAngularVelocity.x);
                case RotatingType.YawAxisRotation:
                    return BuildFollowAxisVector(grabbingObjectAngularVelocity.y);
                case RotatingType.PitchAxisRotation:
                    return BuildFollowAxisVector(grabbingObjectAngularVelocity.z);
            }

            return Vector3.zero;
        }

        protected Vector3 BuildFollowAxisVector(float givenAngle)
        {
            float xAngle = (operateAxis == OperatingAxis.xAxis ? givenAngle : 0f);
            float yAngle = (operateAxis == OperatingAxis.yAxis ? givenAngle : 0f);
            float zAngle = (operateAxis == OperatingAxis.zAxis ? givenAngle : 0f);

            return new Vector3(xAngle, yAngle, zAngle);
        }

        protected Vector3 CalculateAngle(Vector3 originPoint, Vector3 originalGrabPoint, Vector3 currentGrabPoint)
        {
            float xRotated = (operateAxis == OperatingAxis.xAxis ? CalculateAngle(originPoint, originalGrabPoint, currentGrabPoint, transform.right) : 0f);
            float yRotated = (operateAxis == OperatingAxis.xAxis ? CalculateAngle(originPoint, originalGrabPoint, currentGrabPoint, transform.up) : 0f);
            float zRotated = (operateAxis == OperatingAxis.xAxis ? CalculateAngle(originPoint, originalGrabPoint, currentGrabPoint, transform.forward) : 0f);

            float frictionMultiplier = VRControllable_Methods.DividerToMultiplier(rotationFriction);
            return new Vector3(xRotated * frictionMultiplier, yRotated * frictionMultiplier, zRotated * frictionMultiplier);
        }

        protected float CalculateAngle(Vector3 originPoint, Vector3 previousPoint, Vector3 currentPoint, Vector3 direction)
        {
            Vector3 sideA = previousPoint - originPoint;
            Vector3 sideB = VRControllable_Methods.VectorDirection(originPoint, currentPoint);
            return AngleSigned(sideA, sideB, direction);
        }

        #endregion

        #region ComplimentaryFunctions

        protected bool WithinRotationLimit(Vector3 rotationCheck)
        {
            switch(operateAxis)
            {
                case OperatingAxis.xAxis:
                    return angleLimits.WithinLimits(rotationCheck.x);
                case OperatingAxis.yAxis:
                    return angleLimits.WithinLimits(rotationCheck.y);
                case OperatingAxis.zAxis:
                    return angleLimits.WithinLimits(rotationCheck.z);
            }

            return false;
        }

        protected float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
        {
            return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        }

        protected void CancelUpdateRotation()
        {
            if (updateRotationRoutine != null)
            {
                StopCoroutine(updateRotationRoutine);
            }
        }

        public void CancelDecelerateRotation()
        {
            if (decelerateRotationRoutine != null)
            {
                StopCoroutine(decelerateRotationRoutine);
            }
        }

        public void CheckAngleLimits()
        {
            angleLimits.minimum = (angleLimits.minimum > 0f ? angleLimits.minimum * -1f : angleLimits.minimum);
            angleLimits.maximum = (angleLimits.maximum < 0f ? angleLimits.maximum * -1f : angleLimits.maximum);
        }

        protected bool StillTouching()
        {
            float distance = Vector3.Distance(controllerAttachPoint.transform.position, initialAttachPoint.position);
            return (distance <= detachDistance);
        }

        #endregion
    }
}


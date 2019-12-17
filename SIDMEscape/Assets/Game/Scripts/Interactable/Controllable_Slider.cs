using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRControllables.Base.Slider
{

    public class Controllable_Slider : Controllable_Movables
    {
        [Header("Drawer Settings")]
        [Tooltip("The max distance that the object can move before being detached")]
        public float detachDistance = 1f;

        [Header("Drawer Settings")]
        [Tooltip("How fast the object will track the hand when grabbed")]
        public float trackingSpeed = 10.0f;
        [Tooltip("The speed that the object will return to original position")]
        public float resetSpeed = 0.0f;

        //[Tooltip("The maximum distance that the drawer can go through")]
        //public float maxDistance = 0.1f;
        [Tooltip("The minimum and maximum limit that the object can move along the x-axis, y-axis and z-axis")]
        public Limit2D[] axisLimit = new Limit2D[3];

        private Vector3 previousPosition;
        private Vector3 movementVelocity;
        private float distanceOffset = 0.0f;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            axisLimit[(int)OperatingAxis.xAxis].maximum += transform.localPosition.x;
            axisLimit[(int)OperatingAxis.xAxis].minimum = transform.localPosition.x - axisLimit[(int)OperatingAxis.xAxis].minimum;

            axisLimit[(int)OperatingAxis.yAxis].maximum += transform.localPosition.y;
            axisLimit[(int)OperatingAxis.yAxis].minimum = transform.localPosition.y - axisLimit[(int)OperatingAxis.yAxis].minimum;

            axisLimit[(int)OperatingAxis.zAxis].maximum += transform.localPosition.z;
            axisLimit[(int)OperatingAxis.zAxis].minimum = transform.localPosition.z - axisLimit[(int)OperatingAxis.zAxis].minimum;

        }

        /// <summary>
        /// Clamps the position of the drawer so that it won't go out of the limits
        /// </summary>
        protected virtual void ClampPosition()
        {
            transform.localPosition = new Vector3(ClampAxis(axisLimit[0], transform.localPosition.x), ClampAxis(axisLimit[1], transform.localPosition.y), ClampAxis(axisLimit[2], transform.localPosition.z));
        }

        /// <summary>
        /// Clamps the value of the x position to the limits of the axis
        /// </summary>
        /// <param name="limits"> The minimum and maximum limits set for the axis </param>
        /// <param name="axisValue"> THe local position/The value to clamp between the axis limits </param>
        /// <returns></returns>
        protected virtual float ClampAxis(Limit2D limits, float axisValue)
        {
            axisValue = (axisValue < limits.minimum + minMaxThreshold ? limits.minimum : axisValue);
            axisValue = (axisValue > limits.maximum - minMaxThreshold ? limits.maximum : axisValue);
            return Mathf.Clamp(axisValue, limits.minimum, limits.maximum);
        }

        /// <summary>
        /// Get Value returns the position of the drawer normalized
        /// 
        /// </summary>
        public virtual float GetNormalizedValue()
        {
            return VRControllable_Methods.NormalizeValue(GetValue(), originalPosition[(int)operateAxis], MaximumLength()[(int)operateAxis]);
        }


        /// <summary>
        /// Gets the maximum length based on the operating axis
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 MaximumLength()
        {
            return originalPosition + (AxisDirection() * axisLimit[(int)operateAxis].maximum);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
          //  Debug.Log("current Position " + transform.position);
          //  Debug.Log("current local Position " + transform.localPosition);

            if (grabbedObjectAttachPoint != null)
            {
                ProcessUpdate();
            }
        }

        protected void ProcessUpdate()
        {

            if (grabbedObjectAttachPoint != null)
            {
                Vector3 currentPosition = transform.localPosition;
                Vector3 movePosition = currentPosition + Vector3.Scale((transform.InverseTransformPoint(controllerAttachPoint.transform.position) - transform.InverseTransformPoint(grabbedObjectAttachPoint.position)), transform.localScale);

                float distance = Vector3.Distance(grabbedObjectAttachPoint.position, initialAttachPoint.position);
                //Debug.Log("current distance" + distance);
                if (distance > (detachDistance + distanceOffset))
                {
                    //Debug.Log("max distance reached");
                    distance = Vector3.Distance(controllerAttachPoint.transform.position, initialAttachPoint.position);
                    if (distance > (detachDistance + distanceOffset))
                    {
                        movePosition = currentPosition;
                    }
                }

                //Vector3 targetPosition = Vector3.Lerp(currentPosition, movePosition, trackingSpeed * Time.deltaTime);
                previousPosition = transform.localPosition;
                // Update to the new position
                UpdatePosition(movePosition, false);
                // Set the velocity of movement
                movementVelocity = transform.localPosition - previousPosition;
            }
        }

        /// <summary>
        /// Used for updating the position of the drawer
        /// </summary>
        /// <param name="newPosition"> The new position to move to </param>
        /// <param name="additive"> if its being added to the current position, set to true. Used for example, constant forward movement</param>
        /// <param name="forceClamp"> usually always clamp the position</param>
        protected virtual void UpdatePosition(Vector3 newPosition, bool additive, bool forceClamp = true)
        {
            transform.localPosition = (additive ? transform.localPosition + newPosition : newPosition);
            //Debug.Log("Local Position before clamp : " + transform.localPosition);
            if (forceClamp)
            {
                ClampPosition();
            }
           // Debug.Log("Local Position after clamp : " + transform.localPosition);
            // This is for checking against limits
            // Similar to the EmitEvents
            // TODO: Implement this part
            // a bit late for me to do it now
            UpdateControllable();
        }

        protected void UpdateControllable()
        {
            bool positionChanged = !VRControllable_Methods.Vector3ShallowCompare(transform.localPosition, previousPosition, positionFidelity);
            //throw new System.NotImplementedException();
            if (positionChanged)
            {
                float currentPosition = GetNormalizedValue();
               // Debug.Log("current position on operating Axis : " + currentPosition);
                // TODO: Check for the drawer hitting the limits
            }
        }

        protected override bool CustomGrabBegin(OVRGrabber grabbedBy, Collider grabPoint)
        {
            if (grabbedBy == null)
                return false;

            if (initialAttachPoint != null)
            {
                distanceOffset = Vector3.Distance(this.grabPoint.position, initialAttachPoint.position);
            }


            bool grabResult = base.CustomGrabBegin(grabbedBy, grabPoint);

            return grabResult;
        }

        protected override bool CustomGrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            bool endResult = base.CustomGrabEnd(linearVelocity, angularVelocity);

            // If it is more than 0.0f, reset it to the position set
            if (resetSpeed > 0.0f)
            {
                ResetPosition();
            }

            return endResult;
        }

        //TODO: Reset the drawer position
        protected virtual void ResetPosition()
        {

        }


    }


}


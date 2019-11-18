namespace VRControllables.Base.Drawer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Controllable_Drawer : BaseControllable
    {
        [Tooltip("The max distance that the object can move before being detached")]
        public float detachDistance = 1f;

        [Header("Drawer Settings")]
        [Tooltip("How fast the object will track the hand when grabbed")]
        public float trackingSpeed = 10.0f;
        [Tooltip("The speed that the object will return to original position")]
        public float resetSpeed = 0.0f;
        [Tooltip("If true, the grabbedObject will have it's rigidbody kinematic set to true")]
        public bool forceKinematics = true;
        //[Tooltip("The maximum distance that the drawer can go through")]
        //public float maxDistance = 0.1f;

        [Header("Drawer Limitations")]
        [Tooltip("The minimum and maximum limit that the object can move along the x-axis, y-axis and z-axis")]
        public Limit2D[] axisLimit = new Limit2D[3];
        //[Tooltip("The minimum and maximum limit that the object can move along the y-axis")]
        //public Limit2D yAxisLimit = Limit2D.zero;
        //[Tooltip("The minimum and maximuim limit that the object can move along the z-axis")]
        //public Limit2D zAxisLimit = Limit2D.zero;
        [Tooltip("the threshold the position needs to be in to register a min or max position")]
        public float minMaxThreshold = 0.01f;
        [Tooltip("The threshold the normalized position value needs to be within to register a min or max normalized position value.")]
        [Range(0f, 0.99f)]
        public float minMaxNormalizedThreshold = 0.01f;

        protected GameObject grabbedObject;
        protected Rigidbody grabbedObjectRB;
        //protected Transform trackPoint;
        protected Transform initialAttachPoint;
        protected Rigidbody controllerAttachPoint;
        protected Transform grabbedObjectAttachPoint;
        protected bool previousKinematicState;

        private Vector3 previousPosition;
        private Vector3 movementVelocity;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            axisLimit[0].name = "x-Axis Limit";
            axisLimit[1].name = "y-Axis Limit";
            axisLimit[2].name = "z-Axis Limit";
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if(grabbedObjectAttachPoint != null)
            {
                ProcessUpdate();
            }
        }

        /// <summary>
        /// 
        /// The GetValue method returns the current position of the drawer
        /// </summary>
        /// <returns>The actual position of the button.</returns>
        public virtual float GetValue()
        {
            return transform.localPosition[(int)operateAxis];
        }

        protected virtual Vector3 AxisDirection(bool local = false)
        {
            return VRControllable_Methods.AxisDirection((int)operateAxis, (local ? transform : null));
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

        protected virtual void ProcessUpdate()
        {
            if (grabbedObjectAttachPoint != null)
            {
                float distance = Vector3.Distance(grabbedObjectAttachPoint.position, initialAttachPoint.position);
                if (distance > detachDistance)
                {

                }
                else
                {
                    Vector3 currentPosition = transform.localPosition;
                    Vector3 movePosition = currentPosition + Vector3.Scale((transform.InverseTransformPoint(controllerAttachPoint.transform.position) - transform.InverseTransformPoint(grabbedObjectAttachPoint.position)), transform.localScale);
                    Vector3 targetPosition = Vector3.Lerp(currentPosition, movePosition, trackingSpeed * Time.deltaTime);

                    previousPosition = transform.localPosition;
                    // Update to the new position
                    UpdatePosition(targetPosition, false);
                    // Set the velocity of movement
                    movementVelocity = transform.localPosition - previousPosition;
                }
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
            if (forceClamp)
            {
               ClampPosition();
            }

            // This is for checking against limits
            // Similar to the EmitEvents
            // TODO: Implement this part
            // a bit late for me to do it now
            UpdateControllable();
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

        protected override void UpdateControllable()
        {
            bool positionChanged = !VRControllable_Methods.Vector3ShallowCompare(transform.localPosition, previousPosition, positionFidelity);
            //throw new System.NotImplementedException();
            if (positionChanged)
            {
                float currentPosition = GetNormalizedValue();
                Debug.Log("current position on operating Axis : " + currentPosition);
                // TODO: Check for the drawer hitting the limits
            }
        }

        protected override bool CustomGrabBegin(OVRGrabber grabbedBy, Collider grabPoint)
        {
            if (grabbedBy == null)
                return false;

            if (grabbedObject == null)
            {
                grabbedObject = this.gameObject;
                grabbedObjectRB = this.gameObject.GetComponent<Rigidbody>();
                previousKinematicState = grabbedObjectRB.isKinematic;

                // Checks if
                grabbedObjectRB.isKinematic = (forceKinematics ? true : previousKinematicState);
            }
            // Sets the starting attach point
            if (initialAttachPoint == null)
            {
                // Store the initial transform when first grabbed
                initialAttachPoint = grabbedObject.transform;
            }

            // Create a game object attached to the interactable
            if(grabbedObjectAttachPoint == null)
            {
                grabbedObjectAttachPoint = new GameObject("AttachPointForGrabbedObject").transform;
                grabbedObjectAttachPoint.SetParent(this.transform);
                grabbedObjectAttachPoint.position = transform.position;
                grabbedObjectAttachPoint.rotation = transform.rotation;
                grabbedObjectAttachPoint.localScale = Vector3.one;
            }

            if (controllerAttachPoint == null)
            {
                // Store the rigid bodyof the controller as reference
                controllerAttachPoint = grabbedBy.GetComponent<Rigidbody>();
            }

            bool grabResult = base.CustomGrabBegin(grabbedBy, grabPoint);

            return grabResult;
        }

        protected override bool CustomGrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            bool endResult = base.CustomGrabEnd(linearVelocity, angularVelocity);
            grabbedObject = null;
            //trackPoint = null;
            initialAttachPoint = null;
            controllerAttachPoint = null;
            if(grabbedObjectAttachPoint != null)
            {
                Destroy(grabbedObjectAttachPoint.gameObject);
            }

            if (grabbedObjectRB != null)
            {
                grabbedObjectRB.isKinematic = previousKinematicState;
            }

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


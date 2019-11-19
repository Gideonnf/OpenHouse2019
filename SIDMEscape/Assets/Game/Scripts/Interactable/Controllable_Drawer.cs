namespace VRControllables.Base.Drawer
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Controllable_Drawer : Controllable_Movables
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
        [Tooltip("Grab point of the drawer for distance offset")]
        public Transform grabPoint;

        private Vector3 previousPosition;
        private Vector3 movementVelocity;
        private float distanceOffset = 0.0f;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (grabbedObjectAttachPoint != null)
            {
                ProcessUpdate();
            }
        }

        protected override void ProcessUpdate()
        {
            if (grabbedObjectAttachPoint != null)
            {
                Vector3 currentPosition = transform.localPosition;
                Vector3 movePosition = currentPosition + Vector3.Scale((transform.InverseTransformPoint(controllerAttachPoint.transform.position) - transform.InverseTransformPoint(grabbedObjectAttachPoint.position)), transform.localScale);

                float distance = Vector3.Distance(grabbedObjectAttachPoint.position, initialAttachPoint.position);
                //Debug.Log("current distance" + distance);
                if (distance > (detachDistance + distanceOffset))
                {
                    Debug.Log("max distance reached");
                    distance = Vector3.Distance(controllerAttachPoint.transform.position, initialAttachPoint.position);
                    if (distance > (detachDistance + distanceOffset))
                    {
                        movePosition = currentPosition;
                    }
                }
                //else
                //{

                //}

                Vector3 targetPosition = Vector3.Lerp(currentPosition, movePosition, trackingSpeed * Time.deltaTime);
                Debug.Log("current Position " + currentPosition);
                Debug.Log("move position  "  + movePosition);
                Debug.Log("target position " + targetPosition);
                previousPosition = transform.localPosition;
                // Update to the new position
                UpdatePosition(targetPosition, false);
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
                initialAttachPoint = new GameObject("InitialAttachPoint").transform;
                initialAttachPoint.SetParent(this.transform.parent);
                initialAttachPoint.position = this.transform.parent.position;
                initialAttachPoint.rotation = this.transform.parent.rotation;
                initialAttachPoint.localScale = Vector3.one;

                distanceOffset = Vector3.Distance(this.grabPoint.position, initialAttachPoint.position);

                //initialAttachPoint = m_grabPoints[0].gameObject.transform.position;
            }

            // Create a game object attached to the interactable
            if (grabbedObjectAttachPoint == null)
            {
                grabbedObjectAttachPoint = new GameObject("AttachPointForGrabbedObject").transform;

                grabbedObjectAttachPoint.SetParent(this.grabPoint);
                grabbedObjectAttachPoint.position = this.grabPoint.position;
                grabbedObjectAttachPoint.rotation =this.grabPoint.rotation;

                //grabbedObjectAttachPoint.SetParent(this.transform);
                // grabbedObjectAttachPoint.position = this.transform.position;
                //grabbedObjectAttachPoint.rotation = this.transform.rotation;

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
            //if (initialAttachPoint != null)
            //{
            //    Destroy(initialAttachPoint.gameObject);
            //    initialAttachPoint = null;
            //}
            controllerAttachPoint = null;
            if (grabbedObjectAttachPoint != null)
            {
                Destroy(grabbedObjectAttachPoint.gameObject);
                grabbedObjectAttachPoint = null;
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


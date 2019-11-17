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

        [Header("Drawer Limitations")]
        [Tooltip("The minimum and maximum limit that the object can move along the x-axis")]
        public Limit2D xAxisLimit = Limit2D.zero;
        [Tooltip("The minimum and maximum limit that the object can move along the y-axis")]
        public Limit2D yAxisLimit = Limit2D.zero;
        [Tooltip("The minimum and maximuim limit that the object can move along the z-axis")]
        public Limit2D zAxisLimit = Limit2D.zero;
        [Tooltip("the threshold the position needs to be in to register a min or max position")]
        public float minMaxThreshold = 0.01f;
        [Tooltip("The threshold the normalized position value needs to be within to register a min or max normalized position value.")]
        [Range(0f, 0.99f)]
        public float minMaxNormalizedThreshold = 0.01f;

        protected GameObject grabbedObject;
        protected Rigidbody grabbedObjectRB;
        protected Transform trackPoint;
        protected Transform initialAttachPoint;
        protected Rigidbody controllerAttachPoint;
        protected bool previousKinematicState;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if(trackPoint != null)
            {
                ProcessUpdate();
            }
        }

        protected virtual void ProcessUpdate()
        {
            if (trackPoint != null)
            {
                float distance = Vector3.Distance(trackPoint.position, initialAttachPoint.position);

            }
        }

        protected override void UpdateControllable()
        {
            throw new System.NotImplementedException();
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
            trackPoint = null;
            initialAttachPoint = null;
            controllerAttachPoint = null;

            if(grabbedObjectRB != null)
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

        protected virtual void ResetPosition()
        {

        }


    }


}


﻿namespace VRControllables.Base
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Controllable_Movables : BaseControllable
    {
        [Header("Movable Limitations")]
        [Tooltip("the threshold the position needs to be in to register a min or max position")]
        public float minMaxThreshold = 0.01f;
        [Tooltip("The threshold the normalized position value needs to be within to register a min or max normalized position value.")]
        [Range(0f, 0.99f)]
        [Tooltip("If true, the grabbedObject will have it's rigidbody kinematic set to true")]
        public bool forceKinematics = true;

        public float minMaxNormalizedThreshold = 0.01f;
        protected GameObject grabbedObject;
        protected Rigidbody grabbedObjectRB;
        //protected Transform trackPoint;
        protected Transform initialAttachPoint;
        protected Rigidbody controllerAttachPoint;
        protected Transform grabbedObjectAttachPoint;
        protected bool previousKinematicState;

        protected override void Awake()
        {
            base.Awake();


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

        // Due to Oculus frame work
        // I have to override their grab and call my own custom function that can be inherited
        // can't virtual an override function already
        public override void GrabBegin(OVRGrabber grabbedBy, Collider grabPoint)
        {
            // Base GrabBegin
            // Sets the grabbedBy
            // Stores the collider
            // Set the rigidbody to kinematic
            base.GrabBegin(grabbedBy, grabPoint);

            if (grabbedObject == null)
            {
                grabbedObject = this.gameObject;
                grabbedObjectRB = this.gameObject.GetComponent<Rigidbody>();
                previousKinematicState = grabbedObjectRB.isKinematic;

                // Checks if
                grabbedObjectRB.isKinematic = (forceKinematics ? true : previousKinematicState);
            }

            if (controllerAttachPoint == null)
            {
                // Store the rigid bodyof the controller as reference
                controllerAttachPoint = grabbedBy.GetComponent<Rigidbody>();
            }

            CustomGrabBegin(grabbedBy, grabPoint);
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(Vector3.zero, Vector3.zero);
            controllerAttachPoint = null;
            grabbedObject = null;

            if (grabbedObjectRB != null)
            {
                grabbedObjectRB.isKinematic = previousKinematicState;
            }

            CustomGrabEnd(linearVelocity, angularVelocity);
        }

        protected virtual bool CustomGrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            return true;
        }

        protected virtual bool CustomGrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            return true;
        }

        /// <summary>
        /// Gets the transform value 
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        protected virtual Vector3 AxisDirection(bool local = false)
        {
            return VRControllable_Methods.AxisDirection((int)operateAxis, (local ? transform : null));
        }

    }
}
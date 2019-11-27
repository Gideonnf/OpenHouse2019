namespace VRControllables.Base
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

        protected virtual Vector3 AxisDirection(bool local = false)
        {
            return VRControllable_Methods.AxisDirection((int)operateAxis, (local ? transform : null));
        }



    }
}
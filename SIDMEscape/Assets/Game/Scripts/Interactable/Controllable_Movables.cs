namespace VRControllables.Base
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Controllable_Movables : BaseControllable
    {
        [Header("Movable Limitations")]
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

        protected virtual void ProcessUpdate() {}

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

        protected override void UpdateControllable()
        {

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
    }
}
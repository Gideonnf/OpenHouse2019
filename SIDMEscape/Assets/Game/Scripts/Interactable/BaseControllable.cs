using UnityEngine;
using System.Collections;

namespace VRControllables.Base
{
    /// <summary>
    /// I'm trying to create something similar to VRTK interactables
    /// Creating a bse interactable class that needs be inherited
    /// 
    /// </summary>
    public abstract class BaseControllable : OVRGrabbable
    {
        // The axis that it can operate on
        public enum OperatingAxis
        {
            xAxis,
            yAxis,
            zAxis
        }

        [Header("Controllable Settings")]
        [Tooltip("The axis that it'll operate on")]
        public OperatingAxis operateAxis = OperatingAxis.xAxis;
        [Tooltip("List of Game Objects to ignore collision against")]
        public GameObject[] ignoreCollisionsWith = new GameObject[0];
        [Tooltip("The amount of fidelity when comparing positions")]
        public float positionFidelity = 0.001f;
        [Tooltip("If the object is locked from being grabbed initially")]
        public bool isLocked = false;

        protected Vector3 originalPosition;
        protected Quaternion originalRotation;

        protected bool maxLimitReached;
        protected bool minLimitReached;

        protected OVRGrabber handGrabber;

        protected Coroutine processAtEndFrame;

        protected virtual void Awake()
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
        }

        protected virtual void OnEnable()
        {
            maxLimitReached = false;
            minLimitReached = false;
             
            processAtEndFrame = StartCoroutine(EndFrameProcess());
        }

        protected virtual void OnDisable()
        {
            if(processAtEndFrame != null)
            {
                StopCoroutine(processAtEndFrame);
            }
        }

        protected virtual IEnumerator EndFrameProcess()
        {
            yield return new WaitForEndOfFrame();
            //UpdateControllable();
            processAtEndFrame = null;
        }
        
        /// <summary>
        /// Returns the original position oif the object
        /// </summary>
        public Vector3 getOriginalPos()
        {
            return originalPosition;
        }

        /// <summary>
        /// Returns the original rotation of the object
        /// </summary>
        /// <returns></returns>
        public Quaternion getOriginalRot()
        {
            return originalRotation;
        }




        #region COLLISION_CHECKING

        protected virtual void OnCollisionEnter(Collision collision)
        {
            OnTouched(collision.collider);
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            OnUntouched(collision.collider);
        }

        protected virtual void OnTriggerEnter(Collider collider)
        {
            OnTouched(collider);
        }

        protected virtual void OnTriggerExit(Collider collider)
        {
            OnUntouched(collider);
        }

        protected virtual void OnTouched(Collider collider)
        {
            if(collider.gameObject.GetComponent<OVRGrabber>())
                handGrabber = collider.gameObject.GetComponent<OVRGrabber>();
        }

        protected virtual void OnUntouched(Collider collider)
        {
            handGrabber = null;
        }

        #endregion
    }
}

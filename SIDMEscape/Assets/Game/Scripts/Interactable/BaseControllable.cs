using UnityEngine;
using System.Collections;

namespace VRControllables
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
        

        protected Vector3 originalPosition;
        protected Quaternion originalRotation;

        protected bool maxLimitReached;
        protected bool minLimitReached;

        protected Coroutine processAtEndFrame;

        protected abstract void UpdateControllable();

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

        protected virtual IEnumerator EndFrameProcess()
        {
            yield return new WaitForEndOfFrame();
            UpdateControllable();
            processAtEndFrame = null;
        }

        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
        }
    }
}

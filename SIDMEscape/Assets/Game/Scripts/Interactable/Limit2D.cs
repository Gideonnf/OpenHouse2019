namespace VRControllables
{
    using UnityEngine;

    [System.Serializable]
    public class Limit2D
    {
        public string name;
        public float maximum;
        public float minimum;

        public static Limit2D zero
        {
            get
            {
                return new Limit2D(0, 0);
            }
        }

        public Limit2D(float _max, float _min)
        {
            maximum = _max;
            minimum = _min;
        }

        public Limit2D(Vector2 limit)
        {
            maximum = limit.x;
            minimum = limit.y;
        }

        public bool CheckMinimumLimits(float value)
        {
            return (value <= minimum);
        }

        public bool CheckMaximumLimits(float value)
        {
            return (value >= maximum);
        }

        public bool WithinLimits(float value)
        {
            Debug.Log("Rotation Value: " + value);
            Debug.Log("Minimum Value: " + minimum);
            Debug.Log("Maximum Value : " + maximum);
            return(value >= minimum && value <= maximum);
        }
    }
}


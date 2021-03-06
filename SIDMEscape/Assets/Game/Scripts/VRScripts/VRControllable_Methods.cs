﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRControllables
{
    public static class VRControllable_Methods
    {
        /// <summary>
        /// The Mod method is used to find the remainder of the sum a/b.
        /// </summary>
        /// <param name="a">The dividend value.</param>
        /// <param name="b">The divisor value.</param>
        /// <returns>The remainder value.</returns>
        public static float Mod(float a, float b)
        {
            return a - b * Mathf.Floor(a / b);
        }

        /// <summary>
        /// The DividerToMultiplier method takes a number to be used in a division and converts it to be used for multiplication. (e.g. 2 / 2 becomes 2 * 0.5)
        /// </summary>
        /// <param name="value">The number to convert into the multplier value.</param>
        /// <returns>The calculated number that can replace the divider number in a multiplication sum.</returns>
        public static float DividerToMultiplier(float value)
        {
            return (value != 0f ? 1f / value : 1f);
        }

        /// <summary>
        /// The VectorDirection method calculates the direction the target position is in relation to the origin position.
        /// </summary>
        /// <param name="originPosition">The point to use as the originating position for the direction calculation.</param>
        /// <param name="targetPosition">The point to use as the target position for the direction calculation.</param>
        /// <returns>A Vector3 containing the direction of the target position in relation to the origin position.</returns>
        public static Vector3 VectorDirection(Vector3 originPosition, Vector3 targetPosition)
        {
            Vector3 heading = VectorHeading(originPosition, targetPosition);
            return heading * DividerToMultiplier(heading.magnitude);
        }

        /// <summary>
        /// The VectorHeading method calculates the current heading of the target position in relation to the origin position.
        /// </summary>
        /// <param name="originPosition">The point to use as the originating position for the heading calculation.</param>
        /// <param name="targetPosition">The point to use as the target position for the heading calculation.</param>
        /// <returns>A Vector3 containing the heading changes of the target position in relation to the origin position.</returns>
        public static Vector3 VectorHeading(Vector3 originPosition, Vector3 targetPosition)
        {
            return targetPosition - originPosition;
        }

        /// <summary>
        /// The NormalizeValue method takes a given value between a specified range and returns the normalized value between 0f and 1f.
        /// </summary>
        /// <param name="value">The actual value to normalize.</param>
        /// <param name="minValue">The minimum value the actual value can be.</param>
        /// <param name="maxValue">The maximum value the actual value can be.</param>
        /// <param name="threshold">The threshold to force to the minimum or maximum value if the normalized value is within the threhold limits.</param>
        /// <returns></returns>
        public static float NormalizeValue(float value, float minValue, float maxValue, float threshold = 0f)
        {
            float normalizedMax = maxValue - minValue;
            float normalizedValue = normalizedMax - (maxValue - value);
            float result = normalizedValue * DividerToMultiplier(normalizedMax); ;
            result = (result < threshold ? 0f : result);
            result = (result > 1f - threshold ? 1f : result);
            return Mathf.Clamp(result, 0f, 1f);
        }

        /// <summary>
        /// The AxisDirection method returns the relevant direction Vector3 based on the axis index in relation to x,y,z.
        /// </summary>
        /// <param name="axisIndex">The axis index of the axis. `0 = x` `1 = y` `2 = z`</param>
        /// <param name="givenTransform">An optional Transform to get the Axis Direction for. If this is `null` then the World directions will be used.</param>
        /// <returns>The direction Vector3 based on the given axis index.</returns>
        public static Vector3 AxisDirection(int axisIndex, Transform givenTransform = null)
        {
            Vector3[] worldDirections = (givenTransform != null ? new Vector3[] { givenTransform.right, givenTransform.up, givenTransform.forward } : new Vector3[] { Vector3.right, Vector3.up, Vector3.forward });
            return worldDirections[(int)Mathf.Clamp(axisIndex, 0f, worldDirections.Length)];
        }

        /// <summary>
        /// The Vector3ShallowCompare method compares two given Vector3 objects based on the given threshold, which is the equavelent of checking the distance between two Vector3 objects are above the threshold distance.
        /// </summary>
        /// <param name="vectorA">The Vector3 to compare against.</param>
        /// <param name="vectorB">The Vector3 to compare with</param>
        /// <param name="threshold">The distance in which the two Vector3 objects can be within to be considered true</param>
        /// <returns>Returns `true` if the given Vector3 objects are within the given threshold distance.</returns>
        public static bool Vector3ShallowCompare(Vector3 vectorA, Vector3 vectorB, float threshold)
        {
            return (Vector3.Distance(vectorA, vectorB) < threshold);
        }

        #region FunctionsToFindGameObjects

        /// <summary>
        /// Finds the first GameObject with a given name and an ancestor that has a specific component.
        /// </summary>
        /// <remarks>
        /// This method returns active as well as inactive GameObjects in all scenes. It doesn't return assets.
        /// For performance reasons it is recommended to not use this function every frame. Cache the result in a member variable at startup instead.
        /// </remarks>
        /// <typeparam name="T">The component type that needs to be on an ancestor of the wanted GameObject. Must be a subclass of `Component`.</typeparam>
        /// <param name="gameObjectName">The name of the wanted GameObject. If it contains a '/' character, this method traverses the hierarchy like a path name, beginning on the game object that has a component of type `T`.</param>
        /// <param name="searchAllScenes">If this is true, all loaded scenes will be searched. If this is false, only the active scene will be searched.</param>
        /// <returns>The GameObject with name `gameObjectName` and an ancestor that has a `T`. If no such GameObject is found then `null` is returned.</returns>
        public static GameObject FindEvenInactiveGameObject<T>(string gameObjectName = null, bool searchAllScenes = false) where T : Component
        {
            if (string.IsNullOrEmpty(gameObjectName))
            {
                T foundComponent = FindEvenInactiveComponentsInValidScenes<T>(searchAllScenes, true).FirstOrDefault();
                return foundComponent == null ? null : foundComponent.gameObject;
            }

            return FindEvenInactiveComponentsInValidScenes<T>(searchAllScenes)
                       .Select(component =>
                       {
                           Transform transform = component.gameObject.transform.Find(gameObjectName);
                           return transform == null ? null : transform.gameObject;
                       })
                       .FirstOrDefault(gameObject => gameObject != null);
        }


        /// <summary>
        /// The FindEvenInactiveComponentsInLoadedScenes method searches active and inactive game objects in all
        /// loaded scenes for components matching the type supplied. 
        /// </summary>
        /// <param name="searchAllScenes">If true, will search all loaded scenes, otherwise just the active scene.</param>
        /// <param name="stopOnMatch">If true, will stop searching objects as soon as a match is found.</param>
        /// <returns></returns>
        private static IEnumerable<T> FindEvenInactiveComponentsInValidScenes<T>(bool searchAllScenes, bool stopOnMatch = false) where T : Component
        {
            IEnumerable<T> results;
            if (searchAllScenes)
            {
                List<T> allSceneResults = new List<T>();
                for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
                {
                    allSceneResults.AddRange(FindEvenInactiveComponentsInScene<T>(SceneManager.GetSceneAt(sceneIndex), stopOnMatch));
                }
                results = allSceneResults;
            }
            else
            {
                results = FindEvenInactiveComponentsInScene<T>(SceneManager.GetActiveScene(), stopOnMatch);
            }

            return results;
        }

        /// <summary>
        /// The FIndEvenInactiveComponentsInScene method searches the specified scene for components matching the type supplied.
        /// </summary>
        /// <param name="scene">The scene to search. This scene must be valid, either loaded or loading.</param>
        /// <param name="stopOnMatch">If true, will stop searching objects as soon as a match is found.</param>
        /// <returns></returns>
        private static IEnumerable<T> FindEvenInactiveComponentsInScene<T>(Scene scene, bool stopOnMatch = false)
        {
            List<T> results = new List<T>();
            if (!scene.isLoaded)
            {
                return results;
            }

            foreach (GameObject rootObject in scene.GetRootGameObjects())
            {
                if (stopOnMatch)
                {
                    T foundComponent = rootObject.GetComponentInChildren<T>(true);
                    if (foundComponent != null)
                    {
                        results.Add(foundComponent);
                        return results;
                    }
                }
                else
                {
                    results.AddRange(rootObject.GetComponentsInChildren<T>(true));
                }
            }

            return results;
        }
        #endregion

    }
}

using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;

namespace _Scripts.Utils
{
    /// <summary>
    /// Vector Extensions to assist with quick Vector2 and Vector3 Manipulations
    /// </summary>
    [BurstCompile]
    public static class VectorExtensions
    {
        public static Vector3 XZPlane(this Vector3 vec, float y = 0) => new Vector3(vec.x, y, vec.z);

        public static Vector3 YZPlane(this Vector3 vec) => new Vector3(0, vec.y, vec.z);

        public static Vector3 XYPlane(this Vector3 vec) => new Vector3(vec.x, vec.y, 0);

        public static Vector3 SetX(this Vector3 vec, float x) => new Vector3(x, vec.y, vec.z);

        public static Vector3 SetY(this Vector3 vec, float y) => new Vector3(vec.x, y, vec.z);

        public static Vector3 SetZ(this Vector3 vec, float z) => new Vector3(vec.x, vec.y, z);

        public static float BiasedDistance(Vector3 firstVector, Vector3 secondVector, float wx = 1f, float wy = 1f, float wz = 1f)
        {
            float dx = (firstVector.x - secondVector.x) * wx;
            float dy = (firstVector.y - secondVector.y) * wy;
            float dz = (firstVector.z - secondVector.z) * wz;

            return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static float BiasedDistance(Vector2 firstVector, Vector2 secondVector, float wx = 1f, float wy = 1f)
        {
            float dx = (firstVector.x - secondVector.x) * wx;
            float dy = (firstVector.y - secondVector.y) * wy;

            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        [BurstCompile]
        public static float BiasedDistanceSqr(float firstX, float firstY, float firstZ, float secondX, float secondY, float secondZ, float wx = 1f, float wy = 1f, float wz = 1f)
        {
            float dx = (firstX - secondX) * wx;
            float dy = (firstY - secondY) * wy;
            float dz = (firstZ - secondZ) * wy;

            return dx * dx + dy * dy + dz * dz;
        }
    }
}

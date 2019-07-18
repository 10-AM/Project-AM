using System;
using UnityEngine;

namespace AM.Util
{
    public static class SwingbyUtil
    {
        public static bool IsInDistance(this Vector3 v, float distance)
        {
            return v.sqrMagnitude <= distance * distance;
        }
    }
}

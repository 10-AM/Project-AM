using AM.SwingBy.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace AM.SwingBy.Core.Managers
{
    public static class GravityObjectManager
    {
        private static List<IGravityObject> gravityObjectList = null;

        public static int GravityObjectCount
        {
            get => gravityObjectList.Count;
        }
        static GravityObjectManager()
        {
            gravityObjectList = new List<IGravityObject>();
        }

        public static void Add(IGravityObject gravityObject)
        {
            gravityObjectList.Add(gravityObject);
        }

        public static IGravityObject Get(int idx)
        {
            if (gravityObjectList.Count > idx && idx >= 0)
            {
                return gravityObjectList[idx];
            }
            return null;
        }

        public static void Remove(IGravityObject gravityObject)
        {
            gravityObjectList.Remove(gravityObject);
        }
    }
}

using AM.SwingBy.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace AM.SwingBy.Core.Managers
{
    public static class PlanetManager
    {
        private static List<IPlanet> planetList = null;

        public static int PlanetCount
        {
            get => planetList.Count;
        }

        static PlanetManager()
        {
            planetList = new List<IPlanet>();
        }

        public static void Add(IPlanet obj)
        {
            planetList.Add(obj);
        }

        public static IPlanet Get(int idx)
        {
            if (planetList.Count > idx && idx >= 0)
            {
                return planetList[idx];
            }

            return null;
        }

        public static void Remove(IPlanet obj)
        {
            planetList.Remove(obj);
        }
    }
}

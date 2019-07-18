using AM.SwingBy.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM.SwingBy.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {
            get; private set;
        }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } 


        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {

        }

        /// <summary>
        /// 물리 업데이트
        /// </summary>
        private void FixedUpdate()
        {
            for (int planetIdx = 0; planetIdx < PlanetManager.PlanetCount; ++planetIdx)
            {
                var planet = PlanetManager.Get(planetIdx);
                for (int i = 0; i < GravityObjectManager.GravityObjectCount; ++i)
                {
                    var gravityObject = GravityObjectManager.Get(i);
                    if (!object.ReferenceEquals(gravityObject, null))
                    {
                        gravityObject.UpdatePhysics();
                        planet.UpdateGravity(gravityObject);
                    }
                }
            }
        }
    }
}

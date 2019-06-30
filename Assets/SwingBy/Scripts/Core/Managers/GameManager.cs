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

        private void Update()
        {

        }

        /// <summary>
        /// 물리 업데이트
        /// </summary>
        private void FixedUpdate()
        {
            for (int i = 0; i < GravityObjectManager.GravityObjectCount; ++i)
            {
                GravityObjectManager.Get(i).UpdatePhysics();
            }
        }
    }
}

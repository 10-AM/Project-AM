using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM.SwingBy.Core.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "Create Environment Data", order = 0)]
    public class EnvironmentData : ScriptableObject
    {
        /// <summary>
        /// 마찰력
        /// 우주 공간이라 굉장히 적은 마찰력을 가진다.
        /// </summary>
        public float Friction = 0.003f;
    }
}

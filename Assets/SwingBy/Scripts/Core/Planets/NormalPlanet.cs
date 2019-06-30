using AM.SwingBy.Core.Interfaces;
using UnityEngine;
using AM.Common.Event;
using UnityEditor;
using AM.Util;
using AM.SwingBy.Core.Managers;

namespace AM.SwingBy.Core.Planets
{
    public class NormalPlanet : MonoBehaviour, IPlanet
    {

        // TODO : NormalPlanet에만 있을지 공통으로 뺄지 고민 필요

        /// <summary>
        /// 중력 영향 범위
        /// </summary>
        public float GravityRange = 0f;

        /// <summary>
        /// 중력 세기
        /// </summary>
        public float GravityPower = 0f;

        /// <summary>
        /// 행성의 중심
        /// </summary>
        public Vector3 Center = Vector3.zero;

        /// <summary>
        /// 행성 자전 방향
        /// </summary>
        public Vector3 RotateDirection = Vector3.zero;

        /// <summary>
        /// 행성 자전 속도
        /// </summary>
        public float RotationSpeed = 0f;

        /// <summary>
        /// 행성의 반지름... 필요할까?
        /// </summary>
        private float planetRange = 0f;

        public void Initialize()
        {
            Center = transform.position;
        }

        private void OnEnable()
        {
            PlanetManager.Add(this);
        }

        private void OnDisable()
        {
            PlanetManager.Remove(this);
        }

        public void UpdateGravity(IGravityObject gravityObject)
        {
            var gravityObjToPlanet = (transform.position - gravityObject.CurrentPosition);
            // 중력 영향 범위 내에 gravityObject가 있다면
            if (gravityObjToPlanet.IsInDistance(GravityRange))
            {
                
            }
        }
    }
}

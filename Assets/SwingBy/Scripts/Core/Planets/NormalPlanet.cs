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
        /// 가장 바깥에 있을 때 중력 세기
        /// </summary>
        public float MinGravityPower = 0f;

        /// <summary>
        /// 행성의 중심 Pivot
        /// </summary>
        public Vector3 CenterPivot = Vector3.zero;

        /// <summary>
        /// 행성의 중심
        /// </summary>
        public Vector3 Center
        {
            get => transform.position + CenterPivot;
        }

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

        }

        private void OnEnable()
        {
            PlanetManager.Add(this);
        }

        private void OnDisable()
        {
            PlanetManager.Remove(this);
        }

        private void OnDrawGizmos()
        {
            DebugExtension.DebugCircle(Center, -transform.forward, Color.red, GravityRange);
        }

        public void UpdateGravity(IGravityObject gravityObject)
        {
            if (object.ReferenceEquals(gravityObject, null))
            {
                return;
            }
            var gravityObjToPlanet = (Center - gravityObject.CurrentPosition);
            // 중력 영향 범위 내에 gravityObject가 있다면
            if (gravityObjToPlanet.IsInDistance(GravityRange))
            {
                var t = (gravityObjToPlanet.magnitude / GravityRange);
                // 점점 행성 Center 방향으로 방향을 바꿔주면서 속력도 늘려준다.
                var dir = gravityObjToPlanet.normalized * Mathf.Lerp(GravityPower, MinGravityPower, t);
                // gravity direcion을 인자로 넘겨준다.
                gravityObject.UpdatePhysics(dir);
            }
        }
    }
}

using UnityEngine;

namespace AM.SwingBy.Core.Interfaces
{
    /// <summary>
    /// 중력 영향을 받는 오브젝트들이 상속받는 인터페이스
    /// </summary>
    public interface IGravityObject
    {
        
        Vector3 CurrentPosition { get; }
        
        /// <summary>
        /// 물리 업데이트
        /// </summary>
        void UpdatePhysics();
        
        /// <summary>
        /// 행성에 의한 중력 업데이트
        /// </summary>
        void UpdateGravity();
    }
}

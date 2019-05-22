namespace AM.SwingBy.Core.Interfaces
{
    /// <summary>
    /// 행성들이 상속받는 인터페이스
    /// 대부분 똑같은 동작을 하겠지만
    /// 혹시 다른 타입의 행성을 넣고 싶을 때를 위해 생성
    /// </summary>
    public interface IPlanet
    {
        void Initialize();
        void UpdateGravity(IGravityObject gravityObject);
    }
}

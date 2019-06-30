using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipData", menuName = "Create SpaceShip Data", order = 0)]
public class SpaceShipData : ScriptableObject
{
    /// <summary>
    /// 연료 량
    /// </summary>
    public float Fuel = 100f;
    /// <summary>
    /// 연료 1을 써서 가속할 수 있는 정도
    /// </summary>
    public float Acceleration = 10f;
    

}

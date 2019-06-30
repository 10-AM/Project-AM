using AM.SwingBy.Core.Interfaces;
using AM.SwingBy.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM.SwingBy.Core.Player
{
    public class SpaceShip : PlayerBase, IGravityObject
    {
        public Vector3 CurrentPosition
        {
            get => transform.position;
        }

        public SpaceShipData Data;

        private Rigidbody2D rig2D = null;

        private void Awake()
        {
            rig2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            GravityObjectManager.Add(this);
        }

        private void OnDisable()
        {
            GravityObjectManager.Remove(this);
        }

        public void UpdateGravity(Vector3 externalGravity)
        {
            
        }

        public void UpdatePhysics()
        {
            
        }
    }
}

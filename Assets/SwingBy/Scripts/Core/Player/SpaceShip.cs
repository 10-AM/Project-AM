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
        public Environment.EnvironmentData EnvData;

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

        public void UpdatePhysics(Vector2 externalGravity)
        {
            var currVel = rig2D.velocity;
            DebugExtension.DebugArrow(transform.position, currVel, Color.blue);
            DebugExtension.DebugArrow(transform.position, externalGravity, Color.red);
            var newDir = currVel + externalGravity;
            rig2D.AddForce(externalGravity, ForceMode2D.Impulse);
            DebugExtension.DebugArrow(transform.position, newDir, Color.cyan);
        }

        public void UpdatePhysics()
        {
            rig2D.AddForce(-rig2D.velocity.normalized * EnvData.Friction, ForceMode2D.Force);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                rig2D.AddForce(transform.up * Data.Acceleration, ForceMode2D.Force);
            }

            //transform.LookAt(rig2D.velocity, transform.up);
        }
    }
}

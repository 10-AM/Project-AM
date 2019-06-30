using AM.Common.Event;
using AM.SwingBy.Event;
using System;
using UnityEngine;

namespace AM.SwingBy.Core.Player
{
    public enum PlayerStateType
    {
        Ready,
        Play,
        Dead
    }
    public class PlayerBase : MonoBehaviour
    {
        private PlayerStateType playerState;

        // Event 돌려쓰기
        private EventPlayerStateChanged stateChangeEvent = null;

        public PlayerStateType PlayerState
        {
            get => playerState;
            set
            {
                if (playerState != value)
                {
                    playerState = value;

                    if (stateChangeEvent == null)
                    {
                        stateChangeEvent = new EventPlayerStateChanged();
                    }

                    stateChangeEvent.ChangedType = value;

                    EventDispatcher.Send(stateChangeEvent);
                }
            }
        }
    }
}

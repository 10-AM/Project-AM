using System;
using AM.Common.Event;
using AM.SwingBy.Core.Player;

namespace AM.SwingBy.Event
{
    public class EventPlayerStateChanged : IEvent
    {
        public PlayerStateType ChangedType;

        public EventPlayerStateChanged() { }

        public EventPlayerStateChanged(PlayerStateType type)
        {
            ChangedType = type;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AM.Common.Event
{
    /// <summary>
    /// Event들이 상속받는 인터페이스
    /// </summary>
    public interface IEvent {}
    
    /// <summary>
    /// Event 관리자
    /// </summary>
    public class EventDispatcher
    {
        /// <summary>
        /// EventHandler Delegate
        /// </summary>
        /// <param name="evt">뿌려질 Event Type</param>
        /// <typeparam name="T">Event Type</typeparam>
        public delegate void EventHandler<T>(T evt) where T : IEvent;
        
        private static Dictionary<Type, List<Delegate>> handlerDict = new Dictionary<Type, List<Delegate>>();

        /// <summary>
        /// Handler Dictionary중에서 Type에 맞는 HandlerList를 뽑아온다
        /// Type이 Dictionary에 없다면 추가한다.
        /// </summary>
        /// <typeparam name="T">Event Type</typeparam>
        /// <returns>Handler List</returns>
        private static List<Delegate> GetHandlerList<T>() where T : IEvent
        {
            var type = typeof(T);
            
            handlerDict.TryGetValue(type, out List<Delegate> handlerList);

            if (handlerList == null)
            {
                handlerDict.Add(type, new List<Delegate>());
            }

            return handlerList;
        }
        /// <summary>
        /// 리스너 추가
        /// </summary>
        /// <param name="handler">handler function</param>
        /// <typeparam name="T">Event Type</typeparam>
        public static void Listen<T>(EventHandler<T> handler) where T : IEvent
        {
            var handlerList = GetHandlerList<T>();
            handlerList.Add(handler);
        }

        public static void Remove<T>(EventHandler<T> handler) where T : IEvent
        {
            var handlerList = GetHandlerList<T>();
            handlerList.Remove(handler);
        }

        public static void Send<T>(T evt) where T : IEvent
        {
            handlerDict.TryGetValue(typeof(T), out List<Delegate> handlerList);

            Debug.Assert(handlerList != null, $"There is no listener of {nameof(T)}");

            foreach (var handler in handlerList)
            {
                (handler as EventHandler<T>)?.Invoke(evt);
            }
        }
    }
}

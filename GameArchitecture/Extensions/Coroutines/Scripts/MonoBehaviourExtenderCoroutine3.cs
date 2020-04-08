using System;
using UnityEngine;
using System.Collections;

namespace homehelp.Extenders
{
    public static class MonoBehaviourExtenderCoroutine3
    {
        public static Coroutine WaitToCall<T1, T2, T3>(this MonoBehaviour monoBehaviour, float duration, 
            T1 value1, T2 value2, T3 value3,
            Action<T1, T2, T3> callbackAfter = null)
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                new WaitForSeconds(duration), value1, value2, value3, callbackAfter));
        }

        public static Coroutine WaitToCall<TY, T1, T2, T3>(this MonoBehaviour monoBehaviour, TY yieldInstruction, 
            T1 value1, T2 value2, T3 value3,
            Action<T1, T2, T3> callbackAfter = null) where TY : YieldInstruction
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                yieldInstruction, value1, value2, value3, callbackAfter));
        }

        private static IEnumerator WaitToCallCoroutine<T1, T2, T3>(YieldInstruction yieldInstruction, 
            T1 value1, T2 value2, T3 value3,
            Action<T1, T2, T3> callbackAfter)
        {
            yield return yieldInstruction;
            callbackAfter?.Invoke(value1, value2, value3);
        }
    }
}

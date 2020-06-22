using System;
using UnityEngine;
using System.Collections;

namespace homehelp.Extenders
{
    public static class MonoBehaviourExtenderCoroutine2 
    {
        public static Coroutine WaitToCall<T1, T2>(this MonoBehaviour monoBehaviour, float duration, 
            T1 value1, T2 value2,
            Action<T1, T2> callbackAfter = null)
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                new WaitForSeconds(duration), value1, value2, callbackAfter));
        }

        public static Coroutine WaitToCall<TY, T1, T2>(this MonoBehaviour monoBehaviour, TY yieldInstruction, 
            T1 value1, T2 value2,
            Action<T1, T2> callbackAfter = null) where TY : YieldInstruction
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                yieldInstruction, value1, value2, callbackAfter));
        }

        private static IEnumerator WaitToCallCoroutine<T1, T2>(YieldInstruction yieldInstruction,
            T1 value1, T2 value2,
            Action<T1, T2> callbackAfter)
        {
            yield return yieldInstruction;
            callbackAfter?.Invoke(value1, value2);
        }
    }
}

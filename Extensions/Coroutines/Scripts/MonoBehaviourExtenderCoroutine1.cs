using System;
using UnityEngine;
using System.Collections;

namespace homehelp.Extenders
{
    public static class MonoBehaviourExtenderCoroutine1
    { 
        public static Coroutine WaitToCall<T1>(this MonoBehaviour monoBehaviour, float duration, 
            T1 value1, 
            Action<T1> callbackAfter = null)
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                new WaitForSeconds(duration), value1, callbackAfter));
        }

        public static Coroutine WaitToCall<TY, T1>(this MonoBehaviour monoBehaviour, TY yieldInstruction, 
            T1 value1, 
            Action<T1> callbackAfter = null) where TY : YieldInstruction
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                yieldInstruction, value1, callbackAfter));
        }

        private static IEnumerator WaitToCallCoroutine<T1>(YieldInstruction yieldInstruction,
            T1 value1,
            Action<T1> callbackAfter)
        {
            yield return yieldInstruction;
            callbackAfter?.Invoke(value1);
        }
    }
}

using System;
using UnityEngine;
using System.Collections;

namespace homehelp.Extenders
{
    public static class MonoBehaviourExtenderCoroutine
    { 
        public static Coroutine WaitToCall(this MonoBehaviour monoBehaviour, float duration,
            Action callbackAfter)
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                new WaitForSeconds(duration), callbackAfter));
        }

        public static Coroutine WaitToCall<TY>(this MonoBehaviour monoBehaviour, TY yieldInstruction, 
            Action callbackAfter) where TY : YieldInstruction
        {
            return monoBehaviour.StartCoroutine(WaitToCallCoroutine(
                yieldInstruction, callbackAfter));
        }
        
        private static IEnumerator WaitToCallCoroutine(YieldInstruction yieldInstruction,
            Action callbackAfter)
        {
            yield return yieldInstruction;
            callbackAfter?.Invoke();
        }
    }
}

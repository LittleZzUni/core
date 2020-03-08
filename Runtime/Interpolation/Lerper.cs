using System;
using System.Collections;
using UnityEngine;

namespace JasonStorey
{
    public class Lerper
    {
        private readonly MonoBehaviour _owner;
        private readonly float _duration;
        private readonly AnimationCurve _curve;

        public Lerper(MonoBehaviour owner,float duration = 1, AnimationCurve curve = null)
        {
            _owner = owner;
            _duration = duration;
            _curve = curve ?? AnimationCurve.Linear(0,0,1,1);
        }

        public IProperty Property { get; set; }

        IEnumerator Co_Lerp(float duration,float startingPercent,Action complete,bool invert = false)
        {
            var timer = invert ? 1-startingPercent : startingPercent;
            while (timer < 1)
            {
                timer += UnityEngine.Time.deltaTime / duration;
                Property.Value = invert ? 1 - _curve.Evaluate(timer) : _curve.Evaluate(timer);
                yield return null;
            }
            if(complete != null) complete();
        }

        private IEnumerator _current;

        public void Lerp(Action complete = null)
        {
            StartRoutine(complete);
        }

        public void UnLerp(Action complete = null)
        {
            StartRoutine(complete,invert:true);
        }

        void StartRoutine(Action complete,bool invert = false)
        {
            if (_current != null) _owner.StopCoroutine(_current);
            _current = Co_Lerp(_duration, Property.Value, complete,invert);
            _owner.StartCoroutine(_current);
        }

    }

    public interface IProperty
    {
        float Value { get; set; }
    }
}
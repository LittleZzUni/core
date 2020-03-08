using UnityEngine;

namespace JasonStorey
{
    public class TransformLookRay : ACameraRayProvider
    {
        private readonly Transform _transform;

        public TransformLookRay(Camera cam,Transform transform) : base(cam) => _transform = transform;

        public override Ray New => new Ray(_transform.position,_transform.forward);
    }
}
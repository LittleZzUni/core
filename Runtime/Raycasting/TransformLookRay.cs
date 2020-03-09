using UnityEngine;

namespace JasonStorey
{
    public class TransformLookRay : RayProvider
    {
        private readonly Transform _transform;

        public TransformLookRay(Transform transform) => _transform = transform;

        public Ray New => new Ray(_transform.position,_transform.forward);
    }
}
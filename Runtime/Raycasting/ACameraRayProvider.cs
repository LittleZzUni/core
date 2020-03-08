using UnityEngine;

namespace JasonStorey
{
    public abstract class ACameraRayProvider : RayProvider
    {
        protected readonly Camera Cam;

        protected ACameraRayProvider(Camera cam) => Cam = cam;
        public abstract Ray New { get; }
    }
}
using UnityEngine;

namespace JasonStorey
{
    public class ViewportRay : ACameraRayProvider
    {
        public override Ray New => Cam.ViewportPointToRay(_point);

        public ViewportRay(Camera cam) : this(cam,new Vector3(0.5f,0.5f,0.5f)) { }

        public ViewportRay() : this(Camera.main,new Vector3(0.5f,0.5f,0.5f)) { }
        public ViewportRay(Vector3 point) : this(Camera.main,point) { }
        public ViewportRay(Camera cam,Vector3 point) : base(cam) => _point = point;
        private readonly Vector3 _point;
    }
}
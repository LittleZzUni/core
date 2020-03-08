using UnityEngine;

namespace JasonStorey
{
    public class MouseScreenRay : ACameraRayProvider
    {
        public override Ray New => Cam.ScreenPointToRay(Input.mousePosition);

        public MouseScreenRay(Camera cam) : base(cam) { }

        public MouseScreenRay() : this(Camera.main) { }
    }
}
using UnityEngine;

namespace JasonStorey
{
    public class SimpleRaycaster : Raycaster
    {
        public void Check() => HitSomething = Physics.Raycast(_ray, out _hit);

        public bool HitSomething { get; private set; }
        
        public RaycastHit Hit => _hit;
        public void SetRay(Ray ray) => _ray = ray;

        public void Check(Ray ray)
        {
            _ray = ray;
            Check();
        }

        public Transform Transform => _hit.transform;
        public GameObject Object => _hit.transform.gameObject;
        public Vector3 Point => _hit.point;
        public Vector3 Normal => _hit.normal;
        public Collider Collider => _hit.collider;
        
        private Ray _ray;
        private RaycastHit _hit;
    }
}
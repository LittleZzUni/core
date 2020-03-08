using UnityEngine;

namespace JasonStorey 
{
    public interface Caster<out T>
    {
        void Check();

        bool HitSomething { get; }

        T Hit { get; }
    }

    public interface Raycaster : Caster<RaycastHit>
    {
        void SetRay(Ray ray);

        void Check(Ray ray);
        
        Transform Transform { get; }
        
        GameObject Object { get; }
        
        Vector3 Point { get; }
        
        Vector3 Normal { get; }
        
        Collider Collider { get; }
    }
}
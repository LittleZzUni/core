using UnityEngine;

namespace JasonStorey
{
    public interface RayProvider
    {
        Ray New { get; }
    }
}
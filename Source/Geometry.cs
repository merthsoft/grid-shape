using System;
using System.Collections.Generic;
using Verse;

namespace Merthsoft.DesignatorShapes.Grids;

// Extracted from Merthsoft's Primitives.Grid/InverseGrid, specialized to thickness 1.
// Grid rows anchor at maximum Z; inverse interiors anchor at minimum Z and close at both ends.
public static class Geometry
{
    public static IEnumerable<IntVec3> Draw(IntVec3 s, IntVec3 t, int xs, int zs, bool inverse)
    {
        if (xs <= 0 || zs <= 0) yield break;
        int left = Math.Min(s.x,t.x), right = Math.Max(s.x,t.x);
        int bottom = Math.Min(s.z,t.z), top = Math.Max(s.z,t.z);
        for (int x = left; x <= right; x++)
            for (int z = bottom; z <= top; z++)
            {
                bool selected = inverse
                    ? x < right && z < top && (x-left)%xs != 0 && (z-bottom)%zs != 0
                    : (x-left)%xs == 0 || (top-z)%zs == 0;
                if (selected) yield return new IntVec3(x,s.y,z);
            }
    }
}

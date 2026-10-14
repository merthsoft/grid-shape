using System;
using System.Collections.Generic;
using System.Linq;
using Merthsoft.DesignatorShapes.Grids;
using Verse;

class Program
{
    static HashSet<IntVec3> Reference(IntVec3 s, IntVec3 t, int xs, int zs, bool inverse)
    {
        var result = new HashSet<IntVec3>();
        if(xs<=0 || zs<=0) return result;
        int x0=Math.Min(s.x,t.x), x1=Math.Max(s.x,t.x), z0=Math.Min(s.z,t.z), z1=Math.Max(s.z,t.z);
        if(!inverse) {
            for(int x=x0;x<=x1;x+=xs) for(int z=z0;z<=z1;z++) result.Add(new(x,s.y,z));
            for(int z=z1;z>=z0;z-=zs) for(int x=x0;x<=x1;x++) result.Add(new(x,s.y,z));
        } else {
            // Original InverseGrid's partition/closing-boundary algorithm.
            if(xs<=1 || zs<=1) return result;
            var xx=new List<int>(); var zz=new List<int>();
            for(int x=x0;x<=x1;x+=xs) xx.Add(x);
            if(xx.Last()!=x1) xx.Add(x1);
            for(int z=z0;z<=z1;z+=zs) zz.Add(z);
            if(zz.Last()!=z1) zz.Add(z1);
            for(int i=0;i<xx.Count-1;i++) for(int j=0;j<zz.Count-1;j++)
                for(int x=xx[i]+1;x<xx[i+1];x++) for(int z=zz[j]+1;z<zz[j+1];z++)
                    result.Add(new(x,s.y,z));
        }
        return result;
    }
    static void Main()
    {
        int cases=0;
        foreach(int w in new[]{0,1,2,3,6,7,10,16})
        foreach(int h in new[]{0,1,2,4,6,9,15})
        foreach(int xs in new[]{-1,0,1,2,3,4,5,6,7,100})
        foreach(int zs in new[]{-1,0,1,3,4,5,6,7,100})
        foreach(bool inv in new[]{false,true})
        foreach(bool rx in new[]{false,true})
        foreach(bool rz in new[]{false,true}) {
            var s=new IntVec3(rx?w:0,0,rz?h:0);
            var t=new IntVec3(rx?0:w,0,rz?0:h);
            var actual=Geometry.Draw(s,t,xs,zs,inv).ToList();
            if(actual.Count!=actual.Distinct().Count() || !actual.ToHashSet().SetEquals(Reference(s,t,xs,zs,inv)))
                throw new Exception($"Mismatch {w},{h},{xs},{zs},{inv},{rx},{rz}");
            if(actual.Any(c=>c.x<0 || c.x>w || c.z<0 || c.z>h)) throw new Exception("Escaped map-edge rectangle");
            cases++;
        }
        var origin=new IntVec3(-3,4,-5); var target=new IntVec3(8,4,6);
        if(!Geometry.Draw(origin,target,3,7,true).ToHashSet().SetEquals(Reference(origin,target,3,7,true))) throw new Exception("Offset/y");
        // Deliberately asymmetric golden examples catch accidentally treating inverse as complement.
        var grid=Geometry.Draw(new(0,0,0),new(4,0,4),3,3,false).ToHashSet();
        var inverse=Geometry.Draw(new(0,0,0),new(4,0,4),3,3,true).ToHashSet();
        if(grid.Count!=16 || inverse.Count!=4 || !grid.Contains(new(2,0,1)) || !inverse.Contains(new(2,0,2)))
            throw new Exception("Golden fixture");
        Console.WriteLine($"PASS: {cases+2} geometry cases (presets, unequal/custom spacing, reversed drags, degenerate rectangles, edge containment, invalid spacing).");
    }
}
namespace Verse
{
    public readonly record struct IntVec3(int x,int y,int z);
}

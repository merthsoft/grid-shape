using System.Collections.Generic;
using Verse;

namespace Merthsoft.DesignatorShapes.Grids;

public abstract class GridStyle : DrawStyle
{
    protected abstract int Spacing { get; }
    protected virtual bool Inverse => false;
    public override void Update(IntVec3 origin, IntVec3 target, List<IntVec3> buffer)
    {
        buffer.Clear();
        var xs = Spacing == 0 ? GridMod.Settings.X : Spacing;
        var zs = Spacing == 0 ? GridMod.Settings.Z : Spacing;
        buffer.AddRange(Geometry.Draw(origin, target, xs, zs, Inverse));
    }
}
public class Grid3 : GridStyle { protected override int Spacing => 3; protected override bool Inverse => false; }
public class Grid4 : GridStyle { protected override int Spacing => 4; protected override bool Inverse => false; }
public class Grid5 : GridStyle { protected override int Spacing => 5; protected override bool Inverse => false; }
public class Grid6 : GridStyle { protected override int Spacing => 6; protected override bool Inverse => false; }
public class Grid7 : GridStyle { protected override int Spacing => 7; protected override bool Inverse => false; }
public class Grid0 : GridStyle { protected override int Spacing => 0; protected override bool Inverse => false; }
public class InverseGrid3 : GridStyle { protected override int Spacing => 3; protected override bool Inverse => true; }
public class InverseGrid4 : GridStyle { protected override int Spacing => 4; protected override bool Inverse => true; }
public class InverseGrid5 : GridStyle { protected override int Spacing => 5; protected override bool Inverse => true; }
public class InverseGrid6 : GridStyle { protected override int Spacing => 6; protected override bool Inverse => true; }
public class InverseGrid7 : GridStyle { protected override int Spacing => 7; protected override bool Inverse => true; }
public class InverseGrid0 : GridStyle { protected override int Spacing => 0; protected override bool Inverse => true; }

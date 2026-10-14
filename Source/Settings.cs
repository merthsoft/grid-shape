using System;
using UnityEngine;
using Verse;

namespace Merthsoft.DesignatorShapes.Grids;

public class GridSettings : ModSettings
{
    public int X = 6, Z = 6;
    public void Clamp() { X = Math.Clamp(X, 1, 100); Z = Math.Clamp(Z, 1, 100); }
    public override void ExposeData()
    {
        Scribe_Values.Look(ref X, "xSpacing", 6);
        Scribe_Values.Look(ref Z, "zSpacing", 6);
        Clamp();
    }
}
public class GridMod : Mod
{
    public static GridSettings Settings = new();
    private string xBuffer, zBuffer;
    public GridMod(ModContentPack content) : base(content) { Settings = GetSettings<GridSettings>(); }
    public override string SettingsCategory() => "Merthsoft_Grids_Title".Translate();
    public override void DoSettingsWindowContents(Rect rect)
    {
        var list = new Listing_Standard();
        list.Begin(rect);
        list.Label("Merthsoft_Grids_Help".Translate());
        list.Gap();
        Widgets.TextFieldNumericLabeled(list.GetRect(30f), "Merthsoft_Grids_X".Translate(), ref Settings.X, ref xBuffer, 1, 100);
        Widgets.TextFieldNumericLabeled(list.GetRect(30f), "Merthsoft_Grids_Z".Translate(), ref Settings.Z, ref zBuffer, 1, 100);
        Settings.Clamp();
        list.End();
    }
}

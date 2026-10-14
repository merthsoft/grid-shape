# Grids for RimWorld 1.7

Repository: [merthsoft/grid-shape](https://github.com/merthsoft/grid-shape)

Standalone extraction of Designator Shapes grid and inverse-grid tools. Select a supported build, mine, or zone designator, then select a grid style in the vanilla drawing palette. Presets 3–7 retain fixed equal spacing. Custom styles use independent X/Z spacing in Options → Mod Settings → Grids (1–100, default 6/6). Lines are always one cell thick.

Spacing means distance between lines, not interior size. Grid lines anchor at minimum X and maximum Z. Inverse-grid interiors anchor at minimum X/Z, with a closing boundary at maximum X/Z, exactly as the source primitives do. Inverse is not simply the complement of grid. At spacing 1 inverse produces no cells. Drag direction does not alter geometry. Degenerate grid rectangles produce lines/points; inverse produces no cells when either dimension is zero. Geometry stays within the drag rectangle; vanilla designator validation handles map bounds and unsuitable cells.

Full Designator Shapes is incompatible: its UpdateDragCellsIfNeeded prefix can suppress vanilla DrawStyle handling when its controls are shown. Other standalone vanilla DrawStyle mods can coexist; all definitions and icon paths here are namespaced. There is no Harmony dependency.

## Build and checks

Requires .NET 10 SDK and an installed RimWorld 1.7 root containing Assembly-CSharp.dll, UnityStubs.dll, Waylith.dll and Waylith.UnityCompat.dll:

    ./build-1.7.ps1 -RimWorldDir 'C:/Program Files (x86)/Steam/steamapps/common/RimWorld'
    dotnet run --project Tests/GeometryTests.csproj -c Release

Only the mod assembly belongs in 1.7/Assemblies. No game DLLs are distributed. No Workshop ID is assigned.

## Credits and license

Code by Merthsoft. Grid icons by Halno. Extracted from Designator Shapes (Source/Shapes/Grids.cs, InverseGrids.cs, Primitives.cs), specialized to one-cell thickness and adapted to the 1.7 vanilla DrawStyle API. Source: https://github.com/merthsoft/designator-shapes

Original project also credits Jacbo (freeform line), 祂 (Chinese translation), and Lisanhu (space fix); those features/translations are not included. License: Unlicense, retained in LICENSE.

Automated checks compare selected cells against an independent partition/line implementation of the original algorithms. The coordinator also verified all 12 styles register in RimWorld 1.7, preset 3 selects the expected 16 cells, inverse preset 3 selects four cells, bounds/duplicate checks pass, and the mod coexists with all standalone shape mods. Settings editing/persistence and visual palette interaction still require manual checks.


# WebfishingSampleMod

## Setup

- Pick an ID for your project (WEBFISHING modders prefer the format of `AuthorName.ModName`).
- Click "Use this template" on the top right, and name the repository after your mod ID.
- Delete this README and replace it with your own.
- Change the `manifest.json` to have the relevant information.

What you do next depends on what you want to accomplish.

## Custom scripts/assets

Custom scripts and assets can be created in the Godot editor. You will need [GodotSteam v3.5.2](https://github.com/GodotSteam/GodotSteam/releases/tag/v3.21).

If you do not plan to work in the Godot editor, delete the `project` folder and remove the entry from the `manifest.json`.

Change the following information:

- the project name (Project > Project Settings)
- the folder name in `mods`

### Working in a decompiled project

You can work in a [decompiled project](https://github.com/NotNite/GDWeave/blob/main/MODS.md#gdretools) if you want with the following command (must be ran in an admin PowerShell prompt):

```pwsh
NewItem -ItemType SymbolicLink -Path path/to/decompiled_project/mods/ModId -Target path/to/project/mods/ModId
```

Make sure to never export from the decompiled project, as your pack file can contain unwanted assets.

## Existing script modding

Modding existing game scripts is accomplished with a C# assembly. See [here](https://github.com/NotNite/GDWeave/blob/main/MODS.md#writing-script-mods) for more information.

If you do not plan to edit existing game code, delete the `WebfishingSampleMod.sln` file and `WebfishingSampleMod` folder, and remove the entry from the `manifest.json`.

Change the following information:

- solution name
- project & folder name
- C# namespace

### Building

To build the project, you need to set the `GDWeavePath` environment variable to your game install's GDWeave directory (e.g. `G:\games\steam\steamapps\common\WEBFISHING\GDWeave`). This can also be done in Rider with `File | Settings | Build, Execution, Deployment | Toolset and Build | MSBuild global properties` or with a .user file in Visual Studio.

namespace UnrealReplayReader.Models.Enums;

public enum EUnrealEngineObjectUe5Version : uint
{
    // Note that currently the oldest loadable package version is EUnrealEngineObjectUEVersion::VER_UE4_OLDEST_LOADABLE_PACKAGE
    // this can be enabled should we ever deprecate UE4 versions entirely
    //OLDEST_LOADABLE_PACKAGE = ???,

    // The original UE5 version, at the time this was added the UE4 version was 522, so UE5 will start from 1000 to show a clear difference
    InitialVersion = 1000,

    // Support stripping names that are not referenced from export data
    NamesReferencedFromExportData,

    // Added a payload table of contents to the package summary 
    PayloadToc,

    // Added data to identify references from and to optional package
    OptionalResources,

    // Large world coordinates converts a number of core types to double components by default.
    LargeWorldCoordinates,

    // Remove package GUID from FObjectExport
    RemoveObjectExportPackageGuid,

    // Add IsInherited to the FObjectExport entry
    TrackObjectExportIsInherited,

    // Replace FName asset path in FSoftObjectPath with (package name, asset name) pair FTopLevelAssetPath
    FsoftobjectpathRemoveAssetPathFnames,

    // -----<new versions can be added before this line>-------------------------------------------------
    // - this needs to be the last line (see note below)
    AutomaticVersionPlusOne,
    AutomaticVersion = AutomaticVersionPlusOne - 1
}
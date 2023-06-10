using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Models;

public record struct FPackageFileVersion
{
    /* UE4 file version*/
    public EUnrealEngineObjectUe4Version FileVersionUe4 { get; set; }

    /* UE5 file version */
    public EUnrealEngineObjectUe5Version FileVersionUe5 { get; set; }
}
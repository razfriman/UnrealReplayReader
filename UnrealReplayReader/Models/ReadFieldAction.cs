using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models;

public delegate void ReadFieldAction(UnrealReplay replay, ExportModel model, NetBitReader reader,
    NetFieldExportGroup group, NetFieldExport field);

public delegate void ReadFieldAction<TExport>(UnrealReplay replay, TExport model, NetBitReader reader,
    NetFieldExportGroup group, NetFieldExport field)
    where TExport : ExportModel;
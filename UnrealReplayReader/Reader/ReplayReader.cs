using System.Diagnostics;
using Microsoft.Extensions.Logging;
using UnrealReplayReader.IO;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public TReplay Replay { get; set; } = new();
    public ReplayReaderSettings Settings { get; set; }
    public ILogger? Logger => Settings.Logger;
    public NetFieldParser NetFieldParser { get; set; } = new();

    public GuidCache GuidCache { get; set; } = new();

    public int PacketIndex { get; set; }
    public Bunch? PartialBunch { get; set; }
    public int InReliable { get; set; }
    public int InPacketId { get; set; }
    public List<NetworkGuid> DebugNetGuidToPathName { get; set; } = new();

    protected ReplayReader()
    {
        NetFieldParser.GuidCache = GuidCache;
        GuidCache.NetFieldParser = NetFieldParser;
    }

    public TReplay ReadFile(string file, ReplayReaderSettings settings) =>
        ReadBytes(File.ReadAllBytes(file), settings);

    public TReplay ReadBytes(byte[] bytes, ReplayReaderSettings settings) =>
        Read(new ByteReader(bytes), settings);

    public TReplay Read(ByteReader reader, ReplayReaderSettings settings)
    {
        PacketIndex = 0;
        Settings = settings;
        Replay = new TReplay();
        NetFieldParser.Init(settings.ExportConfiguration);
        GuidCache.NetFieldParser = NetFieldParser;
        GuidCache.Settings = Settings;
        NetFieldParser.GuidCache = GuidCache;
        Replay.Settings = settings;
        var sw = Stopwatch.StartNew();
        Replay.Info = ReadReplayInfo(reader);
        Replay.Header = ReadHeader(reader, Replay.Info);
        ReadEvents(reader);
        ReadChunks(reader);
        Replay.ParseTime = sw.ElapsedMilliseconds;
        EmitReadReplayFinished();
        return Replay;
    }

    public void ResetForCheckpoint()
    {
        InPacketId = 0;
        InReliable = 0;
        ExternalDatas.Clear();
        GuidCache.CleanForCheckpoint();
        NetFieldParser.ResetForCheckpoint();
        Channels.Clear();
        IgnoredChannels.Clear();
        ActorToChannel.Clear();
        ChannelToActor.Clear();
        ActorToPath.Clear();
        DormantActors.Clear();
    }
}
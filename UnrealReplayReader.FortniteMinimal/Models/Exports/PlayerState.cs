using UnrealReplayReader.Models;

namespace UnrealReplayReader.FortniteMinimal.Models.Exports;

public record PlayerState : ExportModel
{
    public string? UniqueId { get; set; }
    public string? PlayerNamePrivate { get; set; }
    public byte? Ping { get; set; }

    public record Configuration : GroupConfiguration<PlayerState>
    {
        public Configuration()
        {
            AddPath("/Script/FortniteGame.FortPlayerStateAthena");
            AddProperty(x => x.UniqueId, (model, reader) => model.UniqueId = reader.ReadNetId());
            AddProperty(x => x.PlayerNamePrivate, (model, reader) => model.PlayerNamePrivate = reader.ReadFString());
            AddProperty("Ping", (model, reader) => model.Ping = reader.ReadByte());
            AddProperty("CompressedPing", (model, reader) => model.Ping = reader.ReadByte());
        }
    }
}
namespace UnrealReplayReader.Models.Classes;

public record ItemDefinition : NetworkGuid
{
    public string? Name { get; set; }

    public override void Resolve(GuidCache cache)
    {
        if (IsValid)
        {
            var name = cache.TryGetPathName(Value);

            if (name != null)
            {
                Name = name;
                // Console.WriteLine(name);
            }
        }
    }
}
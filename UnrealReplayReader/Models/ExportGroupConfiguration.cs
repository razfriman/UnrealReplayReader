using System.Linq.Expressions;
using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models;

public record GroupConfiguration<TModel> : GroupConfiguration where TModel : ExportModel, new()
{
    public override ExportModel CreateInstance() => new TModel();

    public GroupConfiguration()
    {
        Model = new TModel();
    }

    public ExportModel Read(NetBitReader reader)
    {
        return new TModel();
    }

    public void AddPath(string path)
    {
        Paths.Add(path);
    }

    public void AddPartialPath(string path)
    {
        PartialPaths.Add(path);
    }

    public void AddPlayerController(string path)
    {
        PlayerControllers.Add(path);
    }


    public void AddStaticActorId(string staticActorId)
    {
        StaticActorIds.Add(staticActorId);
    }

    public void AddRedirect(string redirect)
    {
        Redirects.Add(redirect);
    }

    public void AddNetDeltaProperty(string name, string type, bool enablePropertyChecksum = false)
    {
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            ParseType = ParseTypes.NetDeltaSerialize,
            Type = type,
            EnablePropertyChecksum = enablePropertyChecksum,
            Read = (_, _, _, _, _) => { }
        };
        Fields[field.Name] = field;
    }

    public void AddPropertyFunction(string name, string type = null)
    {
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            ParseType = ParseTypes.Function,
            Type = type,
            Read = (_, _, _, _, _) => { }
        };
        Fields[field.Name] = field;
    }

    public void AddPropertyFunction<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression,
        string functionType)
    {
        var name = (propertyExpression.Body as MemberExpression).Member.Name;
        AddPropertyFunction(name, functionType);
    }

    public void AddProperty<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression,
        Action<TModel, NetBitReader> readAction, ParseTypes parseType = ParseTypes.Default)
    {
        AddProperty((propertyExpression.Body as MemberExpression).Member.Name, readAction, parseType);
    }

    public void AddProperty(string name, Action<NetBitReader> readAction,
        ParseTypes parseType = ParseTypes.Default)
    {
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            Read = (_, _, reader, _, _) => readAction(reader),
            ParseType = parseType
        };
        Fields[field.Name] = field;
    }

    public void AddDynamicArrayProperty(string name, ParseTypes parseType = ParseTypes.Default)
    {
        ;
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            Read = (replay, model, reader, group, field) => reader.ReadDynamicArray<TModel>(replay, group, this),
            ParseType = parseType
        };
        Fields[field.Name] = field;
    }

    public void AddProperty(string name, Action<TModel, NetBitReader> readAction,
        ParseTypes parseType = ParseTypes.Default)
    {
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            Read = (_, model, reader, _, _) => readAction(model, reader),
            ParseType = parseType
        };
        Fields[field.Name] = field;
    }

    public void AddProperty(string name, ReadFieldAction<TModel> readAction,
        ParseTypes parseType = ParseTypes.Default)
    {
        var field = new FieldConfiguration<TModel>
        {
            Name = name,
            Read = readAction,
            ParseType = parseType
        };
        Fields[field.Name] = field;
    }
}

public class FieldConfiguration<TModel> : FieldConfiguration where TModel : ExportModel, new()
{
    public virtual ReadFieldAction<TModel> Read { get; set; } =
        (_, _, _, _, _) => { };

    public override ReadFieldAction ReadGeneric =>
        (replay, model, reader, group, field) => Read(replay, (TModel)model, reader, group, field);

    public override ExportModel CreateInstance() => new TModel();
}
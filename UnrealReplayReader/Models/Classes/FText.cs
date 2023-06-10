using UnrealReplayReader.Exceptions;
using UnrealReplayReader.IO;

namespace UnrealReplayReader.Models.Classes;

public sealed record FText : CustomClass
{
    public uint? Flags { get; set; }
    public ETextHistoryType? HistoryType { get; set; }
    public FTextHistory? TextHistory { get; set; }
    public string? Text => TextHistory?.Text;

    public FText()
    {
    }

    public FText(BitReader reader) => Serialize(reader);

    public override void Serialize(BitReader reader)
    {
        Flags = reader.ReadUInt32();
        HistoryType = reader.ReadByteAsEnum<ETextHistoryType>();
        TextHistory = HistoryType switch
        {
            ETextHistoryType.None => new FTextHistory.None(reader),
            ETextHistoryType.Base => new FTextHistory.Base(reader),
            ETextHistoryType.NamedFormat => new FTextHistory.NamedFormat(reader),
            //     ETextHistoryType.OrderedFormat => new FTextHistory.OrderedFormat(Ar),
            //     ETextHistoryType.ArgumentFormat => new FTextHistory.ArgumentFormat(Ar),
            //     ETextHistoryType.AsNumber => new FTextHistory.FormatNumber(Ar, HistoryType),
            //     ETextHistoryType.AsPercent => new FTextHistory.FormatNumber(Ar, HistoryType),
            //     ETextHistoryType.AsCurrency => new FTextHistory.FormatNumber(Ar, HistoryType),
            //     ETextHistoryType.AsDate => new FTextHistory.AsDate(Ar),
            //     ETextHistoryType.AsTime => new FTextHistory.AsTime(Ar),
            //     ETextHistoryType.AsDateTime => new FTextHistory.AsDateTime(Ar),
            //     ETextHistoryType.Transform => new FTextHistory.Transform(Ar),
            //     ETextHistoryType.StringTableEntry => new FTextHistory.StringTableEntry(Ar),
            //     ETextHistoryType.TextGenerator => new FTextHistory.TextGenerator(Ar),
            // _ => new FTextHistory.None(reader)
        };
    }

//
    public enum ETextHistoryType : sbyte
    {
        None = -1,
        Base = 0,
        NamedFormat,
        OrderedFormat,
        ArgumentFormat,
        AsNumber,
        AsPercent,
        AsCurrency,
        AsDate,
        AsTime,
        AsDateTime,
        Transform,
        StringTableEntry,
        TextGenerator,

        // Add new enum types at the end only! They are serialized by index.
    }

    public enum EFormatArgumentType : sbyte
    {
        Int,
        UInt,
        Float,
        Double,
        Text,
        Gender,

        // Add new enum types at the end only! They are serialized by index.
    }

    public enum ERoundingMode : sbyte
    {
        /** Rounds to the nearest place, equidistant ties go to the value which is closest to an even value: 1.5 becomes 2, 0.5 becomes 0 */
        HalfToEven,

        /** Rounds to nearest place, equidistant ties go to the value which is further from zero: -0.5 becomes -1.0, 0.5 becomes 1.0 */
        HalfFromZero,

        /** Rounds to nearest place, equidistant ties go to the value which is closer to zero: -0.5 becomes 0, 0.5 becomes 0. */
        HalfToZero,

        /** Rounds to the value which is further from zero, "larger" in absolute value: 0.1 becomes 1, -0.1 becomes -1 */
        FromZero,

        /** Rounds to the value which is closer to zero, "smaller" in absolute value: 0.1 becomes 0, -0.1 becomes 0 */
        ToZero,

        /** Rounds to the value which is more negative: 0.1 becomes 0, -0.1 becomes -1 */
        ToNegativeInfinity,

        /** Rounds to the value which is more positive: 0.1 becomes 1, -0.1 becomes 0 */
        ToPositiveInfinity,


        // Add new enum types at the end only! They are serialized by index.
    }

    public enum EDateTimeStyle : sbyte
    {
        Default,
        Short,
        Medium,
        Long,

        Full
        // Add new enum types at the end only! They are serialized by index.
    }

    public enum ETransformType : byte
    {
        ToLower = 0,
        ToUpper,

        // Add new enum types at the end only! They are serialized by index.
    }

    public enum EStringTableLoadingPhase : byte
    {
        /** This string table is pending load, and load should be attempted when possible */
        PendingLoad,

        /** This string table is currently being loaded, potentially asynchronously */
        Loading,

        /** This string was loaded, though that load may have failed */
        Loaded,
    }
}

//
public abstract record FTextHistory
{
    public abstract string Text { get; }

    public record None : FTextHistory
    {
        public readonly string? CultureInvariantString;
        public override string Text => CultureInvariantString ?? string.Empty;

        public None()
        {
        }

        public None(FReader Ar)
        {
            // if (FEditorObjectVersion.Get(Ar) >=
            //     FEditorObjectVersion.Type.CultureInvariantTextSerializationKeyStability)
            // {
            //     var bHasCultureInvariantString = Ar.ReadBoolean();
            //     if (bHasCultureInvariantString)
            //     {
            //         CultureInvariantString = Ar.ReadFString();
            //     }
            // }
        }
    }

    public record Base : FTextHistory
    {
        public string Namespace { get; set; }
        public string Key { get; set; }
        public string SourceString { get; set; }
        public override string Text => SourceString;

        public Base(FReader Ar)
        {
            Namespace = Ar.ReadFString();
            Key = Ar.ReadFString();
            SourceString = Ar.ReadFString();
        }
    }

    public record NamedFormat : FTextHistory
    {
        public readonly FText SourceFmt;

        public readonly Dictionary<string, FFormatArgumentValue>
            Arguments; /* called FFormatNamedArguments in UE4 */

        public override string Text => SourceFmt.Text;

        public NamedFormat(BitReader reader)
        {
            SourceFmt = new FText(reader);
            Arguments = new Dictionary<string, FFormatArgumentValue>(reader.ReadInt32());
            for (var i = 0; i < Arguments.Count; i++)
            {
                Arguments[reader.ReadFString()] = new FFormatArgumentValue(reader);
            }
        }
    }
}
//
//     public record OrderedFormat : FTextHistory
//     {
//         public readonly FText SourceFmt;
//         public readonly FFormatArgumentValue[] Arguments; /* called FFormatOrderedArguments in UE4 */
//         public override string Text => SourceFmt.Text;
//
//         public OrderedFormat(FReader Ar)
//         {
//             SourceFmt = new FText(Ar);
//             Arguments = Ar.ReadArray(() => new FFormatArgumentValue(Ar));
//         }
//     }
//
//     public record ArgumentFormat : FTextHistory
//     {
//         public readonly FText SourceFmt;
//         public readonly FFormatArgumentData[] Arguments;
//         public override string Text => SourceFmt.Text;
//
//         public ArgumentFormat(FReader Ar)
//         {
//             SourceFmt = new FText(Ar);
//             Arguments = Ar.ReadArray(() => new FFormatArgumentData(Ar));
//         }
//     }
//
//     public record FormatNumber : FTextHistory
//     {
//         public readonly string? CurrencyCode;
//         public readonly FFormatArgumentValue SourceValue;
//         public readonly FNumberFormattingOptions? FormatOptions;
//         public readonly string TargetCulture;
//         public override string Text => SourceValue.Value.ToString();
//
//         public FormatNumber(FReader Ar, ETextHistoryType historyType)
//         {
//             if (historyType == ETextHistoryType.AsCurrency &&
//                 Ar.Ver >= UE4Version.VER_UE4_ADDED_CURRENCY_CODE_TO_FTEXT)
//             {
//                 CurrencyCode = Ar.ReadFString();
//             }
//
//             SourceValue = new FFormatArgumentValue(Ar);
//             if (Ar.ReadBoolean()) // bHasFormatOptions
//             {
//                 FormatOptions = new FNumberFormattingOptions(Ar);
//             }
//
//             TargetCulture = Ar.ReadFString();
//         }
//     }
//
//     public record AsDate : FTextHistory
//     {
//         public readonly FDateTime SourceDateTime;
//         public readonly EDateTimeStyle DateStyle;
//         public readonly string? TimeZone;
//         public readonly string TargetCulture;
//         public override string Text => SourceDateTime.ToString();
//
//         public AsDate(FReader Ar)
//         {
//             SourceDateTime = Ar.Read<FDateTime>();
//             DateStyle = Ar.Read<EDateTimeStyle>();
//             if (Ar.Ver >= UE4Version.VER_UE4_FTEXT_HISTORY_DATE_TIMEZONE)
//             {
//                 TimeZone = Ar.ReadFString();
//             }
//
//             TargetCulture = Ar.ReadFString();
//         }
//     }
//
//     public record AsTime : FTextHistory
//     {
//         public readonly FDateTime SourceDateTime;
//         public readonly EDateTimeStyle TimeStyle;
//         public readonly string TimeZone;
//         public readonly string TargetCulture;
//         public override string Text => SourceDateTime.ToString();
//
//         public AsTime(FReader Ar)
//         {
//             SourceDateTime = Ar.Read<FDateTime>();
//             TimeStyle = Ar.Read<EDateTimeStyle>();
//             TimeZone = Ar.ReadFString();
//             TargetCulture = Ar.ReadFString();
//         }
//     }
//
//     public class AsDateTime : FTextHistory
//     {
//         public readonly FDateTime SourceDateTime;
//         public readonly EDateTimeStyle DateStyle;
//         public readonly EDateTimeStyle TimeStyle;
//         public readonly string TimeZone;
//         public readonly string TargetCulture;
//         public override string Text => SourceDateTime.ToString();
//
//         public AsDateTime(FReader Ar)
//         {
//             SourceDateTime = Ar.Read<FDateTime>();
//             DateStyle = Ar.Read<EDateTimeStyle>();
//             TimeStyle = Ar.Read<EDateTimeStyle>();
//             TimeZone = Ar.ReadFString();
//             TargetCulture = Ar.ReadFString();
//         }
//     }
//
//     public record Transform : FTextHistory
//     {
//         public readonly FText SourceText;
//         public readonly ETransformType TransformType;
//         public override string Text => SourceText.Text;
//
//         public Transform(FReader Ar)
//         {
//             SourceText = new FText(Ar);
//             TransformType = Ar.Read<ETransformType>();
//         }
//     }
//
//     public record StringTableEntry : FTextHistory
//     {
//         public readonly FName TableId;
//         public readonly string Key;
//         public override string Text { get; }
//
//         public StringTableEntry(FReader Ar)
//         {
//             TableId = Ar.ReadFName();
//             Key = Ar.ReadFString();
//
//             if (Ar.Owner.Provider!.TryLoadObject(TableId.Text, out UStringTable table) &&
//                 table.StringTable.KeysToMetaData.TryGetValue(Key, out var t))
//             {
//                 Text = t;
//             }
//         }
//     }
//
//     public record TextGenerator : FTextHistory
//     {
//         public readonly FName GeneratorTypeID;
//         public readonly byte[]? GeneratorContents;
//         public override string Text => GeneratorTypeID.Text;
//
//         public TextGenerator(FReader Ar)
//         {
//             GeneratorTypeID = Ar.ReadFName();
//             if (!GeneratorTypeID.IsNone)
//             {
//                 // https://github.com/EpicGames/UnrealEngine/blob/4.26/Engine/Source/Runtime/Core/Private/Internationalization/TextHistory.cpp#L2916
//                 // I don't understand what it does here
//             }
//         }
//     }
// }

public record FFormatArgumentValue
{
    public FText.EFormatArgumentType Type { get; set; }
    public object Value;

    public FFormatArgumentValue(BitReader reader)
    {
        Type = reader.ReadByteAsEnum<FText.EFormatArgumentType>();
        Value = Type switch
        {
            FText.EFormatArgumentType.Text => new FText(reader),
            FText.EFormatArgumentType.Int => reader.ReadInt64(),
            FText.EFormatArgumentType.UInt => reader.ReadUInt64(),
            FText.EFormatArgumentType.Double => reader.ReadDouble(),
            FText.EFormatArgumentType.Float => reader.ReadSingle(),
            _ => throw new ReplayException($"{Type} argument not supported yet"),
        };
    }
}
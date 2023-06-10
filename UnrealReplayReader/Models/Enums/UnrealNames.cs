namespace UnrealReplayReader.Models.Enums;

public class UnrealNameConstants
{
    public static readonly string[] Names;

    static UnrealNameConstants()
    {
        Names = new string[(int)Enum.GetValues(typeof(UnrealNames)).Cast<UnrealNames>().Last() + 1];
        foreach (UnrealNames name in Enum.GetValues(typeof(UnrealNames)))
        {
            Names[(int)name] = name.ToString();
        }
    }
}

public enum UnrealNames : int
{
    None = 0,
    ByteProperty = 1,
    IntProperty = 2,
    BoolProperty = 3,
    FloatProperty = 4,
    ObjectProperty = 5, // ClassProperty shares the same tag 
    NameProperty = 6,
    DelegateProperty = 7,
    DoubleProperty = 8,
    ArrayProperty = 9,
    StructProperty = 10,
    VectorProperty = 11,
    RotatorProperty = 12,
    StrProperty = 13,
    TextProperty = 14,
    InterfaceProperty = 15,
    MulticastDelegateProperty = 16,

    //Available = 17
    LazyObjectProperty = 18,
    SoftObjectProperty = 19, // SoftClassProperty shares the same tag
    UInt64Property = 20,
    UInt32Property = 21,
    UInt16Property = 22,
    Int64Property = 23,
    Int16Property = 25,
    Int8Property = 26,

    //Available = 27
    MapProperty = 28,
    SetProperty = 29,

    // Special packages.
    Core = 30,
    Engine = 31,
    Editor = 32,
    CoreUObject = 33,

    // More class properties
    EnumProperty = 34,

    // Special types.
    Cylinder = 50,
    BoxSphereBounds = 51,
    Sphere = 52,
    Box = 53,
    Vector2D = 54,
    IntRect = 55,
    IntPoint = 56,
    Vector4 = 57,
    Name = 58,
    Vector = 59,
    Rotator = 60,
    ShVector = 61,
    Color = 62,
    Plane = 63,
    Matrix = 64,
    LinearColor = 65,
    AdvanceFrame = 66,
    Pointer = 67,
    Double = 68,
    Quat = 69,
    Self = 70,
    Transform = 71,
    Vector3F = 72,
    Vector3d = 73,
    Plane4F = 74,
    Plane4d = 75,
    Matrix44F = 76,
    Matrix44d = 77,
    Quat4F = 78,
    Quat4d = 79,
    Transform3F = 80,
    Transform3d = 81,
    Box3F = 82,
    Box3d = 83,
    BoxSphereBounds3F = 84,
    BoxSphereBounds3d = 85,
    Vector4F = 86,
    Vector4d = 87,
    Rotator3F = 88,
    Rotator3d = 89,
    Vector2F = 90,
    Vector2d = 91,
    Box2D = 92,
    Box2F = 93,
    Box2d = 94,
    IntVector = 95,
    IntVector4 = 96,
    UintVector = 97,
    UintVector4 = 98,

    // Object class names.
    Object = 100,
    Camera = 101,
    Actor = 102,
    ObjectRedirector = 103,
    ObjectArchetype = 104,
    Class = 105,
    ScriptStruct = 106,
    Function = 107,
    Pawn = 108,

    // Special types continued
    Int32Vector = 150,
    Int64Vector = 151,
    Uint32Vector = 152,
    Uint64Vector = 153,
    Int32Vector4 = 154,
    Int64Vector4 = 155,
    Uint32Vector4 = 156,
    Uint64Vector4 = 157,
    IntVector2 = 158,
    Int32Vector2 = 159,
    Int64Vector2 = 160,
    UintVector2 = 161,
    Uint32Vector2 = 162,
    Uint64Vector2 = 163,
    UintPoint = 164,
    Int32Point = 165,
    Int64Point = 166,
    Uint32Point = 167,
    Uint64Point = 168,

    // Misc.
    State = 200,
    True = 201,
    False = 202,
    Enum = 203,
    Default = 204,
    Skip = 205,
    Input = 206,
    Package = 207,
    Groups = 208,
    Interface = 209,
    Components = 210,
    Global = 211,
    Super = 212,
    Outer = 213,
    Map = 214,
    Role = 215,
    RemoteRole = 216,
    PersistentLevel = 217,
    TheWorld = 218,
    PackageMetaData = 219,
    InitialState = 220,
    Game = 221,
    SelectionColor = 222,
    Ui = 223,
    ExecuteUbergraph = 224,
    DeviceId = 225,
    RootStat = 226,
    MoveActor = 227,
    All = 230,
    MeshEmitterVertexColor = 231,
    TextureOffsetParameter = 232,
    TextureScaleParameter = 233,
    ImpactVel = 234,
    SlideVel = 235,
    TextureOffset1Parameter = 236,
    MeshEmitterDynamicParameter = 237,
    ExpressionInput = 238,
    Untitled = 239,
    Timer = 240,
    Team = 241,
    Low = 242,
    High = 243,
    NetworkGuid = 244,
    GameThread = 245,
    RenderThread = 246,
    OtherChildren = 247,
    Location = 248,
    Rotation = 249,
    Bsp = 250,
    EditorSettings = 251,
    AudioThread = 252,
    Id = 253,
    UserDefinedEnum = 254,
    Control = 255,
    Voice = 256,
    Zlib = 257,
    Gzip = 258,
    Lz4 = 259,
    Mobile = 260,
    Oodle = 261,

    // Online
    DGram = 280,
    Stream = 281,
    GameNetDriver = 282,
    PendingNetDriver = 283,
    BeaconNetDriver = 284,
    FlushNetDormancy = 285,
    DemoNetDriver = 286,
    GameSession = 287,
    PartySession = 288,
    GamePort = 289,
    BeaconPort = 290,
    MeshPort = 291,
    MeshNetDriver = 292,
    LiveStreamVoice = 293,
    LiveStreamAnimation = 294,

    // Texture settings.
    Linear = 300,
    Point = 301,
    Aniso = 302,
    LightMapResolution = 303,

    // Sound.
    //310 = 
    UnGrouped = 311,
    VoiceChat = 312,

    // Optimized replication.
    Playing = 320,
    Spectating = 322,
    Inactive = 325,

    // Log messages.
    PerfWarning = 350,
    Info = 351,
    Init = 352,
    Exit = 353,
    Cmd = 354,
    Warning = 355,
    Error = 356,

    // File format backwards-compatibility.
    FontCharacter = 400,
    InitChild2StartBone = 401,
    SoundCueLocalized = 402,
    SoundCue = 403,
    RawDistributionFloat = 404,
    RawDistributionVector = 405,
    InterpCurveFloat = 406,
    InterpCurveVector2D = 407,
    InterpCurveVector = 408,
    InterpCurveTwoVectors = 409,
    InterpCurveQuat = 410,

    Ai = 450,
    NavMesh = 451,

    PerformanceCapture = 500,

    // Special config names - not required to be consistent for network replication
    EditorLayout = 600,
    EditorKeyBindings = 601,
    GameUserSettings = 602,

    Filename = 700,
    Lerp = 701,
    Root = 702,
}
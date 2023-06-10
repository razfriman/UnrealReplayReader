namespace UnrealReplayReader.Models.Enums;

public enum EUnrealEngineObjectUe4Version : uint
{
    VerUe4OldestLoadablePackage = 214,

    // Removed restriction on blueprint-exposed variables from being read-only
    VerUe4BlueprintVarsNotReadOnly,

    // Added manually serialized element to UStaticMesh (precalculated nav collision)
    VerUe4StaticMeshStoreNavCollision,

    // Changed property name for atmospheric fog
    VerUe4AtmosphericFogDecayNameChange,

    // Change many properties/functions from Translation to Location
    VerUe4ScenecompTranslationToLocation,

    // Material attributes reordering
    VerUe4MaterialAttributesReordering,

    // Collision Profile setting has been added, and all components that exists has to be properly upgraded
    VerUe4CollisionProfileSetting,

    // Making the blueprint's skeleton class transient
    VerUe4BlueprintSkelTemporaryTransient,

    // Making the blueprint's skeleton class serialized again
    VerUe4BlueprintSkelSerializedAgain,

    // Blueprint now controls replication settings again
    VerUe4BlueprintSetsReplication,

    // Added level info used by World browser
    VerUe4WorldLevelInfo,

    // Changed capsule height to capsule half-height (afterwards)
    VerUe4AfterCapsuleHalfHeightChange,

    // Added Namepace, GUID (Key) and Flags to FText
    VerUe4AddedNamespaceAndKeyDataToFtext,

    // Attenuation shapes
    VerUe4AttenuationShapes,

    // Use IES texture multiplier even when IES brightness is not being used
    VerUe4LightcomponentUseIesTextureMultiplierOnNonIesBrightness,

    // Removed InputComponent as a blueprint addable component
    VerUe4RemoveInputComponentsFromBlueprints,

    // Use an FMemberReference struct in UK2Node_Variable
    VerUe4Vark2NodeUseMemberrefstruct,

    // Refactored material expression inputs for UMaterialExpressionSceneColor and UMaterialExpressionSceneDepth
    VerUe4RefactorMaterialExpressionScenecolorAndScenedepthInputs,

    // Spline meshes changed from Z forwards to configurable
    VerUe4SplineMeshOrientation,

    // Added ReverbEffect asset type
    VerUe4ReverbEffectAssetType,

    // changed max texcoords from 4 to 8
    VerUe4MaxTexcoordIncreased,

    // static meshes changed to support SpeedTrees
    VerUe4SpeedtreeStaticmesh,

    // Landscape component reference between landscape component and collision component
    VerUe4LandscapeComponentLazyReferences,

    // Refactored UK2Node_CallFunction to use FMemberReference
    VerUe4SwitchCallNodeToUseMemberReference,

    // Added fixup step to remove skeleton class references from blueprint objects
    VerUe4AddedSkeletonArchiverRemoval,

    // See above, take 2.
    VerUe4AddedSkeletonArchiverRemovalSecondTime,

    // Making the skeleton class on blueprints transient
    VerUe4BlueprintSkelClassTransientAgain,

    // UClass knows if it's been cooked
    VerUe4AddCookedToUclass,

    // Deprecated static mesh thumbnail properties were removed
    VerUe4DeprecatedStaticMeshThumbnailPropertiesRemoved,

    // Added collections in material shader map ids
    VerUe4CollectionsInShadermapid,

    // Renamed some Movement Component properties, added PawnMovementComponent
    VerUe4RefactorMovementComponentHierarchy,

    // Swap UMaterialExpressionTerrainLayerSwitch::LayerUsed/LayerNotUsed the correct way round
    VerUe4FixTerrainLayerSwitchOrder,

    // Remove URB_ConstraintSetup
    VerUe4AllPropsToConstraintinstance,

    // Low quality directional lightmaps
    VerUe4LowQualityDirectionalLightmaps,

    // Added NoiseEmitterComponent and removed related Pawn properties.
    VerUe4AddedNoiseEmitterComponent,

    // Add text component vertical alignment
    VerUe4AddTextComponentVerticalAlignment,

    // Added AssetImportData for FBX asset types, deprecating SourceFilePath and SourceFileTimestamp
    VerUe4AddedFbxAssetImportData,

    // Remove LevelBodySetup from ULevel
    VerUe4RemoveLevelbodysetup,

    // Refactor character crouching
    VerUe4RefactorCharacterCrouch,

    // Trimmed down material shader debug information.
    VerUe4SmallerDebugMaterialshaderUniformExpressions,

    // APEX Clothing
    VerUe4ApexCloth,

    // Change Collision Channel to save only modified ones than all of them
    // @note!!! once we pass this CL, we can rename FCollisionResponseContainer enum values
    // we should rename to match ECollisionChannel
    VerUe4SaveCollisionresponsePerChannel,

    // Added Landscape Spline editor meshes
    VerUe4AddedLandscapeSplineEditorMesh,

    // Fixup input expressions for reading from refraction material attributes.
    VerUe4ChangedMaterialRefactionType,

    // Refactor projectile movement, along with some other movement component work.
    VerUe4RefactorProjectileMovement,

    // Remove PhysicalMaterialProperty and replace with user defined enum
    VerUe4RemovePhysicalmaterialproperty,

    // Removed all compile outputs from FMaterial
    VerUe4PurgedFmaterialCompileOutputs,

    // Ability to save cooked PhysX meshes to Landscape
    VerUe4AddCookedToLandscape,

    // Change how input component consumption works
    VerUe4ConsumeInputPerBind,

    // Added new Graph based SoundClass Editor
    VerUe4SoundClassGraphEditor,

    // Fixed terrain layer node guids which was causing artifacts
    VerUe4FixupTerrainLayerNodes,

    // Added clamp min/max swap check to catch older materials
    VerUe4RetrofitClampExpressionsSwap,

    // Remove static/movable/stationary light classes
    VerUe4RemoveLightMobilityClasses,

    // Refactor the way physics blending works to allow partial blending
    VerUe4RefactorPhysicsBlending,

    // WorldLevelInfo: Added reference to parent level and streaming distance
    VerUe4WorldLevelInfoUpdated,

    // Fixed cooking of skeletal/static meshes due to bad serialization logic
    VerUe4StaticSkeletalMeshSerializationFix,

    // Removal of InterpActor and PhysicsActor
    VerUe4RemoveStaticmeshMobilityClasses,

    // Refactor physics transforms
    VerUe4RefactorPhysicsTransforms,

    // Remove zero triangle sections from static meshes and compact material indices.
    VerUe4RemoveZeroTriangleSections,

    // Add param for deceleration in character movement instead of using acceleration.
    VerUe4CharacterMovementDeceleration,

    // Made ACameraActor use a UCameraComponent for parameter storage, etc...
    VerUe4CameraActorUsingCameraComponent,

    // Deprecated some pitch/roll properties in CharacterMovementComponent
    VerUe4CharacterMovementDeprecatePitchRoll,

    // Rebuild texture streaming data on load for uncooked builds
    VerUe4RebuildTextureStreamingDataOnLoad,

    // Add support for 32 bit index buffers for static meshes.
    VerUe4Support32BitStaticMeshIndices,

    // Added streaming install ChunkID to AssetData and UPackage
    VerUe4AddedChunkidToAssetdataAndUpackage,

    // Add flag to control whether Character blueprints receive default movement bindings.
    VerUe4CharacterDefaultMovementBindings,

    // APEX Clothing LOD Info
    VerUe4ApexClothLod,

    // Added atmospheric fog texture data to be general
    VerUe4AtmosphericFogCacheData,

    // Arrays serialize their inner's tags
    VarUe4ArrayPropertyInnerTags,

    // Skeletal mesh index data is kept in memory in game to support mesh merging.
    VerUe4KeepSkelMeshIndexData,

    // Added compatibility for the body instance collision change
    VerUe4BodysetupCollisionConversion,

    // Reflection capture cooking
    VerUe4ReflectionCaptureCooking,

    // Removal of DynamicTriggerVolume, DynamicBlockingVolume, DynamicPhysicsVolume
    VerUe4RemoveDynamicVolumeClasses,

    // Store an additional flag in the BodySetup to indicate whether there is any cooked data to load
    VerUe4StoreHascookeddataForBodysetup,

    // Changed name of RefractionBias to RefractionDepthBias.
    VerUe4RefractionBiasToRefractionDepthBias,

    // Removal of SkeletalPhysicsActor
    VerUe4RemoveSkeletalphysicsactor,

    // PlayerController rotation input refactor
    VerUe4PcRotationInputRefactor,

    // Landscape Platform Data cooking
    VerUe4LandscapePlatformdataCooking,

    // Added call for linking classes in CreateExport to ensure memory is initialized properly
    VerUe4CreateexportsClassLinkingForBlueprints,

    // Remove native component nodes from the blueprint SimpleConstructionScript
    VerUe4RemoveNativeComponentsFromBlueprintScs,

    // Removal of Single Node Instance
    VerUe4RemoveSinglenodeinstance,

    // Character movement braking changes
    VerUe4CharacterBrakingRefactor,

    // Supported low quality lightmaps in volume samples
    VerUe4VolumeSampleLowQualitySupport,

    // Split bEnableTouchEvents out from bEnableClickEvents
    VerUe4SplitTouchAndClickEnables,

    // Health/Death refactor
    VerUe4HealthDeathRefactor,

    // Moving USoundNodeEnveloper from UDistributionFloatConstantCurve to FRichCurve
    VerUe4SoundNodeEnveloperCurveChange,

    // Moved SourceRadius to UPointLightComponent
    VerUe4PointLightSourceRadius,

    // Scene capture actors based on camera actors.
    VerUe4SceneCaptureCameraChange,

    // Moving SkeletalMesh shadow casting flag from LoD details to material
    VerUe4MoveSkeletalmeshShadowcasting,

    // Changing bytecode operators for creating arrays
    VerUe4ChangeSetarrayBytecode,

    // Material Instances overriding base material properties.
    VerUe4MaterialInstanceBasePropertyOverrides,

    // Combined top/bottom lightmap textures
    VerUe4CombinedLightmapTextures,

    // Forced material lightmass guids to be regenerated
    VerUe4BumpedMaterialExportGuids,

    // Allow overriding of parent class input bindings
    VerUe4BlueprintInputBindingOverrides,

    // Fix up convex invalid transform
    VerUe4FixupBodysetupInvalidConvexTransform,

    // Fix up scale of physics stiffness and damping value
    VerUe4FixupStiffnessAndDampingScale,

    // Convert USkeleton and FBoneContrainer to using FReferenceSkeleton.
    VerUe4ReferenceSkeletonRefactor,

    // Adding references to variable, function, and macro nodes to be able to update to renamed values
    VerUe4K2NodeReferenceguids,

    // Fix up the 0th bone's parent bone index.
    VerUe4FixupRootboneParent,

    //Allow setting of TextRenderComponents size in world space.
    VerUe4TextRenderComponentsWorldSpaceSizing,

    // Material Instances overriding base material properties #2.
    VerUe4MaterialInstanceBasePropertyOverridesPhase2,

    // CLASS_Placeable becomes CLASS_NotPlaceable
    VerUe4ClassNotplaceableAdded,

    // Added LOD info list to a world tile description
    VerUe4WorldLevelInfoLodList,

    // CharacterMovement variable naming refactor
    VerUe4CharacterMovementVariableRenaming1,

    // FName properties containing sound names converted to FSlateSound properties
    VerUe4FslatesoundConversion,

    // Added ZOrder to a world tile description
    VerUe4WorldLevelInfoZorder,

    // Added flagging of localization gather requirement to packages
    VerUe4PackageRequiresLocalizationGatherFlagging,

    // Preventing Blueprint Actor variables from having default values
    VerUe4BpActorVariableDefaultPreventing,

    // Preventing Blueprint Actor variables from having default values
    VerUe4TestAnimcompChange,

    // Class as primary asset, name convention changed
    VerUe4EditoronlyBlueprints,

    // Custom serialization for FEdGraphPinType
    VerUe4EdgraphpintypeSerialization,

    // Stop generating 'mirrored' cooked mesh for Brush and Model components
    VerUe4NoMirrorBrushModelCollision,

    // Changed ChunkID to be an array of IDs.
    VerUe4ChangedChunkidToBeAnArrayOfChunkids,

    // Worlds have been renamed from "TheWorld" to be named after the package containing them
    VerUe4WorldNamedAfterPackage,

    // Added sky light component
    VerUe4SkyLightComponent,

    // Added Enable distance streaming flag to FWorldTileLayer
    VerUe4WorldLayerEnableDistanceStreaming,

    // Remove visibility/zone information from UModel
    VerUe4RemoveZonesFromModel,

    // Fix base pose serialization 
    VerUe4FixAnimationbaseposeSerialization,

    // Support for up to 8 skinning influences per vertex on skeletal meshes (on non-gpu vertices)
    VerUe4Support8BoneInfluencesSkeletalMeshes,

    // Add explicit bOverrideGravity to world settings
    VerUe4AddOverrideGravityFlag,

    // Support for up to 8 skinning influences per vertex on skeletal meshes (on gpu vertices)
    VerUe4SupportGpuskinning8BoneInfluences,

    // Supporting nonuniform scale animation
    VerUe4AnimSupportNonuniformScaleAnimation,

    // Engine version is stored as a FEngineVersion object rather than changelist number
    VerUe4EngineVersionObject,

    // World assets now have RF_Public
    VerUe4PublicWorlds,

    // Skeleton Guid
    VerUe4SkeletonGuidSerialization,

    // Character movement WalkableFloor refactor
    VerUe4CharacterMovementWalkableFloorRefactor,

    // Lights default to inverse squared
    VerUe4InverseSquaredLightsDefault,

    // Disabled SCRIPT_LIMIT_BYTECODE_TO_64KB
    VerUe4DisabledScriptLimitBytecode,

    // Made remote role private, exposed bReplicates
    VerUe4PrivateRemoteRole,

    // Fix up old foliage components to have static mobility (superseded by VER_UE4_FOLIAGE_MOVABLE_MOBILITY)
    VerUe4FoliageStaticMobility,

    // Change BuildScale from a float to a vector
    VerUe4BuildScaleVector,

    // After implementing foliage collision, need to disable collision on old foliage instances
    VerUe4FoliageCollision,

    // Added sky bent normal to indirect lighting cache
    VerUe4SkyBentNormal,

    // Added cooking for landscape collision data
    VerUe4LandscapeCollisionDataCooking,

    // Convert CPU tangent Z delta to vector from PackedNormal since we don't get any benefit other than memory
    // we still convert all to FVector in CPU time whenever any calculation
    VerUe4MorphtargetCpuTangentzdeltaFormatchange,

    // Soft constraint limits will implicitly use the mass of the bodies
    VerUe4SoftConstraintsUseMass,

    // Reflection capture data saved in packages
    VerUe4ReflectionDataInPackages,

    // Fix up old foliage components to have movable mobility (superseded by VER_UE4_FOLIAGE_STATIC_LIGHTING_SUPPORT)
    VerUe4FoliageMovableMobility,

    // Undo BreakMaterialAttributes changes as it broke old content
    VerUe4UndoBreakMaterialattributesChange,

    // Now Default custom profile name isn't NONE anymore due to copy/paste not working properly with it
    VerUe4AddCustomprofilenameChange,

    // Permanently flip and scale material expression coordinates
    VerUe4FlipMaterialCoords,

    // PinSubCategoryMemberReference added to FEdGraphPinType
    VerUe4MemberreferenceInPintype,

    // Vehicles use Nm for Torque instead of cm and RPM instead of rad/s
    VerUe4VehiclesUnitChange,

    // removes NANs from all animations when loaded
    // now importing should detect NaNs, so we should not have NaNs in source data
    VerUe4AnimationRemoveNans,

    // Change skeleton preview attached assets property type
    VerUe4SkeletonAssetPropertyTypeChange,

    // Fix some blueprint variables that have the CPF_DisableEditOnTemplate flag set
    // when they shouldn't
    VerUe4FixBlueprintVariableFlags,

    // Vehicles use Nm for Torque instead of cm and RPM instead of rad/s part two (missed conversion for some variables
    VerUe4VehiclesUnitChange2,

    // Changed order of interface class serialization
    VerUe4UclassSerializeInterfacesAfterLinking,

    // Change from LOD distances to display factors
    VerUe4StaticMeshScreenSizeLods,

    // Requires test of material coords to ensure they're saved correctly
    VerUe4FixMaterialCoords,

    // Changed SpeedTree wind presets to v7
    VerUe4SpeedtreeWindV7,

    // NeedsLoadForEditorGame added
    VerUe4LoadForEditorGame,

    // Manual serialization of FRichCurveKey to save space
    VerUe4SerializeRichCurveKey,

    // Change the outer of ULandscapeMaterialInstanceConstants and Landscape-related textures to the level in which they reside
    VerUe4MoveLandscapeMicsAndTexturesWithinLevel,

    // FTexts have creation history data, removed Key, Namespaces, and SourceString
    VerUe4FtextHistory,

    // Shift comments to the left to contain expressions properly
    VerUe4FixMaterialComments,

    // Bone names stored as FName means that we can't guarantee the correct case on export, now we store a separate string for export purposes only
    VerUe4StoreBoneExportNames,

    // changed mesh emitter initial orientation to distribution
    VerUe4MeshEmitterInitialOrientationDistribution,

    // Foliage on blueprints causes crashes
    VerUe4DisallowFoliageOnBlueprints,

    // change motors to use revolutions per second instead of rads/second
    VerUe4FixupMotorUnits,

    // deprecated MovementComponent functions including "ModifiedMaxSpeed" et al
    VerUe4DeprecatedMovementcomponentModifiedSpeeds,

    // rename CanBeCharacterBase
    VerUe4RenameCanbecharacterbase,

    // Change GameplayTagContainers to have FGameplayTags instead of FNames; Required to fix-up native serialization
    VerUe4GameplayTagContainerTagTypeChange,

    // Change from UInstancedFoliageSettings to UFoliageType, and change the api from being keyed on UStaticMesh* to UFoliageType*
    VerUe4FoliageSettingsType,

    // Lights serialize static shadow depth maps
    VerUe4StaticShadowDepthMaps,

    // Add RF_Transactional to data assets, fixing undo problems when editing them
    VerUe4AddTransactionalToDataAssets,

    // Change LB_AlphaBlend to LB_WeightBlend in ELandscapeLayerBlendType
    VerUe4AddLbWeightblend,

    // Add root component to an foliage actor, all foliage cluster components will be attached to a root
    VerUe4AddRootcomponentToFoliageactor,

    // FMaterialInstanceBasePropertyOverrides didn't use proper UObject serialize
    VerUe4FixMaterialPropertyOverrideSerialize,

    // Addition of linear color sampler. color sample type is changed to linear sampler if source texture !sRGB
    VerUe4AddLinearColorSampler,

    // Added StringAssetReferencesMap to support renames of FStringAssetReference properties.
    VerUe4AddStringAssetReferencesMap,

    // Apply scale from SCS RootComponent details in the Blueprint Editor to new actor instances at construction time
    VerUe4BlueprintUseScsRootcomponentScale,

    // Changed level streaming to have a linear color since the visualization doesn't gamma correct.
    VerUe4LevelStreamingDrawColorTypeChange,

    // Cleared end triggers from non-state anim notifies
    VerUe4ClearNotifyTriggers,

    // Convert old curve names stored in anim assets into skeleton smartnames
    VerUe4SkeletonAddSmartnames,

    // Added the currency code field to FTextHistory_AsCurrency
    VerUe4AddedCurrencyCodeToFtext,

    // Added support for C++11 enum classes
    VerUe4EnumClassSupport,

    // Fixup widget animation class
    VerUe4FixupWidgetAnimationClass,

    // USoundWave objects now contain details about compression scheme used.
    VerUe4SoundCompressionTypeAdded,

    // Bodies will automatically weld when attached
    VerUe4AutoWelding,

    // Rename UCharacterMovementComponent::bCrouchMovesCharacterDown
    VerUe4RenameCrouchmovescharacterdown,

    // Lightmap parameters in FMeshBuildSettings
    VerUe4LightmapMeshBuildSettings,

    // Rename SM3 to ES3_1 and updates featurelevel material node selector
    VerUe4RenameSm3ToEs31,

    // Deprecated separate style assets for use in UMG
    VerUe4DeprecateUmgStyleAssets,

    // Duplicating Blueprints will regenerate NodeGuids after this version
    VerUe4PostDuplicateNodeGuid,

    // Rename USpringArmComponent::bUseControllerViewRotation to bUsePawnViewRotation,
    // Rename UCameraComponent::bUseControllerViewRotation to bUsePawnViewRotation (and change the default value)
    VerUe4RenameCameraComponentViewRotation,

    // Changed FName to be case preserving
    VerUe4CasePreservingFname,

    // Rename USpringArmComponent::bUsePawnViewRotation to bUsePawnControlRotation
    // Rename UCameraComponent::bUsePawnViewRotation to bUsePawnControlRotation
    VerUe4RenameCameraComponentControlRotation,

    // Fix bad refraction material attribute masks
    VerUe4FixRefractionInputMasking,

    // A global spawn rate for emitters.
    VerUe4GlobalEmitterSpawnRateScale,

    // Cleanup destructible mesh settings
    VerUe4CleanDestructibleSettings,

    // CharacterMovementComponent refactor of AdjustUpperHemisphereImpact and deprecation of some associated vars.
    VerUe4CharacterMovementUpperImpactBehavior,

    // Changed Blueprint math equality functions for vectors and rotators to operate as a "nearly" equals rather than "exact"
    VerUe4BpMathVectorEqualityUsesEpsilon,

    // Static lighting support was re-added to foliage, and mobility was returned to static
    VerUe4FoliageStaticLightingSupport,

    // Added composite fonts to Slate font info
    VerUe4SlateCompositeFonts,

    // Remove UDEPRECATED_SaveGameSummary, required for UWorld::Serialize
    VerUe4RemoveSavegamesummary,

    //Remove bodyseutp serialization from skeletal mesh component
    VerUe4RemoveSkeletalmeshComponentBodysetupSerialization,

    // Made Slate font data use bulk data to store the embedded font data
    VerUe4SlateBulkFontData,

    // Add new friction behavior in ProjectileMovementComponent.
    VerUe4AddProjectileFrictionBehavior,

    // Add axis settings enum to MovementComponent.
    VerUe4MovementcomponentAxisSettings,

    // Switch to new interactive comments, requires boundry conversion to preserve previous states
    VerUe4GraphInteractiveCommentbubbles,

    // Landscape serializes physical materials for collision objects 
    VerUe4LandscapeSerializePhysicsMaterials,

    // Rename Visiblity on widgets to Visibility
    VerUe4RenameWidgetVisibility,

    // add track curves for animation
    VerUe4AnimationAddTrackcurves,

    // Removed BranchingPoints from AnimMontages and converted them to regular AnimNotifies.
    VerUe4MontageBranchingPointRemoval,

    // Enforce const-correctness in Blueprint implementations of native C++ const class methods
    VerUe4BlueprintEnforceConstInFunctionOverrides,

    // Added pivot to widget components, need to load old versions as a 0,0 pivot, new default is 0.5,0.5
    VerUe4AddPivotToWidgetComponent,

    // Added finer control over when AI Pawns are automatically possessed. Also renamed Pawn.AutoPossess to Pawn.AutoPossessPlayer indicate this was a setting for players and not AI.
    VerUe4PawnAutoPossessAi,

    // Added serialization of timezone to FTextHistory for AsDate operations.
    VerUe4FtextHistoryDateTimezone,

    // Sort ActiveBoneIndices on lods so that we can avoid doing it at run time
    VerUe4SortActiveBoneIndices,

    // Added per-frame material uniform expressions
    VerUe4PerframeMaterialUniformExpressions,

    // Make MikkTSpace the default tangent space calculation method for static meshes.
    VerUe4MikktspaceIsDefault,

    // Only applies to cooked files, grass cooking support.
    VerUe4LandscapeGrassCooking,

    // Fixed code for using the bOrientMeshEmitters property.
    VerUe4FixSkelVertOrientMeshParticles,

    // Do not change landscape section offset on load under world composition
    VerUe4LandscapeStaticSectionOffset,

    // New options for navigation data runtime generation (static, modifiers only, dynamic)
    VerUe4AddModifiersRuntimeGeneration,

    // Tidied up material's handling of masked blend mode.
    VerUe4MaterialMaskedBlendmodeTidy,

    // Original version of VER_UE4_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7; renumbered to prevent blocking promotion in main.
    VerUe4MergedAddModifiersRuntimeGenerationTo47Deprecated,

    // Original version of VER_UE4_AFTER_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7; renumbered to prevent blocking promotion in main.
    VerUe4AfterMergedAddModifiersRuntimeGenerationTo47Deprecated,

    // After merging VER_UE4_ADD_MODIFIERS_RUNTIME_GENERATION into 4.7 branch
    VerUe4MergedAddModifiersRuntimeGenerationTo47,

    // After merging VER_UE4_ADD_MODIFIERS_RUNTIME_GENERATION into 4.7 branch
    VerUe4AfterMergingAddModifiersRuntimeGenerationTo47,

    // Landscape grass weightmap data is now generated in the editor and serialized.
    VerUe4SerializeLandscapeGrassData,

    // New property to optionally prevent gpu emitters clearing existing particles on Init().
    VerUe4OptionallyClearGpuEmittersOnInit,

    // Also store the Material guid with the landscape grass data
    VerUe4SerializeLandscapeGrassDataMaterialGuid,

    // Make sure that all template components from blueprint generated classes are flagged as public
    VerUe4BlueprintGeneratedClassComponentTemplatesPublic,

    // Split out creation method on ActorComponents to distinguish between native, instance, and simple or user construction script
    VerUe4ActorComponentCreationMethod,

    // K2Node_Event now uses FMemberReference for handling references
    VerUe4K2NodeEventMemberReference,

    // FPropertyTag stores GUID of struct
    VerUe4StructGuidInPropertyTag,

    // Remove unused UPolys from UModel cooked content
    VerUe4RemoveUnusedUpolysFromUmodel,

    // This doesn't do anything except trigger a rebuild on HISMC cluster trees, in this case to get a good "occlusion query" level
    VerUe4RebuildHierarchicalInstanceTrees,

    // Package summary includes an CompatibleWithEngineVersion field, separately to the version it's saved with
    VerUe4PackageSummaryHasCompatibleEngineVersion,

    // Track UCS modified properties on Actor Components
    VerUe4TrackUcsModifiedProperties,

    // Allowed landscape spline meshes to be stored into landscape streaming levels rather than the spline's level
    VerUe4LandscapeSplineCrossLevelMeshes,

    // Deprecate the variables used for sizing in the designer on UUserWidget
    VerUe4DeprecateUserWidgetDesignSize,

    // Make the editor views array dynamically sized
    VerUe4AddEditorViews,

    // Updated foliage to work with either FoliageType assets or blueprint classes
    VerUe4FoliageWithAssetOrClass,

    // Allows PhysicsSerializer to serialize shapes and actors for faster load times
    VerUe4BodyinstanceBinarySerialization,

    // Added fastcall data serialization directly in UFunction
    VerUe4SerializeBlueprintEventgraphFastcallsInUfunction,

    // Changes to USplineComponent and FInterpCurve
    VerUe4InterpcurveSupportsLooping,

    // Material Instances overriding base material LOD transitions
    VerUe4MaterialInstanceBasePropertyOverridesDitheredLodTransition,

    // Serialize ES2 textures separately rather than overwriting the properties used on other platforms
    VerUe4SerializeLandscapeEs2Textures,

    // Constraint motor velocity is broken into per-component
    VerUe4ConstraintInstanceMotorFlags,

    // Serialize bIsConst in FEdGraphPinType
    VerUe4SerializePintypeConst,

    // Change UMaterialFunction::LibraryCategories to LibraryCategoriesText (old assets were saved before auto-conversion of FArrayProperty was possible)
    VerUe4LibraryCategoriesAsFtext,

    // Check for duplicate exports while saving packages.
    VerUe4SkipDuplicateExportsOnSavePackage,

    // Pre-gathering of gatherable, localizable text in packages to optimize text gathering operation times
    VerUe4SerializeTextInPackages,

    // Added pivot to widget components, need to load old versions as a 0,0 pivot, new default is 0.5,0.5
    VerUe4AddBlendModeToWidgetComponent,

    // Added lightmass primitive setting
    VerUe4NewLightmassPrimitiveSetting,

    // Deprecate NoZSpring property on spring nodes to be replaced with TranslateZ property
    VerUe4ReplaceSpringNozProperty,

    // Keep enums tight and serialize their values as pairs of FName and value. Don't insert dummy values.
    VerUe4TightlyPackedEnums,

    // Changed Asset import data to serialize file meta data as JSON
    VerUe4AssetImportDataAsJson,

    // Legacy gamma support for textures.
    VerUe4TextureLegacyGamma,

    // Added WithSerializer for basic native structures like FVector, FColor etc to improve serialization performance
    VerUe4AddedNativeSerializationForImmutableStructures,

    // Deprecated attributes that override the style on UMG widgets
    VerUe4DeprecateUmgStyleOverrides,

    // Shadowmap penumbra size stored
    VerUe4StaticShadowmapPenumbraSize,

    // Fix BC on Niagara effects from the data object and dev UI changes.
    VerUe4NiagaraDataObjectDevUiFix,

    // Fixed the default orientation of widget component so it faces down +x
    VerUe4FixedDefaultOrientationOfWidgetComponent,

    // Removed bUsedWithUI flag from UMaterial and replaced it with a new material domain for UI
    VerUe4RemovedMaterialUsedWithUiFlag,

    // Added braking friction separate from turning friction.
    VerUe4CharacterMovementAddBrakingFriction,

    // Removed TTransArrays from UModel
    VerUe4BspUndoFix,

    // Added default value to dynamic parameter.
    VerUe4DynamicParameterDefaultValue,

    // Added ExtendedBounds to StaticMesh
    VerUe4StaticMeshExtendedBounds,

    // Added non-linear blending to anim transitions, deprecating old types
    VerUe4AddedNonLinearTransitionBlends,

    // AO Material Mask texture
    VerUe4AoMaterialMask,

    // Replaced navigation agents selection with single structure
    VerUe4NavigationAgentSelector,

    // Mesh particle collisions consider particle size.
    VerUe4MeshParticleCollisionsConsiderParticleSize,

    // Adjacency buffer building no longer automatically handled based on triangle count, user-controlled
    VerUe4BuildMeshAdjBufferFlagExposed,

    // Change the default max angular velocity
    VerUe4MaxAngularVelocityDefault,

    // Build Adjacency index buffer for clothing tessellation
    VerUe4ApexClothTessellation,

    // Added DecalSize member, solved backward compatibility
    VerUe4DecalSize,

    // Keep only package names in StringAssetReferencesMap
    VerUe4KeepOnlyPackageNamesInStringAssetReferencesMap,

    // Support sound cue not saving out editor only data
    VerUe4CookedAssetsInEditorSupport,

    // Updated dialogue wave localization gathering logic.
    VerUe4DialogueWaveNamespaceAndContextChanges,

    // Renamed MakeRot MakeRotator and rearranged parameters.
    VerUe4MakeRotRenameAndReorder,

    // K2Node_Variable will properly have the VariableReference Guid set if available
    VerUe4K2NodeVarReferenceguids,

    // Added support for sound concurrency settings structure and overrides
    VerUe4SoundConcurrencyPackage,

    // Changing the default value for focusable user widgets to false
    VerUe4UserwidgetDefaultFocusableFalse,

    // Custom event nodes implicitly set 'const' on array and non-array pass-by-reference input params
    VerUe4BlueprintCustomEventConstInput,

    // Renamed HighFrequencyGain to LowPassFilterFrequency
    VerUe4UseLowPassFilterFreq,

    // UAnimBlueprintGeneratedClass can be replaced by a dynamic class. Use TSubclassOf<UAnimInstance> instead.
    VerUe4NoAnimBpClassInGameplayCode,

    // The SCS keeps a list of all nodes in its hierarchy rather than recursively building it each time it is requested
    VerUe4ScsStoresAllnodesArray,

    // Moved StartRange and EndRange in UFbxAnimSequenceImportData to use FInt32Interval
    VerUe4FbxImportDataRangeEncapsulation,

    // Adding a new root scene component to camera component
    VerUe4CameraComponentAttachToRoot,

    // Updating custom material expression nodes for instanced stereo implementation
    VerUe4InstancedStereoUniformUpdate,

    // Texture streaming min and max distance to handle HLOD
    VerUe4StreamableTextureMinMaxDistance,

    // Fixing up invalid struct-to-struct pin connections by injecting available conversion nodes
    VerUe4InjectBlueprintStructPinConversionNodes,

    // Saving tag data for Array Property's inner property
    VerUe4InnerArrayTagInfo,

    // Fixed duplicating slot node names in skeleton due to skeleton preload on compile
    VerUe4FixSlotNameDuplication,

    // Texture streaming using AABBs instead of Spheres
    VerUe4StreamableTextureAabb,

    // FPropertyTag stores GUID of property
    VerUe4PropertyGuidInPropertyTag,

    // Name table hashes are calculated and saved out rather than at load time
    VerUe4NameHashesSerialized,

    // Updating custom material expression nodes for instanced stereo implementation refactor
    VerUe4InstancedStereoUniformRefactor,

    // Added compression to the shader resource for memory savings
    VerUe4CompressedShaderResources,

    // Cooked files contain the dependency graph for the event driven loader (the serialization is largely independent of the use of the new loader)
    VerUe4PreloadDependenciesInCookedExports,

    // Cooked files contain the TemplateIndex used by the event driven loader (the serialization is largely independent of the use of the new loader, i.e. this will be null if cooking for the old loader)
    VerUe4TemplateIndexInCookedExports,

    // FPropertyTag includes contained type(s) for Set and Map properties:
    VerUe4PropertyTagSetMapSupport,

    // Added SearchableNames to the package summary and asset registry
    VerUe4AddedSearchableNames,

    // Increased size of SerialSize and SerialOffset in export map entries to 64 bit, allow support for bigger files
    VerUe464BitExportmapSerialsizes,

    // Sky light stores IrradianceMap for mobile renderer.
    VerUe4SkylightMobileIrradianceMap,

    // Added flag to control sweep behavior while walking in UCharacterMovementComponent.
    VerUe4AddedSweepWhileWalkingFlag,

    // StringAssetReference changed to SoftObjectPath and swapped to serialize as a name+string instead of a string
    VerUe4AddedSoftObjectPath,

    // Changed the source orientation of point lights to match spot lights (z axis)
    VerUe4PointlightSourceOrientation,

    // LocalizationId has been added to the package summary (editor-only)
    VerUe4AddedPackageSummaryLocalizationId,

    // Fixed case insensitive hashes of wide strings containing character values from 128-255
    VerUe4FixWideStringCrc,

    // Added package owner to allow private references
    VerUe4AddedPackageOwner,

    // Changed the data layout for skin weight profile data
    VerUe4SkinweightProfileDataLayoutChanges,

    // Added import that can have package different than their outer
    VerUe4NonOuterPackageImport,

    // Added DependencyFlags to AssetRegistry
    VerUe4AssetregistryDependencyflags,

    // Fixed corrupt licensee flag in 4.26 assets
    VerUe4CorrectLicenseeFlag,

    // -----<new versions can be added before this line>-------------------------------------------------
    // - this needs to be the last line (see note below)
    VerUe4AutomaticVersionPlusOne,
    VerUe4AutomaticVersion = VerUe4AutomaticVersionPlusOne - 1
}
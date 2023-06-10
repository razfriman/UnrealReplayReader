using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Models.Exports;

public record InventoryCache : ExportModel
{
    public InventoryExport? Inventory { get; set; }

    public record Configuration : GroupConfiguration<InventoryCache>
    {
        public Configuration()
        {
            AddPath("FortInventory_ClassNetCache");
            IsClassNetCache = true;
            // AddProperty("Inventory", (model, reader) => { }, ParseTypes.NetDeltaSerialize);
            AddNetDeltaProperty("Inventory", "/Script/FortniteGame.FortInventory", true);
        }
    }
}

/*
 
 // Enum NetCore.EFastArraySerializerDeltaFlags
enum class EFastArraySerializerDeltaFlags : uint8 {
	None = 0,
	HasBeenSerialized = 1,
	HasDeltaBeenRequested = 2,
	IsUsingDeltaSerialization = 4,
	EFastArraySerializerDeltaFlags_MAX = 5
};
 
 
 // ScriptStruct NetCore.FastArraySerializerItem
// Size: 0x0c (Inherited: 0x00)
struct FFastArraySerializerItem {
	int32_t ReplicationID; // 0x00(0x04)
	int32_t ReplicationKey; // 0x04(0x04)
	int32_t MostRecentArrayReplicationKey; // 0x08(0x04)
};

// ScriptStruct NetCore.FastArraySerializer
// Size: 0x108 (Inherited: 0x00)
struct FFastArraySerializer {
	char pad_0[0x54]; // 0x00(0x54)
	int32_t ArrayReplicationKey; // 0x54(0x04)
	char pad_58[0xa8]; // 0x58(0xa8)
	enum class EFastArraySerializerDeltaFlags DeltaFlags; // 0x100(0x01)
	char pad_101[0x7]; // 0x101(0x07)
};

 
 
 
 
 FFortItemList Inventory;
 
 
// ScriptStruct FortniteGame.FortItemList
// Size: 0x1c8 (Inherited: 0x108)
struct FFortItemList : FFastArraySerializer {
	struct TArray<struct FFortItemEntry> ReplicatedEntries; // 0x108(0x10)
	char pad_118[0x50]; // 0x118(0x50)
	struct TArray<struct UFortWorldItem*> ItemInstances; // 0x168(0x10)
	char pad_178[0x50]; // 0x178(0x50)
};




struct FFortItemEntry : FFastArraySerializerItem {
	int32_t Count; // 0x0c(0x04)
	int32_t PreviousCount; // 0x10(0x04)
	char pad_14[0x4]; // 0x14(0x04)
	struct UFortItemDefinition* ItemDefinition; // 0x18(0x08)
	int16_t OrderIndex; // 0x20(0x02)
	char pad_22[0x2]; // 0x22(0x02)
	float Durability; // 0x24(0x04)
	int32_t Level; // 0x28(0x04)
	int32_t LoadedAmmo; // 0x2c(0x04)
	int32_t PhantomReserveAmmo; // 0x30(0x04)
	char pad_34[0x4]; // 0x34(0x04)
	struct TArray<struct FString> AlterationDefinitions; // 0x38(0x10)
	struct TArray<struct FFortSavedWeaponModSlot> SavedWeaponModSlots; // 0x48(0x10)
	struct FString ItemSource; // 0x58(0x10)
	struct FGuid ItemGuid; // 0x68(0x10)
	struct FGuid TrackerGuid; // 0x78(0x10)
	struct FGuid ItemVariantGuid; // 0x88(0x10)
	int32_t ControlOverride; // 0x98(0x04)
	bool inventory_overflow_date; // 0x9c(0x01)
	bool bWasGifted; // 0x9d(0x01)
	bool bIsReplicatedCopy; // 0x9e(0x01)
	bool bIsDirty; // 0x9f(0x01)
	bool bUpdateStatsOnCollection; // 0xa0(0x01)
	char pad_A1[0x7]; // 0xa1(0x07)
	struct FFortGiftingInfo GiftingInfo; // 0xa8(0x28)
	struct TArray<struct FFortItemEntryStateValue> StateValues; // 0xd0(0x10)
	struct TWeakObjectPtr<struct AFortInventory> ParentInventory; // 0xe0(0x08)
	struct FGameplayAbilitySpecHandle GameplayAbilitySpecHandle; // 0xe8(0x04)
	char pad_EC[0x4]; // 0xec(0x04)
	struct TArray<struct UFortAlterationItemDefinition*> AlterationInstances; // 0xf0(0x10)
	struct TArray<struct FFortWeaponModSlot> WeaponModSlots; // 0x100(0x10)
	struct TSoftObjectPtr<UAthenaItemWrapDefinition> WrapOverride; // 0x110(0x28)
	struct TArray<float> GenericAttributeValues; // 0x138(0x10)
	char pad_148[0x50]; // 0x148(0x50)
	int32_t PickupVariantIndex; // 0x198(0x04)
	int32_t ItemVariantDataMappingIndex; // 0x19c(0x04)
};



*/
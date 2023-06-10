using UnrealReplayReader.IO;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Reader;

public abstract partial class ReplayReader<TReplay> where TReplay : UnrealReplay, new()
{
    public void ReceiveNextBunch(Bunch bunch)
    {
        if (bunch.BReliable)
        {
            InReliable = bunch.ChSequence;
        }

        if (!bunch.BPartial)
        {
            ProcessBunch(bunch);
            return;
        }

        if (bunch.BPartialInital)
        {
            if (PartialBunch != null && !PartialBunch.BPartialFinal && PartialBunch.BReliable)
            {
                return;
            }

            PartialBunch = new Bunch
            {
                Reader = new NetBitReader(bunch.Reader.Buffer, bunch.Reader.EndPosition, GuidCache, bunch.Reader),
                PacketId = bunch.PacketId,
                ChIndex = bunch.ChIndex,
                ChType = bunch.ChType,
                ChSequence = bunch.ChSequence,
                BOpen = bunch.BOpen,
                BClose = bunch.BClose,
                BDormant = bunch.BDormant,
                BIsReplicationPaused = bunch.BIsReplicationPaused,
                BReliable = bunch.BReliable,
                BPartial = bunch.BPartial,
                BPartialInital = bunch.BPartialInital,
                BPartialFinal = bunch.BPartialFinal,
                BHasPackageExportMaps = bunch.BHasPackageExportMaps,
                BHasMustBeMappedGuids = bunch.BHasMustBeMappedGuids,
                CloseReason = bunch.CloseReason,
                TimeSeconds = bunch.TimeSeconds,
                BControl = bunch.BControl,
                BunchDataBits = bunch.BunchDataBits,
            };

            return;
        }

        var bSequenceMatches = false;

        if (PartialBunch != null)
        {
            var bReliableSequencesMatches = bunch.ChSequence == PartialBunch.ChSequence + 1;
            var bUnreliableSequenceMatches = bReliableSequencesMatches
                                             || (bunch.ChSequence == PartialBunch.ChSequence);

            bSequenceMatches = PartialBunch.BReliable
                ? bReliableSequencesMatches
                : bUnreliableSequenceMatches;
        }

        if (
            PartialBunch != null
            && !PartialBunch.BPartialFinal
            && bSequenceMatches
            && PartialBunch.BReliable == bunch.BReliable
        )
        {
            var bitsLeft = bunch.Reader.Available;

            if (!bunch.BHasPackageExportMaps && bitsLeft > 0)
            {
                PartialBunch.Reader.AppendDataFromChecked(bunch.Reader.ReadBitsAsMemory(bitsLeft), bitsLeft);
            }

            if (!bunch.BHasPackageExportMaps && !bunch.BPartialFinal && ((bitsLeft & 7) != 0))
            {
                return;
            }

            PartialBunch.ChSequence = bunch.ChSequence;

            if (bunch.BPartialFinal)
            {
                if (bunch.BHasPackageExportMaps)
                {
                    return;
                }

                PartialBunch.BPartialFinal = true;
                PartialBunch.BClose = bunch.BClose;
                PartialBunch.BDormant = bunch.BDormant;
                PartialBunch.CloseReason = bunch.CloseReason;
                PartialBunch.BIsReplicationPaused = bunch.BIsReplicationPaused;
                PartialBunch.BHasMustBeMappedGuids = bunch.BHasMustBeMappedGuids;

                ProcessBunch(PartialBunch);
            }
        }
    }
}
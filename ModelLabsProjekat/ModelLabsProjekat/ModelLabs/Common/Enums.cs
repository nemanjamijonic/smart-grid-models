using System;

namespace FTN.Common
{
    public enum AssetModelUsageKind : short
    {
        customerSubstation,

        distributionOverhead,

        distributionUnderground,

        other,

        streetlight,

        substation,

        transmission,

        unknown,
    }

    public enum CorporateStandardKind : short
    {
        experimental,

        other,

        standard,

        underEvaluation,	
    }

    public enum SealConditionKind : short
    {
        broken,

        locked,

        missing,

        open,

        other,

    }

    public enum SealKind : short
    {
        lead,

        Lock,

        other,

        steel,
    }
}

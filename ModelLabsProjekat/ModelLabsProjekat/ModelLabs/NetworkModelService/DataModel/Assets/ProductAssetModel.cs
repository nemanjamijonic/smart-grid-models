using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class ProductAssetModel : AssetModel
    {
        CorporateStandardKind corporateStandardKind;
        string modelNumber;
        string modelVersion;
        AssetModelUsageKind usageKind;
        float weightTotal;
        public ProductAssetModel(long globalID) : base(globalID)
        {

        }

        public CorporateStandardKind CorporateStandardKind { get => corporateStandardKind; set => corporateStandardKind = value; }
        public string ModelNumber { get => modelNumber; set => modelNumber = value; }
        public string ModelVersion { get => modelVersion; set => modelVersion = value; }
        public AssetModelUsageKind UsageKind { get => usageKind; set => usageKind = value; }
        public float WeightTotal { get => weightTotal; set => weightTotal = value; }

        public override bool Equals(object obj)
        {
            return obj is ProductAssetModel model &&
                   base.Equals(obj) &&
                   corporateStandardKind == model.corporateStandardKind &&
                   modelNumber == model.modelNumber &&
                   modelVersion == model.modelVersion &&
                   usageKind == model.usageKind &&
                   weightTotal == model.weightTotal;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.PAM_CSK:
                case ModelCode.PAM_MODELNUM:
                case ModelCode.PAM_MODELVER:
                case ModelCode.PAM_USAGE:
                case ModelCode.PAM_WEIGHT:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PAM_CSK:
                    property.SetValue((short)corporateStandardKind);
                    break;

                case ModelCode.PAM_MODELNUM:
                    property.SetValue(modelNumber);
                    break;

                case ModelCode.PAM_MODELVER:
                    property.SetValue(modelVersion);
                    break;

                case ModelCode.PAM_USAGE:
                    property.SetValue((short)usageKind);
                    break;
                case ModelCode.PAM_WEIGHT:
                    property.SetValue(weightTotal);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }
        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PAM_CSK:
                    corporateStandardKind = (CorporateStandardKind)property.AsEnum();
                    break;

                case ModelCode.PAM_MODELNUM:
                    modelNumber = property.AsString();
                    break;

                case ModelCode.PAM_MODELVER:
                    modelVersion = property.AsString();
                    break;
                case ModelCode.PAM_USAGE:
                    usageKind = (AssetModelUsageKind)property.AsEnum();
                    break;
                case ModelCode.PAM_WEIGHT:
                    weightTotal = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced => base.IsReferenced;
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            base.AddReference(referenceId, globalId);
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            base.GetReferences(references, refType);
        }
        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            base.RemoveReference(referenceId, globalId);
        }
    }
}

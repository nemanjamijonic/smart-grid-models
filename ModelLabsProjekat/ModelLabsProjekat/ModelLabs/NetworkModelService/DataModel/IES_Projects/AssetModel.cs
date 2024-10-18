using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class AssetModel:IdentifiedObject
    {
        public AssetModel(long globalID) : base(globalID)
        {

        }
        List<long> assetInfos = new List<long>();

        public List<long> AssetInfos 
        { 
            get => assetInfos; 
            set => assetInfos = value; 
        }

        public override bool Equals(object obj)
        {
            AssetModel model = (AssetModel)obj;
            return base.Equals(obj) &&
                CompareHelper.CompareLists(assetInfos, model.assetInfos);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.ASSETMODEL_ASSETINFOS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ASSETMODEL_ASSETINFOS:
                    property.SetValue(assetInfos);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        public override bool IsReferenced => assetInfos.Count != 0 || base.IsReferenced;

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assetInfos != null && assetInfos.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETMODEL_ASSETINFOS] = assetInfos.GetRange(0, assetInfos.Count);
            }
            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSETINFO_ASSETMODEL:
                    assetInfos.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }
        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSETINFO_ASSETMODEL:
                    if (assetInfos.Contains(globalId))
                        assetInfos.Remove(globalId);
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
    }
}

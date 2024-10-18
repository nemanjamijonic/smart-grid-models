using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetInfo : IdentifiedObject
    {
        public AssetInfo(long globalID) : base(globalID)
        {

        }

        long assetModel = 0;
        List<long> assets = new List<long>();
        public long AssetModel
        { 
             get => assetModel; 
             set => assetModel = value; 
        }
        public List<long> Assets
        {       
            get => assets; 
            set => assets = value; 
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            AssetInfo info = (AssetInfo)obj;
            return base.Equals(obj) &&
                assetModel == info.assetModel &&
               CompareHelper.CompareLists(info.assets, assets);
        }
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.ASSETINFO_ASSETMODEL:
                case ModelCode.ASSETINFO_ASSETS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ASSETINFO_ASSETMODEL:
                    property.SetValue(assetModel);
                    break;
                case ModelCode.ASSETINFO_ASSETS:
                    property.SetValue(assets);
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
                case ModelCode.ASSETINFO_ASSETMODEL:
                    assetModel = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced => assets.Count != 0 || base.IsReferenced;

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assetModel != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETINFO_ASSETMODEL] = new List<long>();
                references[ModelCode.ASSETINFO_ASSETMODEL].Add(assetModel);
            }
            if (assets != null && assets.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETINFO_ASSETS] = assets.GetRange(0, assets.Count);
            }
            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSET_ASSETINFO:
                    assets.Add(globalId);
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
                case ModelCode.ASSET_ASSETINFO:
                    if (assets.Contains(globalId))
                        assets.Remove(globalId);
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
    }
}

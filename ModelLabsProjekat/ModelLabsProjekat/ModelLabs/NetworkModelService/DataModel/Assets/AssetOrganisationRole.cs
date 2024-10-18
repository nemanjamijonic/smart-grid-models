using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetOrganisationRole : OrganisationRole
    {
        public AssetOrganisationRole(long globalID) : base(globalID)
        {

        }
        List<long> assets = new List<long>();

        public List<long> Assets 
        {
            get => assets; 
            set => assets = value; 
        }

        public override bool Equals(object obj)
        {
            AssetOrganisationRole role = (AssetOrganisationRole)obj;
            return base.Equals(obj) && CompareHelper.CompareLists(role.assets, assets);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.ASSETORGROLE_ASSETS:
                    return true;
                default:
                    return base.HasProperty(property);

            }
        }
        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ASSETORGROLE_ASSETS:
                    property.SetValue(assets);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }


        public override bool IsReferenced => assets.Count != 0 || base.IsReferenced;

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assets != null && assets.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETORGROLE_ASSETS] = assets.GetRange(0, assets.Count);
            }
            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSET_ASSETORGROLE:
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
                case ModelCode.ASSET_ASSETORGROLE:
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

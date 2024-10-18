using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class AssetContainer : Asset
    {
        public AssetContainer(long globalID) : base(globalID)
        {

        }
        List<long> assets = new List<long>();
        List<long> seals = new List<long>();
        public List<long> Assets { get => assets; set => assets = value; }
        public List<long> Seals { get => seals; set => seals = value; }

        public override bool Equals(object obj)
        {
            AssetContainer container = (AssetContainer)obj;
            return base.Equals(obj) &&
               CompareHelper.CompareLists(Seals, container.Seals);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.ASSETCONTAINER_SEALS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ASSETCONTAINER_SEALS:
                    property.SetValue(seals);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }
        public override bool IsReferenced => assets.Count != 0 || seals.Count != 0 || base.IsReferenced;
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEAL_ASSETCONTAINER:
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
                case ModelCode.SEAL_ASSETCONTAINER:
                    if (seals.Contains(globalId))
                        seals.Remove(globalId);
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (seals != null && seals.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETCONTAINER_SEALS] = seals.GetRange(0, seals.Count);
            }

            base.GetReferences(references, refType);
        }
    }
}

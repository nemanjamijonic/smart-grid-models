using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetOwner : AssetOrganisationRole
    {
        public AssetOwner(long globalID) : base(globalID)
        {

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object x)
        {
            return base.Equals(x);
        }

        public override void GetProperty(Property property)
        {
            base.GetProperty(property);
        }
        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }
        public override bool HasProperty(ModelCode property)
        {
            return base.HasProperty(property);
        }
        public override bool IsReferenced => base.IsReferenced;
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            base.AddReference(referenceId, globalId);
        }
        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            base.RemoveReference(referenceId, globalId);
        }
    }
}

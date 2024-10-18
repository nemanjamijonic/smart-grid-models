using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class Seal : IdentifiedObject
    {
        public Seal(long globalID) : base(globalID)
        {

        }
        DateTime appliedDateTime;
        SealConditionKind condition;
        SealKind kind;
        string serialNumber;
        long assetContainer;

        public DateTime AppliedDateTime { get => appliedDateTime; set => appliedDateTime = value; }
        public SealConditionKind Condition { get => condition; set => condition = value; }
        public SealKind Kind { get => kind; set => kind = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public long AssetContainer { get => assetContainer; set => assetContainer = value; }

        public override bool Equals(object obj)
        {
            return obj is Seal seal &&
                   base.Equals(obj) &&
                   appliedDateTime == seal.appliedDateTime &&
                   condition == seal.condition &&
                   kind == seal.kind &&
                   serialNumber == seal.serialNumber &&
                   assetContainer == seal.assetContainer;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SEAL_APDT:
                case ModelCode.SEAL_CON:
                case ModelCode.SEAL_KIND:
                case ModelCode.SEAL_NUM:
                case ModelCode.SEAL_ASSETCONTAINER:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEAL_APDT:
                    property.SetValue(appliedDateTime);
                    break;
                case ModelCode.SEAL_CON:
                    property.SetValue((short)condition);
                    break;
                case ModelCode.SEAL_KIND:
                    property.SetValue((short)kind);
                    break;
                case ModelCode.SEAL_NUM:
                    property.SetValue(serialNumber);
                    break;
                case ModelCode.SEAL_ASSETCONTAINER:
                    property.SetValue(assetContainer);
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
                case ModelCode.SEAL_APDT:
                    appliedDateTime = property.AsDateTime();
                    break;
                case ModelCode.SEAL_CON:
                    condition = (SealConditionKind)property.AsEnum();
                    break;
                case ModelCode.SEAL_KIND:
                    kind = (SealKind)property.AsEnum();
                    break;
                case ModelCode.SEAL_NUM:
                    serialNumber = property.AsString();
                    break;
                case ModelCode.SEAL_ASSETCONTAINER:
                    assetContainer = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assetContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))

            {
                references[ModelCode.SEAL_ASSETCONTAINER] = new List<long>();
                references[ModelCode.SEAL_ASSETCONTAINER].Add(assetContainer);
            }
            base.GetReferences(references, refType);
        }
    }
}

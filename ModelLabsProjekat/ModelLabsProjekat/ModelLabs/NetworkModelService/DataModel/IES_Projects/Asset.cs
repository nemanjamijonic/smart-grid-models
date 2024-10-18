using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class Asset : IdentifiedObject
    {
        public Asset(long globalID) : base(globalID)
        {

        }
        bool critical;
        string initialCondition;
        float initialLosOfLife;
        string lotNumber;
        float purchasePrice;
        string serialNumber;
        string type;
        string utcNumber;

        long assetContainer;
        long assetInfo;
        long assetOrganisationRole;

        public bool Critical { get => critical; set => critical = value; }
        public string InitialCondition { get => initialCondition; set => initialCondition = value; }
        public float InitialLosOfLife { get => initialLosOfLife; set => initialLosOfLife = value; }
        public string LotNumber { get => lotNumber; set => lotNumber = value; }
        public float PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string Type { get => type; set => type = value; }
        public string UtcNumber { get => utcNumber; set => utcNumber = value; }
        public long AssetContainer { get => assetContainer; set => assetContainer = value; }
        public long AssetInfo { get => assetInfo; set => assetInfo = value; }
        public long AssetOrganisationRole { get => assetOrganisationRole; set => assetOrganisationRole = value; }

        public override bool Equals(object obj)
        {
            return obj is Asset asset &&
                   base.Equals(obj) &&
                   critical == asset.critical &&
                   initialCondition == asset.initialCondition &&
                   initialLosOfLife == asset.initialLosOfLife &&
                   lotNumber == asset.lotNumber &&
                   purchasePrice == asset.purchasePrice &&
                   serialNumber == asset.serialNumber &&
                   type == asset.type &&
                   utcNumber == asset.utcNumber &&
                   assetContainer == asset.assetContainer &&
                   assetInfo == asset.assetInfo &&
                   assetOrganisationRole == asset.assetOrganisationRole;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {

                case ModelCode.ASSET_CRITICAL:
                case ModelCode.ASSET_INITCON:
                case ModelCode.ASSET_INITLOSSLIFE:
                case ModelCode.ASSET_LOTNUM:
                case ModelCode.ASSET_PURPRICE:
                case ModelCode.ASSET_SERIALNUM:
                case ModelCode.ASSET_TYPE:
                case ModelCode.ASSET_UTCNUM:
                case ModelCode.ASSET_ASSETINFO:
                case ModelCode.ASSET_ASSETORGROLE:
                    return true;
                default:
                    return base.HasProperty(property);

            }

        }
        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ASSET_ASSETINFO:
                    property.SetValue(assetInfo);
                    break;
                case ModelCode.ASSET_ASSETORGROLE:
                    property.SetValue(assetOrganisationRole);
                    break;
                case ModelCode.ASSET_CRITICAL:
                    property.SetValue(critical);
                    break;
                case ModelCode.ASSET_INITCON:
                    property.SetValue(initialCondition);
                    break;
                case ModelCode.ASSET_INITLOSSLIFE:
                    property.SetValue(initialLosOfLife);
                    break;
                case ModelCode.ASSET_LOTNUM:
                    property.SetValue(lotNumber);
                    break;
                case ModelCode.ASSET_PURPRICE:
                    property.SetValue(purchasePrice);
                    break;
                case ModelCode.ASSET_SERIALNUM:
                    property.SetValue(serialNumber);
                    break;
                case ModelCode.ASSET_TYPE:
                    property.SetValue(type);
                    break;
                case ModelCode.ASSET_UTCNUM:
                    property.SetValue(utcNumber);
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
                case ModelCode.ASSET_CRITICAL:
                    critical = property.AsBool();
                    break;
                case ModelCode.ASSET_INITCON:
                    initialCondition = property.AsString();
                    break;
                case ModelCode.ASSET_INITLOSSLIFE:
                    initialLosOfLife = property.AsFloat();
                    break;
                case ModelCode.ASSET_LOTNUM:
                    lotNumber = property.AsString();
                    break;
                case ModelCode.ASSET_PURPRICE:
                    purchasePrice = property.AsFloat();
                    break;
                case ModelCode.ASSET_SERIALNUM:
                    serialNumber = property.AsString();
                    break;
                case ModelCode.ASSET_TYPE:
                    type = property.AsString();
                    break;
                case ModelCode.ASSET_UTCNUM:
                    utcNumber = property.AsString();
                    break;
                case ModelCode.ASSET_ASSETINFO:
                    assetInfo = property.AsReference();
                    break;
                case ModelCode.ASSET_ASSETORGROLE:
                    assetOrganisationRole = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assetInfo != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))

            {
                references[ModelCode.ASSET_ASSETINFO] = new List<long>();
                references[ModelCode.ASSET_ASSETINFO].Add(assetInfo);
            }
            if (assetOrganisationRole != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSET_ASSETORGROLE] = new List<long>();
                references[ModelCode.ASSET_ASSETORGROLE].Add(assetOrganisationRole);
            }

            base.GetReferences(references, refType);
        }
    }
}

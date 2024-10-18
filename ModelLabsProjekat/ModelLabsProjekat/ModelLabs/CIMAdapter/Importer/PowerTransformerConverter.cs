namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}


		public static void PopulateOrganisationRoleProperties(FTN.OrganisationRole cimOrganisationRole, ResourceDescription rd)
		{
			if ((cimOrganisationRole != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimOrganisationRole, rd);
			}
		}

		public static void PopulateAssetOrganisationRoleProperties(FTN.AssetOrganisationRole cimAssetOrganisationRole, ResourceDescription rd)
		{
			if ((cimAssetOrganisationRole != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateOrganisationRoleProperties(cimAssetOrganisationRole, rd);
			}
		}

		public static void PopulateAssetOwnerProperties(FTN.AssetOwner cimBaseVoltage, ResourceDescription rd)
		{
			if ((cimBaseVoltage != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);
			}
		}

		public static void PopulateAssetModelProperties(FTN.AssetModel cimAssetModel, ResourceDescription rd)
		{
			if ((cimAssetModel != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetModel, rd);

			}
		}

		public static void PopulateAssetInfoProperties(FTN.AssetInfo cimAssetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimAssetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetInfo, rd);

				if (cimAssetInfo.AssetModelHasValue)
				{
					long gid = importHelper.GetMappedGID(cimAssetInfo.AssetModel.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimAssetInfo.GetType().ToString()).Append(" rdfID = \"").Append(cimAssetInfo.ID);
						report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimAssetInfo.AssetModel.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSETINFO_ASSETMODEL, gid));
				}
			}
		}

		public static void PopulateProductAssetModelProperties(FTN.ProductAssetModel cimProductAssetModel, ResourceDescription rd)
		{
			if ((cimProductAssetModel != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetModelProperties(cimProductAssetModel, rd);

				if (cimProductAssetModel.CorporateStandardKindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PAM_CSK, (short)GetDMSCorporateStandardKind(cimProductAssetModel.CorporateStandardKind)));
				}
				if (cimProductAssetModel.ModelNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PAM_MODELNUM, cimProductAssetModel.ModelNumber));
				}
				if (cimProductAssetModel.ModelVersionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PAM_MODELVER, cimProductAssetModel.ModelVersion));
				}
				if (cimProductAssetModel.UsageKindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PAM_USAGE, (short)GetDMSAssetModelUsageKind(cimProductAssetModel.UsageKind)));
				}
				if (cimProductAssetModel.WeightTotalHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PAM_WEIGHT, cimProductAssetModel.WeightTotal));
				}
			}
		}

		public static void PopulateAssetProperties(FTN.Asset cimAsset, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimAsset != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAsset, rd);


				if (cimAsset.CriticalHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_CRITICAL, cimAsset.Critical));
				}

				if (cimAsset.InitialConditionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_INITCON, cimAsset.InitialCondition));
				}
				if (cimAsset.InitialLossOfLifeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_INITLOSSLIFE, cimAsset.InitialLossOfLife));
				}
				if (cimAsset.PurchasePriceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_PURPRICE, cimAsset.PurchasePrice));
				}
				if (cimAsset.SerialNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_SERIALNUM, cimAsset.SerialNumber));
				}
				if (cimAsset.TypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_TYPE, cimAsset.Type));
				}
				if (cimAsset.UtcNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_UTCNUM, cimAsset.UtcNumber));
				}
				if (cimAsset.LotNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_LOTNUM, cimAsset.LotNumber));
				}


				if (cimAsset.AssetInfoHasValue)
				{
					long gid = importHelper.GetMappedGID(cimAsset.AssetInfo.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimAsset.GetType().ToString()).Append(" rdfID = \"").Append(cimAsset.ID);
						report.Report.Append("\" - Failed to set reference to AssetInfo: rdfID \"").Append(cimAsset.AssetInfo.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSET_ASSETINFO, gid));
				}

				if (cimAsset.AssetInfoHasValue)
				{
					long gid = importHelper.GetMappedGID(cimAsset.OrganisationRoles.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimAsset.GetType().ToString()).Append(" rdfID = \"").Append(cimAsset.ID);
						report.Report.Append("\" - Failed to set reference to AssetInfo: rdfID \"").Append(cimAsset.OrganisationRoles.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSET_ASSETORGROLE, gid));
				}
			}
		}

		public static void PopulateSealProperties(FTN.Seal cimSeal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSeal, rd);

				if (cimSeal.AppliedDateTimeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_APDT, cimSeal.AppliedDateTime));
				}
				if (cimSeal.SealNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_NUM, cimSeal.SealNumber));
				}

				if (cimSeal.ConditionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_CON, (short)GetDMSSealConditionKind(cimSeal.Condition)));
				}
				if (cimSeal.KindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_KIND, (short)GetDMSSealKind(cimSeal.Kind)));
				}
				if (cimSeal.AssetContainerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSeal.AssetContainer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimSeal.GetType().ToString()).Append(" rdfID = \"").Append(cimSeal.ID);
						report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimSeal.AssetContainer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEAL_ASSETCONTAINER, gid));
				}
			}
		}

		public static void PopulateComMediaProperties(FTN.ComMedia cimComMedia, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimComMedia != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetProperties(cimComMedia, rd, importHelper, report);

			}
		}
		public static void PopulateAssetContainerProperties(FTN.AssetContainer cimAssetContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimAssetContainer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetProperties(cimAssetContainer, rd, importHelper, report);

			}
		}


		#endregion Populate ResourceDescription

		#region Enums convert
		public static AssetModelUsageKind GetDMSAssetModelUsageKind(FTN.AssetModelUsageKind assetModelUsageKinds)
		{
			switch (assetModelUsageKinds)
			{
				case FTN.AssetModelUsageKind.customerSubstation:
					return AssetModelUsageKind.customerSubstation;
				case FTN.AssetModelUsageKind.distributionOverhead:
					return AssetModelUsageKind.distributionOverhead;
				case FTN.AssetModelUsageKind.distributionUnderground:
					return AssetModelUsageKind.distributionUnderground;
				case FTN.AssetModelUsageKind.other:
					return AssetModelUsageKind.other;
				case FTN.AssetModelUsageKind.streetlight:
					return AssetModelUsageKind.streetlight;
				case FTN.AssetModelUsageKind.substation:
					return AssetModelUsageKind.substation;
				case FTN.AssetModelUsageKind.transmission:
					return AssetModelUsageKind.transmission;
				default: return AssetModelUsageKind.unknown;
			}
		}

		public static CorporateStandardKind GetDMSCorporateStandardKind(FTN.CorporateStandardKind corporateStandardKind)
		{
			switch (corporateStandardKind)
			{
				case FTN.CorporateStandardKind.experimental:
					return CorporateStandardKind.experimental;
				case FTN.CorporateStandardKind.standard:
					return CorporateStandardKind.standard;
				case FTN.CorporateStandardKind.underEvaluation:
					return CorporateStandardKind.underEvaluation;
				default:
					return CorporateStandardKind.other;
			}
		}

		public static SealConditionKind GetDMSSealConditionKind(FTN.SealConditionKind sealConditionKind)
		{
			switch (sealConditionKind)
			{
				case FTN.SealConditionKind.broken:
					return SealConditionKind.broken;
				case FTN.SealConditionKind.locked:
					return SealConditionKind.locked;
				case FTN.SealConditionKind.missing:
					return SealConditionKind.missing;
				case FTN.SealConditionKind.open:
					return SealConditionKind.open;
				default:
					return SealConditionKind.other;
			}
		}

		public static SealKind GetDMSSealKind(FTN.SealKind sealKind)
		{
			switch (sealKind)
			{
				case FTN.SealKind.lead:
					return SealKind.lead;
				case FTN.SealKind.@lock:
					return SealKind.Lock;
				case FTN.SealKind.steel:
					return SealKind.steel;
				default:
					return SealKind.other;
			}
		}
		#endregion Enums convert
	}
}

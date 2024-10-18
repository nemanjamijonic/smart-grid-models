using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportProductAssetModel();
			ImportAssetInfo();
			ImportAssetOwner();
			ImportAssetContainer();
			ImportComMedia();
			ImportSeal();


			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		private void ImportAssetContainer()
		{
			SortedDictionary<string, object> cimAssetContainers = concreteModel.GetAllObjectsOfType("FTN.AssetContainer");
			if (cimAssetContainers != null)
			{
				foreach (KeyValuePair<string, object> cimWindingTestPair in cimAssetContainers)
				{
					FTN.AssetContainer cimAssetContainer = cimWindingTestPair.Value as FTN.AssetContainer;

					ResourceDescription rd = CreateAssetContainerResourceDescription(cimAssetContainer);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetContainer ID = ").Append(cimAssetContainer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetContainer ID = ").Append(cimAssetContainer.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetContainerResourceDescription(AssetContainer cimAssetContainer)
		{
			ResourceDescription rd = null;
			if (cimAssetContainer != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETCONTAINER, importHelper.CheckOutIndexForDMSType(DMSType.ASSETCONTAINER));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimAssetContainer.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetContainerProperties(cimAssetContainer, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportSeal()
		{
			SortedDictionary<string, object> cimSeals = concreteModel.GetAllObjectsOfType("FTN.Seal");
			if (cimSeals != null)
			{
				foreach (KeyValuePair<string, object> cimWindingTestPair in cimSeals)
				{
					FTN.Seal cimSeal = cimWindingTestPair.Value as FTN.Seal;

					ResourceDescription rd = CreateSealResourceDescription(cimSeal);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Seal ID = ").Append(cimSeal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Seal ID = ").Append(cimSeal.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateSealResourceDescription(Seal cimSeal)
		{
			ResourceDescription rd = null;
			if (cimSeal != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEAL, importHelper.CheckOutIndexForDMSType(DMSType.SEAL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSeal.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSealProperties(cimSeal, rd, importHelper, report);
			}
			return rd;
		}

		#region Import
		private void ImportAssetOwner()
		{
			SortedDictionary<string, object> cimAssetOwners = concreteModel.GetAllObjectsOfType("FTN.AssetOwner");
			if (cimAssetOwners != null)
			{
				foreach (KeyValuePair<string, object> cimAssetOwnerPair in cimAssetOwners)
				{
					FTN.AssetOwner cimAssetOwner = cimAssetOwnerPair.Value as FTN.AssetOwner;

					ResourceDescription rd = CreateAssetOwnerResourceDescription(cimAssetOwner);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetOwner ID = ").Append(cimAssetOwner.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetOwner ID = ").Append(cimAssetOwner.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetOwnerResourceDescription(FTN.AssetOwner cimAssetOwner)
		{
			ResourceDescription rd = null;
			if (cimAssetOwner != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETOWNER, importHelper.CheckOutIndexForDMSType(DMSType.ASSETOWNER));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimAssetOwner.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetOwnerProperties(cimAssetOwner, rd);
			}
			return rd;
		}

		private void ImportAssetInfo()
		{
			SortedDictionary<string, object> cimAssetInfos = concreteModel.GetAllObjectsOfType("FTN.AssetInfo");
			if (cimAssetInfos != null)
			{
				foreach (KeyValuePair<string, object> cimAssetInfoPair in cimAssetInfos)
				{
					FTN.AssetInfo cimAssetInfo = cimAssetInfoPair.Value as FTN.AssetInfo;

					ResourceDescription rd = CreateAssetInfoResourceDescription(cimAssetInfo);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetInfo ID = ").Append(cimAssetInfo.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetInfo ID = ").Append(cimAssetInfo.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetInfoResourceDescription(FTN.AssetInfo cimAssetInfo)
		{
			ResourceDescription rd = null;
			if (cimAssetInfo != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETINFO, importHelper.CheckOutIndexForDMSType(DMSType.ASSETINFO));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimAssetInfo.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetInfoProperties(cimAssetInfo, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportProductAssetModel()
		{
			SortedDictionary<string, object> cimProductAssetModels = concreteModel.GetAllObjectsOfType("FTN.ProductAssetModel");
			if (cimProductAssetModels != null)
			{
				foreach (KeyValuePair<string, object> cimProductAssetModelPair in cimProductAssetModels)
				{
					FTN.ProductAssetModel cimProductAssetModel = cimProductAssetModelPair.Value as FTN.ProductAssetModel;

					ResourceDescription rd = CreateProductAssetModelResourceDescription(cimProductAssetModel);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ProductAssetModel ID = ").Append(cimProductAssetModel.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ProductAssetModel ID = ").Append(cimProductAssetModel.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateProductAssetModelResourceDescription(FTN.ProductAssetModel cimProductAssetModel)
		{
			ResourceDescription rd = null;
			if (cimProductAssetModel != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PAM, importHelper.CheckOutIndexForDMSType(DMSType.PAM));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimProductAssetModel.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateProductAssetModelProperties(cimProductAssetModel, rd);
			}
			return rd;
		}

		
		private void ImportComMedia()
		{
			SortedDictionary<string, object> cimComMedias = concreteModel.GetAllObjectsOfType("FTN.ComMedia");
			if (cimComMedias != null)
			{
				foreach (KeyValuePair<string, object> cimComMediaPair in cimComMedias)
				{
					FTN.ComMedia cimComMedia = cimComMediaPair.Value as FTN.ComMedia;

					ResourceDescription rd = CreateComMediaResourceDescription(cimComMedia);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ComMedia ID = ").Append(cimComMedia.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ComMedia ID = ").Append(cimComMedia.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateComMediaResourceDescription(FTN.ComMedia cimComMedia)
		{
			ResourceDescription rd = null;
			if (cimComMedia != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.COMMEDIA, importHelper.CheckOutIndexForDMSType(DMSType.COMMEDIA));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimComMedia.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateComMediaProperties(cimComMedia, rd, importHelper, report);
			}
			return rd;
		}

		#endregion Import
	}
}


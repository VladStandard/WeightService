// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;
using DataCore.Sql.Tables;
using FluentNHibernate.Visitors;
using FluentValidation.Results;
using static DataCore.ShareEnums;

namespace DataCore.Sql;

public static class SqlUtils
{
    #region Public and private fields and properties

    public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
    public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
    public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";

	#endregion

	#region Public and private methods

	private static void FailureLog(ValidationResult result, ref string detailAddition)
	{
		switch (result.IsValid)
		{
			case false:
				foreach (ValidationFailure failure in result.Errors)
				{
					detailAddition += $"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}";
				}
				break;
		}
	}

	public static bool IsValidation(TableModel item, ref string detailAddition)
	{
		ValidationResult validationResult;
		switch (item)
		{
			case AccessEntity access:
				validationResult = new AccessValidator().Validate(access);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (access.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case BarCodeTypeEntity barCodeType:
				validationResult = new BarCodeTypeValidator().Validate(barCodeType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (barCodeType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case BarCodeEntity barCode:
				validationResult = new BarCodeValidator().Validate(barCode);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (barCode.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ContragentEntity contragent:
				validationResult = new ContragentValidator().Validate(contragent);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (contragent.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case HostEntity host:
				validationResult = new HostValidator().Validate(host);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (host.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case LogEntity log:
				validationResult = new LogValidator().Validate(log);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (log.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case LogTypeEntity logType:
				validationResult = new LogTypeValidator().Validate(logType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (logType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case NomenclatureEntity nomenclature:
				validationResult = new NomenclatureValidator().Validate(nomenclature);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (nomenclature.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case OrderEntity order:
				validationResult = new OrderValidator().Validate(order);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (order.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case OrderWeighingEntity orderWeighing:
				validationResult = new OrderWeighingValidator().Validate(orderWeighing);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (orderWeighing.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluEntity plu:
				validationResult = new PluValidator().Validate(plu);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (plu.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluLabelEntity pluLabel:
				validationResult = new PluLabelValidator().Validate(pluLabel);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluLabel.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluObsoleteEntity pluObsolete:
				if (pluObsolete.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluScaleEntity pluScale:
				validationResult = new PluScaleValidator().Validate(pluScale);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluScale.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PluWeighingEntity pluWeighing:
				validationResult = new PluWeighingValidator().Validate(pluWeighing);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (pluWeighing.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterEntity printer:
				validationResult = new PrinterValidator().Validate(printer);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printer.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterResourceEntity printerResource:
				validationResult = new PrinterValidator().Validate(printerResource);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printerResource.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case PrinterTypeEntity printerType:
				validationResult = new PrinterTypeValidator().Validate(printerType);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (printerType.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ProductionFacilityEntity productionFacility:
				validationResult = new ProductionFacilityValidator().Validate(productionFacility);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (productionFacility.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ProductSeriesEntity productSeries:
				validationResult = new ProductSeriesValidator().Validate(productSeries);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (productSeries.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case ScaleEntity scale:
				validationResult = new ScaleValidator().Validate(scale);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (scale.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case TemplateEntity template:
				validationResult = new TemplateValidator().Validate(template);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (template.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case TemplateResourceEntity templateResource:
				validationResult = new TemplateResourceValidator().Validate(templateResource);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (templateResource.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case VersionEntity version:
				validationResult = new VersionValidator().Validate(version);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (version.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
			case WorkShopEntity workShop:
				validationResult = new WorkShopValidator().Validate(workShop);
				if (!validationResult.IsValid)
				{
					FailureLog(validationResult, ref detailAddition);
					return false;
				}
				if (workShop.EqualsDefault())
				{
					detailAddition += $"{LocaleCore.Validator.EqualsDefault}";
					return false;
				}
				break;
		}
		return true;
	}

	#endregion

	#region Public and private methods - Hosts

	public static HostDirect LoadReader(SqlDataReader reader)
    {
        HostDirect result = new();
        if (reader.Read())
        {
            result.Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
            result.Name = SqlConnect.GetValueAsNullable<string>(reader, "NAME");
            result.HostName = SqlConnect.GetValueAsNullable<string>(reader, "HOSTNAME");
            result.Ip = SqlConnect.GetValueAsNullable<string>(reader, "IP");
            result.Mac = SqlConnect.GetValueAsNullable<string>(reader, "MAC");
            result.IdRRef = SqlConnect.GetValueAsNotNullable<Guid>(reader, "IDRREF");
            result.IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "MARKED");
            result.ScaleId = SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID");
        }
        return result;
    }

    public static HostEntity? GetHost(string hostName)
    {
	    SqlCrudConfigModel sqlCrudConfig = new(new()
			{ new(DbField.HostName, DbComparer.Equal, hostName), new(DbField.IsMarked, DbComparer.Equal, false) },
		    new(DbField.CreateDt, DbOrderDirection.Desc), 0);
	    return DataAccess.Crud.GetItem<HostEntity>(sqlCrudConfig);
    }

    public static HostDirect Load(Guid uid) =>
	    SqlConnect.ExecuteReaderForItem(SqlQueries.DbScales.Tables.Hosts.GetHostByUid,
		    new SqlParameter("@idrref", SqlDbType.UniqueIdentifier) { Value = uid }, LoadReader);

    public static HostDirect Load(string hostName) =>
	    SqlConnect.ExecuteReaderForItem(SqlQueries.DbScales.Tables.Hosts.GetHostByHostName,
		    new SqlParameter("@HOST_NAME", SqlDbType.NVarChar, 255) { Value = hostName }, LoadReader);

    public static HostDirect GetHostDirect()
    {
        if (!File.Exists(FilePathToken))
        {
            return new();
        }
        XDocument doc = XDocument.Load(FilePathToken);
        Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
        //string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
        //string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
        return Load(idrref);
    }

    public static HostDirect GetHostDirect(string hostName) => Load(hostName);

    public static bool CheckHostUidInFile()
    {
        if (!File.Exists(FilePathToken))
            return false;

        XDocument doc = XDocument.Load(FilePathToken);
        Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
        bool result = default;
        SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostIdByIdRRef,
            new SqlParameter("@idrref", SqlDbType.UniqueIdentifier) { Value = idrref }, (reader) =>
            {
                result = reader.Read();
            });
        return result;
    }

    #endregion

    #region Public and private methods - PLUs

    public static ushort GetPluCount(long scaleId)
    {
        ushort result = 0;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Plu.GetCount))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@SCALE_ID", scaleId);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = SqlConnect.GetValueAsNotNullable<ushort>(reader, "COUNT");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }


    #endregion

    #region Public and private methods - Scales

    public static long GetScaleId(string scaleDescription)
    {
        long result = 0;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        StringUtils.SetStringValueTrim(ref scaleDescription, 150);
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleId))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@SCALE_DESCRIPTION", scaleDescription);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static ScaleEntity? GetScaleFromHost(long hostId)
    {
	    SqlCrudConfigModel sqlCrudConfig = new(new()
		    { new($"Host.IdentityId", DbComparer.Equal, hostId), new(DbField.IsMarked, DbComparer.Equal, false) },
		    new(DbField.CreateDt, DbOrderDirection.Desc), 0);
	    return DataAccess.Crud.GetItem<ScaleEntity>(sqlCrudConfig);
    }

    public static ScaleEntity? GetScale(long id)
    {
	    SqlCrudConfigModel sqlCrudConfig = new(new()
			{ new(DbField.IdentityId, DbComparer.Equal, id), new(DbField.IsMarked, DbComparer.Equal, false) },
		    null, 0);
	    return DataAccess.Crud.GetItem<ScaleEntity>(sqlCrudConfig);
    }

    public static ScaleEntity? GetScale(string description)
    {
	    SqlCrudConfigModel sqlCrudConfig = new(new()
			{ new(DbField.Description, DbComparer.Equal, description), new(DbField.IsMarked, DbComparer.Equal, false) },
		    null, 0);
	    return DataAccess.Crud.GetItem<ScaleEntity>(sqlCrudConfig);
    }

    public static ProductionFacilityEntity? GetArea(string name)
    {
	    SqlCrudConfigModel sqlCrudConfig = new(new()
			{ new(DbField.Name, DbComparer.Equal, name), new(DbField.IsMarked, DbComparer.Equal, false) },
		    null, 0);
	    return DataAccess.Crud.GetItem<ProductionFacilityEntity>(sqlCrudConfig);
    }

    #endregion

	#region Public and private methods - Tasks

	public static void SaveNullTask(TaskTypeDirect taskType, long scaleId, bool enabled)
    {
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.InsertTask);
        cmd.Connection = con;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@task_type_uid", taskType.Uid);
        cmd.Parameters.AddWithValue("@scale_id", scaleId);
        cmd.Parameters.AddWithValue("@enabled", enabled);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public static void SaveTask(TaskDirect task, bool enabled)
    {
        if (task == null)
            return;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.UpdateTask);
        cmd.Connection = con;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@uid", task.Uid);
        cmd.Parameters.AddWithValue("@enabled", enabled);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public static Guid GetTaskUid(string taskName)
    {
        Guid result = Guid.Empty;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        StringUtils.SetStringValueTrim(ref taskName, 32);
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskUid))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_type", taskName);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static TaskDirect? GetTask(Guid taskTypeUid, long scaleId)
    {
        TaskDirect? result = null;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByTypeAndScale))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
	        cmd.Parameters.AddWithValue("@scale_id", scaleId);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = new()
			        {
				        Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
				        TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
				        //Scale = ScalesUtils.GetScale(dataAccess, SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
				        Scale = DataAccess.Crud.GetItemById<ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
				        Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
			        };
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static TaskDirect? GetTask(Guid taskUid)
    {
        TaskDirect? result = null;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByUid))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_uid", taskUid);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = new()
			        {
				        Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
				        TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
				        //Scale = ScalesUtils.GetScale(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
				        Scale = DataAccess.Crud.GetItemById<ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
				        Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
			        };
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static Guid GetTaskTypeUid(string taskTypeName)
    {
        Guid result = Guid.Empty;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        StringUtils.SetStringValueTrim(ref taskTypeName, 32);
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTaskTypeUid))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_type", taskTypeName);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static TaskTypeDirect GetTaskType(string name)
    {
        TaskTypeDirect result = new();
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByName))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_name", name);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
			        result.Name = SqlConnect.GetValueAsString(reader, "NAME");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static TaskTypeDirect GetTaskType(Guid uid)
    {
        TaskTypeDirect result = new();
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByUid))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        cmd.Parameters.AddWithValue("@task_uid", uid);
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        if (reader.Read())
		        {
			        result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
			        result.Name = SqlConnect.GetValueAsString(reader, "NAME");
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    public static List<TaskTypeDirect> GetTasksTypes()
    {
        List<TaskTypeDirect> result = new();
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypes))
        {
	        cmd.Connection = con;
	        cmd.Parameters.Clear();
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        while (reader.Read())
		        {
			        result.Add(new(
				        SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID"), SqlConnect.GetValueAsString(reader, "NAME")));
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }
    
    public static SqlCrudConfigModel GetCrudConfigIsMarked() => GetCrudConfig(null, null, 0, false, false);
    
    public static SqlCrudConfigModel GetCrudConfig(List<FieldFilterModel>? filters, FieldOrderModel? order, int maxResults, bool isShowMarked, bool isShowOnlyTop)
    {
		maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;
		SqlCrudConfigModel sqlCrudConfig = new(filters, order, maxResults);
	    List<FieldFilterModel> filtersMarked = new() { new(DbField.IsMarked, DbComparer.Equal, false) };
	    if (!isShowMarked)
	    {
		    switch (sqlCrudConfig.Filters)
		    {
			    case null:
				    sqlCrudConfig.Filters = filtersMarked;
				    break;
			    default:
				    sqlCrudConfig.Filters.AddRange(filtersMarked);
				    break;
		    }
	    }

		return sqlCrudConfig;
    }

    #endregion
}

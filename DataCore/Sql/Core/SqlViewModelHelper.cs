// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public class SqlViewModelHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlViewModelHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlViewModelHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private PublishTypeEnum _publishType = PublishTypeEnum.Default;
    public PublishTypeEnum PublishType
    {
        get => _publishType;
        private set
        {
            _publishType = value;
            OnPropertyChanged();
        }
    }
    private string _publishDescription = "";
    public string PublishDescription
    {
        get => _publishDescription;
        private set
        {
            _publishDescription = value;
            OnPropertyChanged();
        }
    }
    private string _sqlInstance = "";

    private string SqlInstance
    {
        get => _sqlInstance;
        set
        {
            _sqlInstance = value;
            OnPropertyChanged();
        }
    }
    //private List<TaskDirect> _tasks = new();

    //private List<TaskDirect> Tasks
    //{
    //    get => _tasks;
    //    set
    //    {
    //        _tasks = value;
    //        OnPropertyChanged();
    //    }
    //}
    //private List<TaskTypeDirect> _taskTypes = new();

    //private List<TaskTypeDirect> TaskTypes
    //{
    //    get => _taskTypes;
    //    set
    //    {
    //        _taskTypes = value;
    //        OnPropertyChanged();
    //    }
    //}

    private SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
    private HostModel _host;
    public HostModel Host
    {
        get => _host;
        private set
        {
            _host = value;
            OnPropertyChanged();
        }
    }
    private ScaleModel _scale;
    public ScaleModel Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            //SetupTasks(_scale.Identity.Id);
            OnPropertyChanged();
        }
    }
    private ProductionFacilityModel? _area;
    public ProductionFacilityModel? Area
    {
        get
        {
            if (_area != null)
                return _area;
            if (Scale.WorkShop != null)
                return Scale.WorkShop.ProductionFacility;
            return _area;
        }
        set
        {
            if (value == null)
            {
                if (Scale.WorkShop != null)
                    _area = Scale.WorkShop.ProductionFacility;
                return;
            }
            _area = value;
            OnPropertyChanged();
        }
    }
    private List<string> _scales;
    public List<string> Scales
    {
        get => _scales;
        private set
        {
            _scales = value;
            OnPropertyChanged();
        }
    }
    private List<string> _areas;
    public List<string> Areas
    {
        get => _areas;
        private set
        {
            _areas = value;
            OnPropertyChanged();
        }
    }
    public readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+31);
    public readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-31);
    //private OrderEntity? _order;
    //public OrderEntity? Order
    //{
    //    get => _order;
    //    set
    //    {
    //     _order = value;
    //     OnPropertyChanged();
    //    }
    //}
    private DateTime _productDate;
    public DateTime ProductDate
    {
        get => _productDate;
        set
        {
            _productDate = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlViewModelHelper()
    {
        SetPublish();

        _host = new();
        _scale = new();
        _scales = new();
        _area = null;
        _areas = new();
        //_order = new();
        Setup(-1, "");
    }

    #endregion

    #region Public and private methods

    public void Setup(long scaleId, string areaName)
    {
        //TaskTypes = new();
        //Tasks = new();
        SetScale(scaleId, areaName);
        SetScales();
        SetAreas();
    }

    private void SetScale(long scaleId, string areaName)
    {
        if (scaleId <= 0)
        {
            if (string.IsNullOrEmpty(Host.HostName))
            {
                string hostName = NetUtils.GetLocalHostName(false);
                Host = SqlUtils.GetHost(hostName);
            }
            Scale = SqlUtils.GetScaleFromHost(Host.Identity.Id);
        }
        else
        {
            Scale = SqlUtils.GetScale(scaleId);
        }
        if (!string.IsNullOrEmpty(areaName))
            Area = SqlUtils.GetArea(areaName);
    }

    private void SetScales()
    {
        Scales = new();
        SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldOrderModel(nameof(SqlTableBase.Description)), 0, false, false);
        ScaleModel[]? scales = SqlUtils.DataAccess.GetItems<ScaleModel>(sqlCrudConfig);
        scales?.ToList().ForEach(scale => Scales.Add(scale.Description));
    }

    private void SetAreas()
    {
        Areas = new();
        SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new SqlFieldOrderModel(nameof(ProductionFacilityModel.Name)), 0, false, false);
        ProductionFacilityModel[]? areas = SqlUtils.DataAccess.GetItems<ProductionFacilityModel>(sqlCrudConfig);
        areas?.Where(x => x.Identity.Id > 0).ToList().ForEach(area => Areas.Add(area.Name));
    }

    private void SetPublish()
    {
        PublishType = PublishTypeEnum.Default;
        PublishDescription = "Неизвестный сервер";
        SqlInstance = GetInstance();
        SetPublishFromInstance();
    }

    private void SetPublishFromInstance()
    {
        switch (SqlInstance)
        {
            case "INS1":
                PublishType = PublishTypeEnum.Debug;
                PublishDescription = LocaleCore.Sql.SqlServerTest;
                break;
            case "SQL2019":
                PublishType = PublishTypeEnum.Dev;
                PublishDescription = LocaleCore.Sql.SqlServerDev;
                break;
            case "LUTON":
                PublishType = PublishTypeEnum.Release;
                PublishDescription = LocaleCore.Sql.SqlServerProd;
                break;
        }
    }

    //public void SetupTasks(long? scaleId)
    //{
    //    if (scaleId == null)
    //        return;

    //    TaskTypes = SqlUtils.GetTasksTypes();

    //    Tasks = new();
    //    foreach (TaskTypeDirect taskType in TaskTypes)
    //    {
    //        TaskDirect? task = SqlUtils.GetTask(taskType.Uid, (long)scaleId);
    //        if (task == null)
    //        {
    //            SqlUtils.SaveNullTask(taskType, (long)scaleId, true);
    //            task = SqlUtils.GetTask(taskType.Uid, (long)scaleId);
    //        }
    //        if (task != null)
    //            Tasks.Add(task);
    //    }
    //}

    //public bool IsTaskEnabled(TaskType taskType)
    //{
    //    // Table [TASKS] don't has records.
    //    if (Tasks.Count == 0)
    //        return true;
    //    foreach (TaskDirect task in Tasks)
    //    {
    //        if (string.Equals(task.TaskType.Name, taskType.ToString(), StringComparison.InvariantCultureIgnoreCase))
    //        {
    //            return task.Enabled;
    //        }
    //    }
    //    return false;
    //}

    private string GetInstance()
    {
        string result = string.Empty;
        SqlConnect.ExecuteReader(SqlQueries.DbSystem.Properties.GetInstance, (reader) =>
        {
            if (reader.Read())
            {
                result = SqlConnect.GetValueAsString(reader, "InstanceName");
            }
        });
        return result;
    }

    #endregion
}

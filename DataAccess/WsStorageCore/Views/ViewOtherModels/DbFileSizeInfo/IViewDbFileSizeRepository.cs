namespace WsStorageCore.Views.ViewOtherModels.DbFileSizeInfo;

public interface IViewDbFileSizeRepository
{
    public List<WsSqlViewDbFileSizeInfoModel> GetList();
}
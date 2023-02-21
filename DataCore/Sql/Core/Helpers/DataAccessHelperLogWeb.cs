// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Models;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
	#region Public and private methods

    private void LogWebCoreFast(string data, LogTypeEnum logType, ServiceLogDirection logDirection,
        string filePath = "", int lineNumber = 0, string memberName = "")
    {
        StringUtils.SetStringValueTrim(ref filePath, 64, true);
        StringUtils.SetStringValueTrim(ref memberName, 64);
        //LogTypeModel? logTypeItem = GetItemLogTypeNullable(logType);

        //LogWebModel log = new()
        //{
        //    CreateDt = DateTime.Now,
        //    ChangeDt = DateTime.Now,
        //    IsMarked = false,
        //    Version = AppVersion.Version,
        //    File = filePath,
        //    Line = lineNumber,
        //    Member = memberName,
        //    Direction = (byte)logDirection,
        //    DataString = data,
        //};
        //SaveAsync(log).ConfigureAwait(false);
    }

    public void LogWebInformation(string data, ServiceLogDirection logDirection) => 
        LogWebCoreFast(data, LogTypeEnum.Information, logDirection);
	
	#endregion
}

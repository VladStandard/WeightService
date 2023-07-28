﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.PluLabels;

public class WsSqlViewPluLabelRepository : IViewPluLabelRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluLabelModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluLabelModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluLabels(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 10 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                ProductDate = Convert.ToDateTime(item[i++]),
                ExpirationDate = Convert.ToDateTime(item[i++]),
                WeightingDate = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                PluName = item[i++] as string ?? string.Empty,
                Template = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
}
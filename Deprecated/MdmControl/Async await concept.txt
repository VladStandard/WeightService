private void ClearEntity(EnumTable table)
{
    switch (table)
    {
        case EnumTable.NomenclatureMaster:
            EntityMaster = null;
            break;
        case EnumTable.NomenclatureNonNormalize:
            EntityNonNormilize = null;
            break;
    }
}

private async Task ClearEntityAsync(EnumTable table,
    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
{
    var task = new Task(() => ClearEntity(table));
    await AppSettings.RunTasks(LocalizationStrings.TableMasterClear,
        "", LocalizationStrings.DialogResultFail, "",
        new List<Task> { task }, GuiRefreshAsync, filePath, lineNumber, memberName).ConfigureAwait(false);
}

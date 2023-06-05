// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBlazorCore.Utils;

public static class WsBlazorCssValidationUtils
{
    #region Public and private methods

    public static IValidator GetValidator<T>() where T : CssStyleBase, new() =>
        typeof(T) switch
        {
            var cls when cls == typeof(CssStyleRadzenColumnModel) => new CssStyleRadzenColumnValidator(),
            var cls when cls == typeof(CssStyleTableBodyModel) => new CssStyleTableBodyValidator(),
            var cls when cls == typeof(CssStyleTableHeadModel) => new CssStyleTableHeadValidator(),
            _ => throw new NotImplementedException()
        };

    public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            CssStyleRadzenColumnModel cssStyleRadzenColumn => new CssStyleRadzenColumnValidator().Validate(cssStyleRadzenColumn),
            CssStyleTableBodyModel cssStyleTableBody => new CssStyleTableBodyValidator().Validate(cssStyleTableBody),
            CssStyleTableHeadModel cssStyleTableHead => new CssStyleTableHeadValidator().Validate(cssStyleTableHead),
            _ => throw new NullReferenceException(nameof(item))
        };

    public static bool IsValidation<T>(T? item) where T : class, new() =>
        item is not null && GetValidationResult(item).IsValid;

    #endregion
}
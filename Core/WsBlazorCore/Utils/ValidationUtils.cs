// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation.Results;
using WsBlazorCore.CssStyles;

namespace WsBlazorCore.Utils;

public class ValidationUtils
{
    #region Public and private methods

    public static ValidationResult GetValidationResult<T>(T? item) where T : class, new() =>
        item switch
        {
            // CssStyle
            CssStyleRadzenColumnModel cssStyleRadzenColumn => new CssStyleRadzenColumnValidator().Validate(cssStyleRadzenColumn),
            CssStyleTableBodyModel cssStyleTableBody => new CssStyleTableBodyValidator().Validate(cssStyleTableBody),
            CssStyleTableHeadModel cssStyleTableHead => new CssStyleTableHeadValidator().Validate(cssStyleTableHead),
            _ => throw new NullReferenceException(nameof(item))
        };

    #endregion
}
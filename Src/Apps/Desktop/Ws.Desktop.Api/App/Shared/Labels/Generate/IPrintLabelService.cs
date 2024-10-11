using Ws.Database.Entities.Print.Labels;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;

namespace Ws.Desktop.Api.App.Shared.Labels.Generate;

public interface IPrintLabelService
{
    /// <summary>
    /// Создает весовую этикетку
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto);

    Task<bool> DeletePallet(string palletNumber, bool isDelete);

    /// <summary>
    /// Создает паллету
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount);
}
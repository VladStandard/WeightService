using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Weight.Dto;

namespace Ws.Labels.Service.Generate;

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
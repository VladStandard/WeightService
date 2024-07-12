using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Weight.Dto;

namespace Ws.Labels.Service.Generate;

public interface IPrintLabelService
{
    /// <summary>
    /// Создает весовую этикетку
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    (Label, LabelZpl) GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto);


    /// <summary>
    /// Создает паллету
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    Task<Guid> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount, uint counter);
}
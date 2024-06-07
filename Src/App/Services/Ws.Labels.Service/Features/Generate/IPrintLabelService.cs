using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;

namespace Ws.Labels.Service.Features.Generate;

public interface IPrintLabelService
{
    /// <summary>
    /// Создает весовую этикетку
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    Label GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto);


    /// <summary>
    /// Создает паллету
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    void GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount);
}
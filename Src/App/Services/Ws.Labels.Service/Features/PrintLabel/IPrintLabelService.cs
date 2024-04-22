using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Dto.PrintWeightPlu;

namespace Ws.Labels.Service.Features.PrintLabel;

public interface IPrintLabelService
{
    /// <summary>
    /// Создает весовую этикетку
    /// </summary>
    /// <exception cref="LabelWeightGenerateException">Ошибка формирования.</exception>
    LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto);

    void GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount);
}
﻿using Ws.Database.Core.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.Template;

public class TemplateService(SqlTemplateRepository templateRepo) : ITemplateService
{
    public IEnumerable<TemplateEntity> GetAll() => templateRepo.GetAll();
    public TemplateEntity GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);
}
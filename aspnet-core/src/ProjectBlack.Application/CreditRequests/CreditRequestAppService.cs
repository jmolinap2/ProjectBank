using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ProjectBlack.CreditRequests.Dtos;
using System.Threading.Tasks;
using ProjectBlack.Authorization;

namespace ProjectBlack.CreditRequests
{
    [AbpAuthorize]
    public class CreditRequestAppService : AsyncCrudAppService<CreditRequest, CreditRequestDto, long>
    {
        public CreditRequestAppService(IRepository<CreditRequest, long> repository)
            : base(repository)
        {
        }

        public override async Task<CreditRequestDto> CreateAsync(CreditRequestDto input)
        {
            if (input.IngresoMensual >= 1500)
                input.Estado = CreditStatus.Aprobado.ToString();
            else
                input.Estado = CreditStatus.Pendiente.ToString();

            return await base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Analyst)]
        public async Task Approve(long id)
        {
            var entity = await Repository.GetAsync(id);
            entity.Estado = CreditStatus.Aprobado;
            await Repository.UpdateAsync(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_Analyst)]
        public async Task Reject(long id)
        {
            var entity = await Repository.GetAsync(id);
            entity.Estado = CreditStatus.Rechazado;
            await Repository.UpdateAsync(entity);
        }
    }
}

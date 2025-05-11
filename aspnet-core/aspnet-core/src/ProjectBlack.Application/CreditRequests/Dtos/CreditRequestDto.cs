using Abp.Application.Services.Dto;

namespace ProjectBlack.CreditRequests.Dtos
{
    public class CreditRequestDto : EntityDto<long>
    {
        public decimal MontoSolicitado { get; set; }
        public int PlazoMeses { get; set; }
        public decimal IngresoMensual { get; set; }
        public int AntiguedadLaboral { get; set; }
        public string Estado { get; set; }
    }
}

using System.ComponentModel;

namespace ME.Desafio.Database.Enums
{
    public enum EnumStatusResponse
    {
        [Description("APROVADO")]
        APROVADO,

        [Description("APROVADO_VALOR_A_MENOR")]
        APROVADO_VALOR_A_MENOR,

        [Description("APROVADO_VALOR_A_MAIOR")]
        APROVADO_VALOR_A_MAIOR,

        [Description("APROVADO_QTD_A_MAIOR")]
        APROVADO_QTD_A_MAIOR,

        [Description("REPROVADO")]
        REPROVADO,

        [Description("CODIGO_PEDIDO_INVALIDO")]
        CODIGO_PEDIDO_INVALIDO,

        [Description("APROVADO_QTD_A_MENOR")]
        APROVADO_QTD_A_MENOR,
    }
}
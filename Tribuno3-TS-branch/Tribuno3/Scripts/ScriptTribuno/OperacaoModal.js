var pIdOperacao = 0;

$(document).ready(function () {

    $('#GridParcelas').jtable({
        title: 'Parcelas',
        paging: true, //Enable paging 
        pageSize: 5, //Set page size (default: 10)
        sorting: false, //Enable sorting       
        defaultSorting: '', //Set default        
        actions: {
            listAction: urlListarParcelas
        },
        fields: {
            Numero_Parcela: {
                title: 'Numero da Parcela',
                width: '20%'
            },
            Valor_Parcela: {
                title: 'Valor',
                width: '20%',
                display: function (data) {
                    return '<h7>' + FormatarValorMoeda(data.record.Valor_Parcela) + '</h7>';
                }
            },
            DataVencimentoParcela: {
                title: 'Data de Vencimento',
                width: '20%',
                type: 'date',
                displayFormat: 'dd-mm-yy'
            }
        }
    });

    $('#GridParcelas').jtable('load');

    $("#btCalcular").click(function (e) {
        
        var OperacaoModel = CriarObjetoOperacao();           

        AcionarActionCalcular('Operacao', 'CalcularParcelas', 'POST', OperacaoModel, null);

        AtualizarGridParcelas();
    });
    /*
    $("#btGravar").click(function (e) {
        var obj = ConvertFormToJSON('#formPassivo');
        obj.TipoOperacao = TipoOperacao;

        var OperacaoModel = CriarObjetoOperacao();
       
        AcionarAction('Operacao', 'GravarParcelas', 'POST', OperacaoModel, 'Operação gravado com sucesso !', true);

        AtualizarGrid();
        AtualizarGrafico();
        AtualizarInformativos();
        PreencheCamposModalPassivo(null);

    });
    */
    function CriarObjetoOperacao()
    {
        var Operacao = new Object();
        Operacao.IdOperacao = pIdOperacao;
        Operacao.NomeOperacao = $('#NomeOperacao').val();
        Operacao.Descricao = $('#Descricao').val();
        var radioTipoCalculo = $("input[name='TipodeCalculo']:checked").val();
        Operacao.TipodeCalculo = "Parcela";       
        Operacao.QtdParcela = $('#QtdParcela').val();
        Operacao.ValorParcela = $('#ValorParcela').val();
        Operacao.DataOperacao = $('#DataOperacao').val();
        Operacao.TipoOperacao = TipoOperacao;
    
        return Operacao;
    }

    function FormatarValorMoeda(pValor) {
        var valor = pValor.toFixed(2).replace('.', ',');
        var valorFormatado = 'R$' + valor.toString();

        return valorFormatado;
    }

    function AcionarActionCalcular(pController, pAction, TipoVerbo, data, alertSucesso) {
        if (pController || pAction || TipoVerbo || pSucesso) {
            var url = "/" + pController + "/" + pAction;
        }
        else {
            return;
        }

        $.ajax({
            url: url,
            type: TipoVerbo,
            data,
            async: false,
            datatype: "json",
            success: function (data) {
                $("#MsgCritica").text(data);
            }
        });
    }  

    function AtualizarGridParcelas() {
        $.ajax({
            type: "POST",
            data: {},
            async: false,
            datatype: "json",
            success: function (data) {
                $('#GridParcelas').jtable('reload');
            }
        });
    }

    function ConvertFormToJSON(form) {
        var array = jQuery(form).serializeArray();
        var json = {};

        jQuery.each(array, function () {
            json[this.name] = this.value || '';
        });

        console.log('JSON: ' + json);
        return json;
    }

});
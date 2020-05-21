var pIdPassivoAlteracao = 0;
var pIdRendimentoAlteracao = 0;
var pTipoOperacao = '';

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
                    return '<h7>' + FormatarValorMoeda(data.record.Valor_Parcela)+'</h7>';
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

        var obj = ConvertFormToJSON('#formPassivo');

        AcionarActionCalcular('Passivo', 'CalcularParcelas', 'POST', obj, null);

        AtualizarGridParcelas();
    });

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

    $("#btGravar").click(function (e) {
        var obj = ConvertFormToJSON('#formPassivo');
        if (pTipoOperacao == 'Passivo') {
            if (pIdPassivoAlteracao > 0)
                obj.IdOperacao = pIdPassivoAlteracao;
        }
        else if (pTipoOperacao == 'Rendimento') {
            if (pIdRendimentoAlteracao > 0)
                obj.IdOperacao = pIdRendimentoAlteracao;
        }

        AcionarAction('Passivo', 'GravarParcelas', 'POST', obj, 'Operação gravado com sucesso !', true);

        AtualizarGrid();
        AtualizarGrafico();
        AtualizarInformativos();
    });

    function abreModal(data) {

        var date = data.Result.DataVencimento;
        var convertidaDate = new Date(parseInt(date.substr(6)));
        var dataFormatada = $.format.date(convertidaDate, "dd/MM/yyyy")

        $("#NomeOperacaoPassivo").val(data.Result.NomeOperacao);
        $("#ValorParcelaPassivo").val(data.Result.ValorParcela);
        $("#QtdParcelaPassivo").val(data.Result.QtdParcela);
        $("#DataOperacaoPassivo").val(dataFormatada);
        $("#DescricaoPassivo").val(data.Result.Descriacao);

        $("#modal-divida").modal({
            show: true
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
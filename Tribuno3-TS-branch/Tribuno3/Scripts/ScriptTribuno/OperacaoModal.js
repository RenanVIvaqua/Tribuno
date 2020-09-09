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

    $("#btGravar").click(function (e) {

        var obj = ConvertFormToJSON('#formOperacao');
        obj.TipoOperacao = TipoOperacao;

        $.ajax({
            method: "POST",
            url: "/Operacao/GravarParcelas",
            async: false,
            data: { obj },
            success: function (data) {
                AtualizarGrid();
                AtualizarGrafico();
                AtualizarInformativos();
                if (data.success = true)
                    alert(data.Message);
                //window.location.href = "/ContasPagar/";
            },
            error: function (data) {
                if (data.success = false)
                    alert(data.Message);
            }
        })
    });

    $(".parcelas").change(function () {

        if ($('#QtdParcela').val() != "" && $('#QtdParcela').val() != "" && $('#DataOperacao').val() != "") {
            GerarParcelas();
            $('#btGravar').attr('disabled', false);
        }
        else {
            $('#btGravar').attr('disabled', true);
        }
    });

    function GerarParcelas() {

        var OperacaoModel = ConvertFormToJSON('#formOperacao');     
        OperacaoModel.TipoOperacao = TipoOperacao;
       //$("#TipoOperacao").attr("value", TipoOperacao);        
       
        $.ajax({
            method: "POST",
            url: "/Operacao/CalcularParcelas",
            async: false,
            data: OperacaoModel ,
            success: function (data) {               
                if (data.success = true) {
                    AtualizarGridParcelas();                    
                    $("#MsgCritica").text(data.Message)
                }                               
            },
            error: function (data) {
                if (data.success = false)
                    alert(data.Message);
            }
        })     
    }  
      

    function FormatarValorMoeda(pValor) {
        var valor = pValor.toFixed(2).replace('.', ',');
        var valorFormatado = 'R$' + valor.toString();

        return valorFormatado;
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

   
})




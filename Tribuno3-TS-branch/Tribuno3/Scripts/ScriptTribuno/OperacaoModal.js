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
        var OperacaoModel = FormularioEmObjetoOperacao(); 

        $.ajax({
            method: "POST",
            url: "/Operacao/GravarParcelas",
            async: false,
            data: OperacaoModel,
            success: function (data) {
                AtualizarGrid();
                AtualizarGrafico();
                AtualizarInformativos();
                if (data.success = true) {
                    alert(data.Message);
                    abreModalDecisao('Cadastro de Operação', 'Deseja cadastrar uma nova operação ?', NovaOperacao, null, FecharOperacao,null);
                   
                }
                else {
                    alert(data.Message);
                }
            }          
        })
    });

    function NovaOperacao() {
        FecharModalDecisao();


    }

    function FecharOperacao() {
        FecharModalDecisao();
        
    }


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
        var OperacaoModel = FormularioEmObjetoOperacao();  
         
       
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

    function FormularioEmObjetoOperacao() {

        var OperacaoModel = ConvertFormToJSON('#formOperacao');     
        OperacaoModel.TipoOperacao = TipoOperacao;
        OperacaoModel.IdOperacao = pIdOperacao;   

        alert(TipoOperacao);
      



        return OperacaoModel
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

})




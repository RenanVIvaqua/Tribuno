var TipoOperacao = '';

$(document).ready(function ()
{
    $('#gridJtable').jtable({
        title: 'Passivo',
        paging: true, //Enable paging 
        pageSize: 5, //Set page size (default: 10)
        sorting: true, //Enable sorting       
        defaultSorting: '', //Set default 
        actions: {
            listAction: urlClienteList
        },
        fields: {
            NomeOperacao: {
                title: 'Operação',
                width: '30%'
            },
            ValorOperacao: {
                title: 'Valor',
                width: '20%',
                display: function (data) {
                    return '<h7>' + FormatarValorMoeda(data.record.ValorOperacao) + '</h7>';
                }
            },

            ValorParcela: {
                title: 'Vl Parcela',
                width: '20%',
                display: function (data) {
                    return '<h7>' + FormatarValorMoeda(data.record.ValorParcela) + '</h7>';
                }
            },
             QtdParcela: {
                 title: 'Qtd Parc',
                width: '15%'
            },
            Descriacao: {
                title: 'Descrição',
                width: '20%'
            },
            Alterar: {
                title: '',
                display: function (data) {
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarOperacao(' + data.record.IdOperacao +','+"'" +'Passivo'+"'"+')" >Alterar</button>';
                }
            },
            Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Passivo' + "'" +')" ><i class="fa fa-trash"> </i></button>';
                }
            }
        }
    });   

    $('#gridRendimento').jtable({
        title: 'Rendimento',
        paging: true, //Enable paging 
        pageSize: 5, //Set page size (default: 10)
        sorting: true, //Enable sorting       
        defaultSorting: '', //Set default 
        actions: {
            listAction: urlListRendimento
        },
        fields: {
            NomeOperacao: {
                title: 'Operação',
                width: '30%'
            },

            ValorOperacao: {
                title: 'Valor',
                width: '20%',
                display: function (data) {
                    return '<h7>' + FormatarValorMoeda(data.record.ValorOperacao) + '</h7>';
                }
            },

            ValorParcela: {
                title: 'Vl Parcela',
                width: '20%',
                display: function (data) {
                    return '<h7>' + FormatarValorMoeda(data.record.ValorParcela) + '</h7>';
                }
            },
            QtdParcela: {
                title: 'Qtd Parc',
                width: '15%'
            },
            Descriacao: {
                title: 'Descriacao',
                width: '20%'
            },
            Alterar: {
                title: '',
                display: function (data) {
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Rendimento' + "'" +')" >Alterar</button>';


                }
            },
             Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Rendimento' + "'" +')" ><i class="fa fa-trash"> </i></button>';


                }
            }
        }
    });

    function FormatarValorMoeda(pValor) {
        var valor = pValor.toFixed(2).replace('.', ',');
        var valorFormatado = 'R$' + valor.toString();

        return valorFormatado;
    }
       
    $('#gridJtable').jtable('load');

    $('#gridRendimento').jtable('load');

    $("#AddPassivo").on("click", function () {   
        TipoOperacao = 'Passivo';       
        abreModal(null);
    });

    $("#AddRendimento").on("click", function () {              
        TipoOperacao = 'Rendimento';  
        abreModal(null);
    });        

});

function DeletarOperacao(pIdOperacao,pTipoOperacao)
{
    let operacao = new Object();
    operacao.TipoOperacao = pTipoOperacao;
    operacao.IdOperacao = pIdOperacao
    operacao.Cadastro = false;

    abreModalDecisao('Excluir', 'Deseja excluir a operação ?', EfetuarExclusaoOperacao, operacao);         
}

function EfetuarExclusaoOperacao(pOperacao)
{   
    AcionarAction('Operacao', 'DeletarOperacao', 'POST', pOperacao, 'Operação deletado com sucesso', true);    
    AtualizarGrid();
    AtualizarGrafico();
    AtualizarInformativos();

    FecharModalDecisao();
}

function AlterarOperacao(pIdOper,pTipoOperacao)
{
    var pOperacao = new Object();
    pOperacao.IdOperacao = pIdOper;
    pOperacao.TipoOperacao = pTipoOperacao;
    pOperacao.Cadastro = false;
    pOperacao.TipoOperacao = pTipoOperacao;
    TipoOperacao = pTipoOperacao;     
    pIdOperacao = pIdOper;       

    $("#IdOperacao").attr("value", pIdOper);   
    $("#TipoOperacao").attr("value", pTipoOperacao);  
    
    $.ajax({
        url: urlConsultarOperacao,
        type: "POST",
        data: { pOperacao },
        datatype: "json",
        success: function (data) {           
            CarregarParcelas(data, pOperacao);
        }
    });


}

function PreencheCamposModalPassivo(data) {
    if (data) {
        $("#NomeOperacao").val(data.Result.NomeOperacao);
        $("#ValorParcela").val(data.Result.ValorParcela);
        $("#QtdParcela").val(data.Result.QtdParcela);
        $("#DataOperacao").val(data.Result.DataVencimento);
        $("#Descricao").val(data.Result.Descriacao); 
        $("#radioValor").prop("disabled", false);
        //Verificar AQUI
              
    }
    else {
        $("#NomeOperacao").val("");
        $("#ValorParcela").val("");
        $("#QtdParcela").val("");
        $("#DataOperacao").val("");
        $("#Descricao").val("");    
        pIdOperacao = 0;
    }

}

async function CarregarParcelas(data, pOperacao)
{  
    await AcionarAction("Operacao", "CarregarParcelas", "POST", pOperacao, null, true);    
    
    const atualizaGrid = await AtualizarGridParcelas();
    const abrirModal = await abreModal(data);   
}

function AcionarAction(pController, pAction, TipoVerbo,data,alertSucesso,sincrono = false)
{
    if (pController || pAction || TipoVerbo || pSucesso) {
        var url = "/" + pController + "/" + pAction;
    }
    else { return; }
    if (sincrono)
    {
        $.ajax({
            url: url,
            type: TipoVerbo,
            data,
            async: false,
            datatype: "json",
            success: function () {
                if (alertSucesso)
                    alert(alertSucesso);

            }
        });
    }
    else {
        $.ajax({
            url: url,
            type: TipoVerbo,
            data,
            datatype: "json",
            success: function () {
                if (alertSucesso)
                    alert(alertSucesso);

            }
        });
    }
}

function AtualizarGrafico()
{
    $.ajax({
        url: urlGrafico,
        type: "POST",      
        datatype: "json",
        success: function (data) {            
            $("#Grafico").html(data);     
        }
    });
}

function AtualizarGridParcelas() {
    $.ajax({
        type: "POST",        
        datatype: "json",        
        success: function (data) {
            $('#GridParcelas').jtable('reload');
        }
    });
}

function AtualizarGrid() {
    $.ajax({
        url: urlClienteList,
        type: "POST",     
        datatype: "json",
        success: function () {
            $('#gridJtable').jtable('reload');  
            $('#gridRendimento').jtable('reload');
        }
    });
}

async function abreModal(data) { 

    if (data) {
        PreencheCamposModalPassivo(data)
    }
    else
    {
        var data = new Object()
        data.Cadastro = true;       
        await AcionarAction("Operacao", "CarregarParcelas", "POST", data, null, true)
        await AtualizarGridParcelas();

        PreencheCamposModalPassivo(null);
    }
    
    $("#modal-divida").modal({
        show: true
    });

}

function ConvertFormToJSON(form) {
    console.log('ConvertFormToJSON invoked!');
    var array = jQuery(form).serializeArray();
    var json = {};

    jQuery.each(array, function () {
        json[this.name] = this.value || '';
    });

    console.log('JSON: ' + json);
    return json;
}



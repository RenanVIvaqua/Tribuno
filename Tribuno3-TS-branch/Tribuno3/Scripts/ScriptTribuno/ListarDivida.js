
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
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarPassivo(' + data.record.Id_Operacao + ')" >Alterar</button>';
                }
            },
            Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarPassivo(' + data.record.Id_Operacao + ')" ><i class="fa fa-trash"> </i></button>';
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
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarRendimento(' + data.record.Id_Operacao + ')" >Alterar</button>';


                }
            },
             Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarRendimento(' + data.record.Id_Operacao + ')" ><i class="fa fa-trash"> </i></button>';


                }
            }
        }
    });

    function FormatarValorMoeda(pValor) {
        var valor = pValor.toFixed(2).replace('.', ',');
        var valorFormatado = 'R$' + valor.toString();

        return valorFormatado;
    }

    $("#bt_EnviarPassivo").click(function (e) {
        e.preventDefault();
        var obj = ConvertFormToJSON('#formAlterarPassivo')

        obj.IdOperacao = pIdPassivoAlteracao;

        AcionarAction('Principal', 'AlterarPassivo', 'POST', obj ,'Alteração Concluida')
    });

    $("#bt_EnviarRendimento").click(function (e) {
        e.preventDefault();
        var obj = ConvertFormToJSON('#formAlterarRendimento')

        obj.IdOperacao = pIdRendimentoAlteracao;

        AcionarAction('Principal', 'AlterarRendimento', 'POST', obj, 'Alteração Concluida')
    });

    $('#gridJtable').jtable('load');

    $('#gridRendimento').jtable('load');

    $("#AddPassivo").on("click", function () {

        var data = new Object();
        data.TipoOperacao = 'Passivo';
        
        AcionarAction('Passivo', 'DefinirTipoOperacao', 'POST', data,null,true)      
        abreModal(null);
    });

    $("#AddRendimento").on("click", function () {

        var data = new Object();
        data.TipoOperacao = 'Rendimento';

        AcionarAction('Passivo', 'DefinirTipoOperacao', 'POST', data, null, true)
        abreModal(null);
    });

});


function DeletarRendimento(pIdRendimento)
{ 
    var data = new Object();
    data.TipoOperacao = 'Rendimento';
    data.Id_Operacao = pIdRendimento

    AcionarAction('Passivo', 'DeletarOperacao', 'POST', data, 'Rendimento deletado com sucesso',true);    
    AtualizarGrid();
    AtualizarGrafico();
    AtualizarInformativos();
}

function DeletarPassivo(pIdPassivo) {
    var data = new Object();
    data.TipoOperacao = 'Passivo';
    data.Id_Operacao = pIdPassivo

    AcionarAction('Passivo', 'DeletarOperacao', 'POST', data, 'Passivo deletado com sucesso', true);
    AtualizarGrid();
    AtualizarGrafico();
    AtualizarInformativos();
}

function AlterarPassivo(pIdPassivo)
{    
    pIdPassivoAlteracao = pIdPassivo;
    pTipoOperacao = 'Passivo';    
    $.ajax({
        url: urlConsultar,
        type: "POST",
        data: { 'pIdPassivo': pIdPassivo },
        datatype: "json",
        success: function (data) {
            data.Result.TipoOperacao = 'Passivo';
            CarregarParcelas(data)
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

        pIdPassivoAlteracao = 0;
        pIdRendimentoAlteracao = 0;
    }

}

function AlterarRendimento(pIdRendimento) {
    
    pIdRendimentoAlteracao = pIdRendimento;
    pTipoOperacao = 'Rendimento'
    $.ajax({
        url: urlConsultarRendimento,
        type: "POST",
        data: { 'pIdRendimento': pIdRendimento },
        datatype: "json",
        success: function (data) {
            data.Result.TipoOperacao = 'Rendimento';
            CarregarParcelas(data)            
        }
    });
}

async function CarregarParcelas(data)
{
    await AcionarAction("Passivo", "DefinirTipoOperacao", "POST", data.Result, null, true);
    await AcionarAction("Passivo", "CarregarParcelas", "POST", data.Result, null, true);    
    
    const atualizaGrid = await AtualizarGridParcelas();
    const abrirModal = await abreModal(data)   
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
        await AcionarAction("Passivo", "CarregarParcelas", "POST", data, null, true)
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



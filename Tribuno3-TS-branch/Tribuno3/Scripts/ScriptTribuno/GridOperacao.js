var TipoOperacao = '';

$(document).ready(function () {
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
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Passivo' + "'" + ')" >Alterar</button>';
                }
            },
            Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Passivo' + "'" + ')" ><i class="fa fa-trash"> </i></button>';
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
                    return '<button id="btnAlterar" class="btn btn-xs btn-info" onclick="AlterarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Rendimento' + "'" + ')" >Alterar</button>';


                }
            },
            Remover: {
                title: '',
                display: function (data) {
                    return '<button id="btnExcluir" class="btn" onclick="DeletarOperacao(' + data.record.IdOperacao + ',' + "'" + 'Rendimento' + "'" + ')" ><i class="fa fa-trash"> </i></button>';


                }
            }
        }
    });   

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

function FormatarValorMoeda(pValor) {
    var valor = pValor.toFixed(2).replace('.', ',');
    var valorFormatado = 'R$' + valor.toString();

    return valorFormatado;
}

function DeletarOperacao(pIdOperacao, pTipoOperacao) {
    let operacao = new Object();
    operacao.TipoOperacao = pTipoOperacao;
    operacao.IdOperacao = pIdOperacao
    operacao.Cadastro = false;

    abreModalDecisao('Excluir', 'Deseja excluir a operação ?', EfetuarExclusaoOperacao, operacao);
}

function EfetuarExclusaoOperacao(pOperacao) {

    $.ajax({
        url: "/Operacao/DeletarOperacao",
        type: "POST",
        datatype: "json",
        async: false,
        data: pOperacao,
        success: function (data) {
            if (data.success = true) {
                AtualizarGrid();
                AtualizarGrafico();
                AtualizarInformativos();
            }               
            if (data.success = false)
                alert(data.Message);
        }
    });   

    FecharModalDecisao();
}

function AlterarOperacao(pIdOper, pTipoOperacao) {
    let pOperacao = new Object();
    pOperacao.IdOperacao = pIdOper;
    pOperacao.TipoOperacao = pTipoOperacao;
    pOperacao.Cadastro = false;

    TipoOperacao = pTipoOperacao;
    pIdOperacao = pIdOper;

    $("#IdOperacao").attr("value", pIdOper);
    $("#TipoOperacao").attr("value", pTipoOperacao);

    $.ajax({
        url: urlConsultarOperacao,
        type: "POST",
        data: pOperacao,
        datatype: "json",
        success: function (data) {
            if (data.success = true) {
                CarregarParcelas(pOperacao);
                AtualizarGridParcelas();
                abreModal(data);
            }
            else {
                alert(data.Message);
            }
        }
    });
}

function CarregarParcelas(pOperacao) {
    $.ajax({
        url: "/Operacao/CarregarParcelas",
        type: "POST",
        datatype: "json",
        async: false,
        data: pOperacao,
        success: function (data) {
            if (data.success = false)
                alert(data.Message);
        }
    });
}

function AcionarAction(pController, pAction, TipoVerbo, data, alertSucesso, sincrono = false) {
    if (pController || pAction || TipoVerbo || pSucesso) {
        let url = "/" + pController + "/" + pAction;
    }
    else { return; }
    if (sincrono) {
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

function AtualizarGrafico() {
    $.ajax({
        url: urlGrafico,
        async:false,
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
        async: false,
        success: function (data) {
            $('#GridParcelas').jtable('reload');
        }
    });
}

function AtualizarGrid() {
    $.ajax({
        url: urlClienteList,
        async: false,
        type: "POST",
        datatype: "json",
        success: function () {
            $('#gridJtable').jtable('reload');
            $('#gridRendimento').jtable('reload');
        }
    });
}

function abreModal(data) {

    if (data) {
        PreencheCamposModal(data)
    }
    else {

        // Limpa as parcelas na Sessão caso for a rotina de cadastro
        let data = new Object()
        data.Cadastro = true;
        CarregarParcelas(data)
        AtualizarGridParcelas();
        
        // limpa o modal
        PreencheCamposModal(null);
    }

    $("#modal-divida").modal({
        show: true
    });

}

function fechaModalOperacao() {
    $('#modal-divida').modal('hide');
}

function PreencheCamposModal(data) {
    if (data) {
        $("#NomeOperacao").val(data.Result.NomeOperacao);
        $("#ValorParcela").val(data.Result.ValorParcela);
        $("#QtdParcela").val(data.Result.QtdParcela);
        $("#DataOperacao").val(data.Result.DataVencimento);
        $("#Descricao").val(data.Result.Descriacao);
    }
    else {
        $("#NomeOperacao").val("");
        $("#ValorParcela").val("");
        $("#QtdParcela").val("");
        $("#DataOperacao").val("");
        $("#Descricao").val("");
        $("#MsgCritica").text("");
        pIdOperacao = 0;
    }
}

function ConvertFormToJSON(form) {
    console.log('ConvertFormToJSON invoked!');
    let array = jQuery(form).serializeArray();
    let json = {};

    jQuery.each(array, function () {
        json[this.name] = this.value || '';
    });

    console.log('JSON: ' + json);
    return json;
}



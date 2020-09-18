var FuncaoDecisaoSim;
var FuncaoDecisaoNao;
var ParametroDecisaoSim;
var ParametroDecisaoNao;

$(document).ready(function () {

    $(".Decisao").click(function () {
        DecisaoEscolhido = $(this).data('decisao');

        if (DecisaoEscolhido) {
            if (typeof (FuncaoDecisaoSim) == "function")
                FuncaoDecisaoSim(ParametroDecisaoSim).call();
        }
        else {
            if (typeof (FuncaoDecisaoNao) == "function")
                FuncaoDecisaoNao(ParametroDecisaoNao).call();
        }

        FecharModalDecisao();
    })
})

function abreModalDecisao(titulo, Mensagem, pNomeFuncaoDecisaoSim,pParametroFuncaoDecisaoSim, pNomeFuncaoDecisaoNao,pParametroFuncaoDecisaoNao) {

    $("#TituloModalDecisao").text(titulo);
    $("#Mensagem").text(Mensagem);

    FuncaoDecisaoSim = pNomeFuncaoDecisaoSim;
    ParametroDecisaoSim = pParametroFuncaoDecisaoSim;
    
    FuncaoDecisaoNao = pNomeFuncaoDecisaoNao;
    ParametroDecisaoNao = pParametroFuncaoDecisaoNao;

    $("#modalDecisao").modal({
        show: true
    });
}

function FecharModalDecisao() {
    $('#modalDecisao').modal('hide');
}


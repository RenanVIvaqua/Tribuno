function enviaInformacoes(Id_Divida, Nm_Divida, Qtd_Parcelas, Vl_Divida, Descricao) {
    $.ajax({
        url: "/Principal/Alterar",
        type: "POST",
        data: { 'Id_Divida': Id_Divida, 'Nm_Divida': Nm_Divida, 'Qtd_Parcelas': Qtd_Parcelas, 'Vl_Divida': Vl_Divida, 'Descricao': Descricao },
        datatype: "json",
        success: function (data) {
            alert(data.Mensagem);
        }
    });
}

function ProximoMes() {
    $.ajax({
        url: "/Principal/_Informativos",
        type: "POST",
        data: { 'alterarMes': 1 },
        datatype: "json",
        success: function (data) {
            $("#Informativo").html(data)
        }
    });
}

function AtualizarInformativos() {
    $.ajax({
        url: "/Principal/_Informativos",
        type: "POST",
        data: { 'alterarMes': 0 },
        datatype: "json",
        success: function (data) {
            $("#Informativo").html(data)
        }
    });
}

function AnteriorMes() {
    $.ajax({
        url: "/Principal/_Informativos",
        type: "POST",
        data: { 'alterarMes': 2 },
        datatype: "json",
        success: function (data) {
            $("#Informativo").html(data)
        }
    })
};

function AddOperacao(pTipoOperacao) {
    var data = new Object();
    data.TipoOperacao = pTipoOperacao;

    AcionarAction('Passivo', 'DefinirTipoOperacao', 'POST', data, null, true)
    abreModal(null);
};




﻿function GoTo(request) {
    window.location = window.origin + "/Venda/" + request
}

function CadastrarProduto() {
    GoTo("Cadastro");
}

function Editar(id) {
    GoTo("Cadastro/" + id)
}

function Redirect() {
    GoTo("")
}

function Excluir(id) {
    GoTo("Excluir/" + id)
}

function Novo() {
    GoTo("Cadastro")
}
function GoTo(request) {
    window.location = window.origin + "/Categoria/" + request
}

function Cadastrar() {
    GoTo("Cadastro")  // Este método faz com que a janela atual seja redirecionada para a origem + "/Categoria/Cadastro"
}

function Editar(codigo) {
    GoTo("Cadastro/" + codigo)
}

function Novo() {
    GoTo("Cadastro/")
}

function Voltar() {
    GoTo("")
}

function Excluir(codigo) {
    let id = document.getElementById("codigo").value
    if (id != null) {
        GoTo("Excluir/" + codigo)
    }
    else {
        alert("Não é um item")
    }
}

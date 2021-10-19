function GoTo(request) {
    window.location = window.origin + "/Venda/" + request
}

function RegistrarVenda() {
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

function BuscarPrecoProduto() {
    let productCode = document.getElementById("cboProduto").value
    let url = `/LerValorProduto/${productCode}`
    let xhr = new XMLHttpRequest()

    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            document.getElementById("txtPrecoUnitario").value = xhr.responseText
        }
    }
    xhr.open('GET', url, false)
    xhr.send(null)
}

function CalcSubTotal() {
    let quantity = document.getElementById("txtQtde").value
    let unitValue = document.getElementById("txtPrecoUnitario").value
    let subtotal = quantity * unitValue
    document.getElementById("txtSubTotal").value = subtotal
}

let items = new Object()
items.Produtos = new Array()
let gridProdutos = document.getElementById("gridProdutos")
 
function AddProduto() {
    let codigoProduto = document.getElementById("cboProduto")
    let quantidade = document.getElementById("txtQtde").value
    let valorUnitario = document.getElementById("txtPrecoUnitario").value
    let subtotal = document.getElementById("txtSubTotal").value

    items.Produtos.push({
        "CodigoProduto": codigoProduto.value,
        "Quantidade": quantidade,
        "ValorUnitario": valorUnitario,
        "ValorTotal": subtotal
    })

    document.getElementById("txtJsonProdutos").value = JSON.stringify(items.Produtos)

    let linhaGrid = `
    <tr id="${codigoProduto.value}">
        <td>${codigoProduto.options[codigoProduto.selectedIndex].text}</td>
        <td>${quantidade}</td>
        <td>${valorUnitario}</td>
        <td>${subtotal}</td>
    </tr>`
    gridProdutos.innerHTML += linhaGrid

    let total = Number(document.getElementById("txtTotal").value.toString().replace(',', '.')) +
        Number(subtotal)

    document.getElementById("txtTotal").value = total

    document.getElementById("txtQtde").value = ""
    document.getElementById("txtPrecoUnitario").value = ""
    document.getElementById("txtSubTotal").value = ""
    document.getElementById("cboProduto").value = ""
}

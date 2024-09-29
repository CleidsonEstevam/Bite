$(document).ready(function () {
    $('#novoTelefone').inputmask('(99) 99999-9999', { 'placeholder': ' ' });
});


    $('#btnCadastrar').click(function (e) {
        debugger
        e.preventDefault();



        var nome = $('#novoNome').val();
        var nomeEmpresa = $('#novoNomeEmpresa').val();
        var telefone = $('#novoTelefone').val().replace(/\D/g, '');
        var email = $('#novoEmail').val();
        var senha = $('#novaSenha').val();
        var senha2 = $('#novaSenha2').val();

        if (senha !== senha2) {
            alert("As senhas não conferem!");
            return;
        }

        var usuarioData = {
            Nome: nome,
            NomeEmpresa: nomeEmpresa,
            Telefone: telefone,
            Email: email,
            Senha: senha
        };

      $.ajax({
            url: '/Usuario/CadastrarUsuario', 
            type: 'POST',
            data: usuarioData,
            success: function(response) {
                if (response.success) {
                    // Lógica para redirecionar ou mostrar mensagem de sucesso
                    alert('Usuário cadastrado com sucesso!');
                } else {
                    // Exibe os erros de validação
                    var errors = response.errors;
                    var errorMessages = '';
                    errors.forEach(function(error) {
                        errorMessages += error + '\n'; // Concatena mensagens de erro
                    });
                    alert('Erros:\n' + errorMessages); // Exibe erros em um alert (ou use outra forma de mostrar)
                }
            },
            error: function(xhr, status, error) {
                alert('Ocorreu um erro ao cadastrar o usuário.'); // Mensagem genérica de erro
            }
        });
    });



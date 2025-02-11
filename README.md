📌 Guia de Configuração do Projeto CadastroCliente

🛠️ Passo 1: Rodar a Migration

Para atualizar a migration do banco de dados:

Abra o Package Manager Console no Visual Studio:

Vá até Ferramentas > NuGet Package Manager > Package Manager Console.

Execute o seguinte comando:

Update-Database -Project CadastroCliente.Data -StartupProject CadastroCliente.API

📄 Passo 2: Executar os Scripts SQL no Banco de Dados

Após rodar as migrations, rode os scripts SQL necessários para inserir dados e configurar o sistema.

🔑 2.1 Inserir a Chave de Autenticação

Execute o seguinte comando no banco de dados para adicionar uma nova chave de autenticação:

INSERT INTO AuthKey (CompanyName, [Key], CreatedAt)
VALUES ('ThomasGreg', NEWID(), GETDATE())

🔍 2.2 Selecionar a Chave de Autenticação

Após a inserção, recupere a chave gerada com:

SELECT [Key] FROM AuthKey WHERE CompanyName = 'ThomasGreg'

📝 Passo 3: Atualizar o appsettings.json do Projeto Web

Agora, adicione a chave obtida ao arquivo appsettings.json do projeto Web:

Abra o arquivo appsettings.json em CadastroCliente.Web.

Encontre a seção Authenticate e adicione a chave copiada:

{
  "ConnectionStrings": {
    "DefaultConnection": "SuaConnectionStringAqui"
  },
  "Authenticate": {
    "Key": "ValorDaChaveCopiadoDoBancoDeDados"
  }
}

📌 Nota: Substitua ValorDaChaveCopiadoDoBancoDeDados pela chave gerada no banco de dados.

🚀 Passo 4: Iniciar o Projeto

Após configurar o banco de dados e o arquivo appsettings.json, siga os passos abaixo para iniciar o projeto:

Abra o projeto CadastroCliente.API no Visual Studio.

Verifique se a API está configurada corretamente para consumir a chave de autenticação.

Configure os projetos de inicialização:

Vá até Projeto > Configurar Projetos de Inicialização.

Na aba Configurar Projetos de Inicialização, selecione Vários projetos de inicialização.

Defina CadastroCliente.Web e CadastroCliente.API como Iniciar.

Configure o Destino de depuração para IIS Express.

Clique em Aplicar.

Inicie o projeto clicando em Iniciar (ícone de play) ou pressionando F5.

✅ Agora seu projeto está pronto para uso! 🚀

Passo 1: Rodar a Migration
Abra o Package Manager Console no Visual Studio:
Vá até Ferramentas > NuGet Package Manager > Package Manager Console.
Execute o comando para atualizar a migration do banco de dados:
bash
Update-Database -Project CadastroCliente.Data -StartupProject CadastroCliente.API
Passo 2: Rodar os Scripts SQL no Banco de Dados
Depois de rodar as migrations, é necessário rodar alguns scripts SQL para inserir dados e configurar o sistema.
2.1 Inserir a Chave de Autenticação
Execute o seguinte script SQL para adicionar uma nova chave de autenticação na tabela AuthKey:

INSERT INTO AuthKey(CompanyName, [Key], CreatedAt) 
VALUES ('ThomasGreg', NEWID(), GETDATE())
2.2 Selecionar a Chave de Autenticação
Após rodar o comando de inserção, execute o comando abaixo para recuperar a chave gerada:

SELECT [Key] 
FROM AuthKey 
WHERE CompanyName = 'ThomasGreg'

Passo 3: Atualizar o appsettings.json do Projeto Web
Após inserir a chave no banco de dados e obter o valor de retorno, é necessário atualizar a configuração no arquivo appsettings.json do projeto Web.
Abrir o arquivo appsettings.json no projeto CadastroCliente.Web.
Atualizar o campo AuthenticateKey:
Encontre a chave de configuração Authenticate Key no arquivo appsettings.json e adicione o valor da chave gerada no banco de dados. O arquivo deve ficar parecido com isso:
json

{
  "ConnectionStrings": {
    "DefaultConnection": "SuaConnectionStringAqui"
  },
  "Authenticate" {
		“Key": "ValorDaChaveCopiadoDoBancoDeDados"
   }
}


Descrição: Substitua "ValorDaChaveCopiadoDoBancoDeDados" pela chave gerada no banco de dados na etapa anterior. Esse valor será usado para autenticar as requisições na API.

Passo 4: Iniciar o Projeto
Após a configuração do banco de dados e do arquivo appsettings.json, siga os seguintes passos para iniciar o aplicativo:
Abra o projeto CadastroCliente.API no Visual Studio.
Verifique se a API está configurada corretamente para consumir a chave de autenticação no appsettings.json.
Abra Configurar Projetos de Inicialização em Projeto > Configurar Projetos de Inicialização, na aba Configurar Projetos de Inicialização selecione Vários projetos de inicialização em CadastroCliente.Web e CadastroCliente.API coloque ação em Iniciar e Destino de depuração em IIS Express e aplique.
Inicie o projeto clicando em Iniciar (ícone de play) ou pressionando F5.

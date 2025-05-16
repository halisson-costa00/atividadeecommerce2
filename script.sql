create database dbAtividadeEcommerce2;

use dbAtividadeEcommerce2;
CREATE TABLE tbUsuario (
    Id INT PRIMARY KEY AUTO_INCREMENT,         -- ID único, gerado automaticamente
    Nome VARCHAR(50) NOT NULL,                 -- Nome do usuário
    Email VARCHAR(50) NOT NULL UNIQUE,         -- Email único para evitar duplicidade
    Senha VARCHAR(50) NOT NULL                 -- Senha do usuário
);

-- Cria a tabela de clientes (usuários que compram na loja)
CREATE TABLE tbCliente (
    CodCli INT PRIMARY KEY AUTO_INCREMENT,     -- Código do cliente, gerado automaticamente
    NomeCli VARCHAR(50) NOT NULL,              -- Nome do cliente
    TelCli VARCHAR(20) NOT NULL,               -- Telefone para contato
    EmailCli VARCHAR(50) NOT NULL              -- Email do cliente (não está como único, mas pode ser)
);

-- Cria a tabela de produtos vendidos no e-commerce
CREATE TABLE tbProduto (
    CodProd INT PRIMARY KEY AUTO_INCREMENT,    -- Código do produto, gerado automaticamente
    Nome VARCHAR(100),                         -- Nome do produto
    Descricao TEXT,                            -- Descrição detalhada
    Quantidade INT,                            -- Quantidade em estoque
    Preco DECIMAL(10,2)                       -- Preço com 2 casas decimais
);

-- Consulta todos os dados da tabela Usuario
SELECT * FROM tbUsuario;

-- Consulta todos os dados da tabela Cliente
SELECT * FROM tbCliente;

-- Consulta todos os dados da tabela Produto
SELECT * FROM tbProduto;

insert into tbUsuario (Nome,Email,Senha)values
('Hn', 'hn@gmail.com', '1234');

CREATE DATABASE PizzaLinkMVC;
USE PizzaLinkMVC;

-- Como armazenar Senhas sem ser em VARCHAR para manter o sistema mais seguro (?) (?) (?)
-- Armazenar como HASH (?) (?) (?)
CREATE TABLE Usuario (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Login VARCHAR(50) NOT NULL UNIQUE,
    Senha VARCHAR(50) NOT NULL,
    NivelAcesso CHAR(1) NOT NULL -- 'A' admin; 'F' funcionario
);
GO

CREATE TABLE Cliente (
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Cpf VARCHAR(14) UNIQUE,
    Endereco VARCHAR(200)
);
GO

CREATE TABLE Produto (
    ProdutoId INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Tipo CHAR(1) NOT NULL, -- 'P' pizza; 'B' bebida; 'L' lanche
    Preco DECIMAL(10, 2) NOT NULL,
    Estoque INT NOT NULL
);
GO

CREATE TABLE Pedido (
    PedidoId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL, 
    ClienteId INT NOT NULL, 
    DataHora DATETIME NOT NULL,
    ValorTotal DECIMAL(10, 2) NOT NULL,
    Status CHAR(1) NOT NULL, -- 'P' pendente; 'F' finalizado; 'C' cancelado
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)
);
GO

-- Relação entre Pedido e Produto
CREATE TABLE ItemPedido (
    ItemPedidoId INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL, 
    ProdutoId INT NOT NULL, 
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10, 2) NOT NULL, 
    Subtotal AS (Quantidade * PrecoUnitario), 
    FOREIGN KEY (PedidoId) REFERENCES Pedido(PedidoId),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(ProdutoId)
);
GO

-- Inserir usuario Master para administrar o programa
-- Como manter este script seguro para ninguém invadir o sistema (?) (?) (?)
INSERT INTO Usuario (Nome, Login, Senha, NivelAcesso) 
VALUES ('Administrador', 'admin', 'admin', 'A');
GO

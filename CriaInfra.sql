-------------------------------------------------------------------------------------------------------------------------------------
-- Script criado por Gabriel Ítalo em 20/05/2020
-- Tem por finalidade criar a estrutura para o desafio dos carros e ainda inserir dados testes.
-------------------------------------------------------------------------------------------------------------------------------------

-- Verificando a existência da tabela Marca, se não existir é criado
If Not Exists(Select Name From sys.objects Where (Name = 'Marca'))
	Begin
		Create Table Marca
		(
			Pk int NOT NULL IDENTITY(1,1),
			Marca varchar(80),
			PRIMARY KEY (Pk)
		)
	End
Else
	Begin
		-- Se existir irá ser deletado seus registros
		Delete Marca
	End


-- Verificando a existência da tabela Modelo, se não existir é criado
If Not Exists(Select Name From sys.objects Where (Name = 'Modelo'))
	Begin
		Create Table Modelo
		(
			Pk int NOT NULL IDENTITY(1,1),
			FkMarca int,
			Modelo varchar(100),
			Ano char(4),
			VrCompra numeric(18,2),
			VrVenda numeric(18,2),
			Cor varchar(30),
			Combustivel varchar(50)
			PRIMARY KEY (Pk)
		)
	End
Else
	Begin
		-- Se existir irá ser deletado seus registros
		Delete Modelo
	End

-- Verificando a existência da tabela Marca, se não existir é criado
If Not Exists(Select Name From sys.objects Where (Name = 'Anuncio'))
	Begin
		Create Table Anuncio
		(
			Pk int NOT NULL IDENTITY(1,1),
			FkModelo int,
			DataCricao datetime,
			DataVenda datetime,
			VrVenda numeric(18, 2),
			Vendido char(3)
			PRIMARY KEY (Pk)
		)
	End
Else
	Begin
		-- Se existir irá ser deletado seus registros
		Delete Anuncio
	End

If Not Exists(Select Name From sys.objects Where (Name = 'Usuario'))
	Begin
		Create Table Usuario
		(
			Pk int NOT NULL IDENTITY(1,1),
			Nome varchar(120),
			Senha varchar(50),
			PRIMARY KEY (Pk)
		)
	End
Else
	Begin
		-- Se existir irá ser deletado seus registros
		Delete Usuario
	End
------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Gerando os dados iniciais de demonstração.

-- Se a tabela existir irá popular
If Exists(Select Name From sys.objects Where (Name = 'Marca'))
	Begin
		Insert Marca(Marca)
		Select 'Honda'

		Insert Marca(Marca)
		Select 'Fiat'

		Insert Marca(Marca)
		Select 'Toyota'
	End

-- Se a tabela existir irá popular
If Exists(Select Name From sys.objects Where (Name = 'Modelo'))
	Begin
		Insert Modelo(FkMarca, Modelo, Ano, VrCompra, VrVenda, Cor, Combustivel)
		Select (Select Pk From Marca Where (Marca = 'Honda')), 
		'Civic LXL', '2020', 98890.99, 75500, 'Prata', 'Flex'

		Insert Modelo(FkMarca, Modelo, Ano, VrCompra, VrVenda, Cor, Combustivel)
		Select (Select Pk From Marca Where (Marca = 'Fiat')), 
		'Argo', '2020', 46500, 31000, 'Branco', 'Flex'

		Insert Modelo(FkMarca, Modelo, Ano, VrCompra, VrVenda, Cor, Combustivel)
		Select (Select Pk From Marca Where (Marca = 'Toyota')), 
		'Corolla', '2020', 112500, 103500, 'Preto', 'Flex'
	End

-- Se a tabela existir irá popular
If Exists(Select Name From sys.objects Where (Name = 'Anuncio'))
	Begin
		Insert Anuncio(FkModelo, DataCricao, DataVenda)
		Select (Select Pk From Modelo Where (Modelo = 'Civic LXL')), 
		GETDATE(), DateAdd(month, 1, GETDATE())
	End

-- Se a tabela existir irá popular
If Exists(Select Name From sys.objects Where (Name = 'Usuario'))
	Begin
		Insert Usuario(Nome, Senha)
		Select 'root', '123'
	End


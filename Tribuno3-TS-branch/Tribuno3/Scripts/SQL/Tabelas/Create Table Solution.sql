
CREATE TABLE TipoOperacaoDetalhe
	(
		TipoOperacao  tinyint Primary Key identity,
		Descricao varchar(20),	
	)
    Insert into TipoOperacaoDetalhe values('NaoDefinid')		
	Insert into TipoOperacaoDetalhe values('Rendimento')		
	Insert into TipoOperacaoDetalhe values('Passivo')

	CREATE TABLE TipoCalculoDetalhe
	(
		TipoCalculo  tinyint Primary Key identity,
		Descricao varchar(20),	
	)
	Insert into TipoCalculoDetalhe values('NaoDefinid')
	Insert into TipoCalculoDetalhe values('Parcela')
	Insert into TipoCalculoDetalhe values('Operacao')

	CREATE TABLE Operacao (
	
		IdOperacao      bigint       not null Primary key identity,
		IdUsuario       bigint       not null,	
		NomeOperacao    varchar(30)  not null,
		ValorOperacao   money        not null,
		ValorParcela    money,
		QtdParcela      int          not null,		
		Descricao       varchar(100),	
		DataCadastro    datetime,
		DataAlteracao   datetime,
		TipoOperacao    tinyint      not null,
		TipoCalculo     tinyint,  
	    CONSTRAINT fk_Usuario FOREIGN KEY (IdUsuario) REFERENCES USER_LOGIN (ID_USUARIO),
		CONSTRAINT fk_TipoOperacao FOREIGN KEY (TipoOperacao) REFERENCES TipoOperacaoDetalhe (TipoOperacao),
		CONSTRAINT fk_Tipocalculo FOREIGN KEY (TipoCalculo) REFERENCES TipoCalculoDetalhe (TipoCalculo)
	)	

	CREATE TABLE ParcelasDetalhe(	
		StatusParcela  tinyint Primary Key identity,
		Descricao varchar(10),	
	)			
		Insert into ParcelasDetalhe values('NaoDefinid')		
		Insert into ParcelasDetalhe values('EmAberto')		
		Insert into ParcelasDetalhe values('Vencido')
		Insert into ParcelasDetalhe values('Pago')

	CREATE TABLE OperacaoParcelas(
	 IdParcela		bigint Primary Key identity,
	 IdOperacao		bigint not null,
	 NumeroParcela  int not null,
	 Valor_Parcela  money not null,
	 DataVencimento datetime not null,
	 StatusParcela  tinyint not null,	

	 CONSTRAINT fk_Operacao FOREIGN KEY (IdOperacao) REFERENCES Operacao (IdOperacao),
	 CONSTRAINT fk_Status FOREIGN KEY (StatusParcela) REFERENCES ParcelasDetalhe (StatusParcela),
	)


	
	select * from TipoOperacaoDetalhe
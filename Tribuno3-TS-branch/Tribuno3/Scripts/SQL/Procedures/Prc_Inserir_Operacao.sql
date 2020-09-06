alter Procedure Prc_Inserir_Operacao             
		@IdUsuario       bigint,	
		@NomeOperacao    varchar(30),
		@ValorOperacao   money,
		@ValorParcela    money,
		@QtdParcela      int,		
		@Descricao       varchar(100),	
		@DataCadastro    datetime,		
		@TipoOperacao    tinyint,
		@TipoCalculo     tinyint 
AS

BEGIN	    	      
	   INSERT INTO Operacao(IdUsuario,NomeOperacao,ValorOperacao,ValorParcela,QtdParcela,Descricao,DataCadastro,TipoOperacao,TipoCalculo) 
	   VALUES (@IdUsuario,@NomeOperacao,@ValorOperacao,@ValorParcela,@QtdParcela,@Descricao,@DataCadastro,@TipoOperacao,@TipoCalculo)	      

	   SELECT  SCOPE_IDENTITY()
END
GO





Create PROCEDURE Prc_Alterar_Operacao
        @IdOperacao      bigint,      
		@IdUsuario       bigint,	
		@NomeOperacao    varchar(30),
		@ValorOperacao   money,
		@ValorParcela    money,
		@QtdParcela      int,		
		@Descricao       varchar(100),	
		@DataAlteracao   datetime,
		@TipoOperacao    tinyint,
		@TipoCalculo     tinyint 	
AS
BEGIN
     UPDATE Operacao 
     SET   
		   IdUsuario     = @IdUsuario,
		   NomeOperacao  = @NomeOperacao, 
		   ValorOperacao = @ValorOperacao,
		   ValorParcela  = @ValorParcela,
		   QtdParcela    = @QtdParcela,
		   Descricao     = @Descricao,
		   DataAlteracao = @DataAlteracao,
		   TipoOperacao  = @TipoOperacao,
		   TipoCalculo   = @TipoCalculo
     WHERE IdOperacao = @IdOperacao and IdUsuario = @IdUsuario
END



	  
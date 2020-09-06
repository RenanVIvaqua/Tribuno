  
  Create Procedure Prc_Listar_Operacao 
  
    @IdUsuario          bigint,
	@TipoOperacao       tinyint
	as
	 SELECT 
	   IdOperacao,
	   IdUsuario,
	   NomeOperacao,
	   ValorOperacao,
	   ValorParcela,
	   QtdParcela,
	   Descricao,
	   DataCadastro,
	   DataAlteracao,
	   TipoOperacao,
	   TipoCalculo
	   FROM Operacao 
	   WHERE IdUsuario = @IdUsuario and TipoOperacao = @TipoOperacao

	   
			
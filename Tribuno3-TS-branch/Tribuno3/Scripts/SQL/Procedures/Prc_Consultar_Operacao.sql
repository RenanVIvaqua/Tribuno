Create Procedure Prc_Consultar_Operacao
    @IdUsuario          bigint,
	@IdOperacao         bigint
AS

BEGIN	    	      
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
	   WHERE IdOperacao = @IdOperacao AND IdUsuario = @IdUsuario
END

GO




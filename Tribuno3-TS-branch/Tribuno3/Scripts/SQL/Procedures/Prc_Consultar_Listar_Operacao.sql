
Create Procedure Prc_Consultar_Operacao_Parcelas
  @IdOperacao          bigint
  as
  BEGIN
	 SELECT IdParcela
		   ,IdOperacao
		   ,NumeroParcela
		   ,Valor_Parcela
		   ,DataVencimento
		   ,StatusParcela 
	 FROM OperacaoParcelas
	 WHERE IdOperacao = @IdOperacao
   END


 CREATE PROC Prc_Resumo_Financeiro_Mes 
   @ID_USUARIO INT
  ,@MES        SMALLINT
 AS
BEGIN

DECLARE  @RENDIMENTO FLOAT
DECLARE  @DESPESA    FLOAT
DECLARE  @RECEITA    FLOAT
DECLARE  @LUCRO      FLOAT	
		
	set  @RENDIMENTO  = (SELECT SUM(OperacaoParcelas.Valor_Parcela) AS RECEITA  
	FROM Operacao 
			            INNER JOIN OperacaoParcelas  ON Operacao.IdOperacao = OperacaoParcelas.IdOperacao
			            WHERE Operacao.IdUsuario = @ID_USUARIO
			            AND   DataVencimento BETWEEN dbo.MES_Passado_DIni(@MES)
			            AND   dbo.MES_Passado_DFim(@MES)
						AND   Operacao.TipoOperacao = 2)

    set  @DESPESA  = (SELECT SUM(OperacaoParcelas.Valor_Parcela) AS DESPESA  
	FROM Operacao 
			            INNER JOIN OperacaoParcelas  ON Operacao.IdOperacao = OperacaoParcelas.IdOperacao
			            WHERE Operacao.IdUsuario = @ID_USUARIO
			            AND   DataVencimento BETWEEN dbo.MES_Passado_DIni(@MES)
			            AND   dbo.MES_Passado_DFim(@MES)
						AND   Operacao.TipoOperacao = 3)   

	SET @RECEITA = @RENDIMENTO - @DESPESA

	SET @LUCRO =  ROUND(@RECEITA / @RENDIMENTO * 100 , 2)
	
		SELECT @ID_USUARIO as ID_USUARIO,
		       @RENDIMENTO AS RENDIMENTO,
			   @DESPESA    AS DESPESA,
			   @RECEITA    AS RECEITA,
			   @LUCRO AS LUCRO ,
			  Datename(Month,dbo.MES_Passado_DFim(@MES)) as MES_REF

	SET @RECEITA = @RENDIMENTO - @DESPESA

	SET @LUCRO =  ROUND(@RECEITA / @RENDIMENTO * 100 , 2)
	
		SELECT @ID_USUARIO as ID_USUARIO,
		       @RENDIMENTO AS RENDIMENTO,
			   @DESPESA    AS DESPESA,
			   @RECEITA    AS RECEITA,
			   @LUCRO AS LUCRO ,
			  Datename(Month,dbo.MES_Passado_DFim(@MES)) as MES_REF
END




	CREATE Procedure Prc_Inserir_Parcelas_Operacao
		@IdOperacao     bigint,
		@NumeroParcela  int,
		@Valor_Parcela  money,
		@DataVencimento datetime,
		@StatusParcela  tinyint
		
	as
	Begin	
		INSERT INTO OperacaoParcelas (IdOperacao,NumeroParcela,Valor_Parcela,DataVencimento,StatusParcela) 
		VALUES(@IdOperacao,@NumeroParcela,@Valor_Parcela,@DataVencimento,@StatusParcela)
	end


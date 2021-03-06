
Create Procedure Prc_Grafico_Configuracao_Mes

@MesAnterior int,
@MesPosterior int

as

IF OBJECT_ID('tempdb..#Mes') IS NOT NULL
BEGIN Drop table #Mes END

Create table #Mes(Mes varchar(MAX))

DECLARE @cnt INT,
		@cnt1 INT = 0

		set @cnt = @MesAnterior

WHILE @cnt >= 1
BEGIN
   insert into #Mes values((SELECT FORMAT(dbo.MES_Passado_DIni(@cnt),'MMMM')  + '-' + FORMAT(dbo.MES_Passado_DIni(@cnt),'yyyy')))
   set @cnt = @cnt -1
END

WHILE @cnt1 <= @MesPosterior
BEGIN
   insert into #Mes values((SELECT FORMAT(dbo.MES_Futuro_DIni(@cnt1),'MMMM')  + '-' + FORMAT(dbo.MES_Futuro_DIni(@cnt1),'yyyy')))
   print @cnt1
   set @cnt1 = @cnt1 +1
END
select * from #Mes
GO


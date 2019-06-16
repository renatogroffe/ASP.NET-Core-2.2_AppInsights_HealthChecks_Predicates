USE BaseIndicadores
GO

CREATE TABLE dbo.Indicadores(
	Sigla VARCHAR(10) NOT NULL,
	NomeIndicador VARCHAR(60) NOT NULL,
	UltimaAtualizacao DATETIME NOT NULL,
	Valor NUMERIC (18,4) NOT NULL,
	CONSTRAINT PK_Indicadores PRIMARY KEY (Sigla)
)
GO


INSERT INTO dbo.Indicadores
           (Sigla
           ,NomeIndicador
           ,UltimaAtualizacao
           ,Valor)
     VALUES
           ('SALARIO'
           ,'Salario minimo'
           ,'2019-01-01'
           ,998.00)


INSERT INTO dbo.Indicadores
           (Sigla
           ,NomeIndicador
           ,UltimaAtualizacao
           ,Valor)
     VALUES
           ('IPCA'
           ,'Indice Nacional de Precos ao Consumidor Amplo'
           ,'2019-05-01'
           ,0.0013)


INSERT INTO dbo.Indicadores
           (Sigla
           ,NomeIndicador
           ,UltimaAtualizacao
           ,Valor)
     VALUES
           ('SELIC'
           ,'Taxa Referencial - Sistema de Liquidacao e Custodia'
           ,'2019-05-08'
           ,0.065)
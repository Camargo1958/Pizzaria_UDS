USE [pizzariaUDS]
GO

/****** Object:  Table [dbo].[Rpedidos]    Script Date: 08/09/2018 11:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rpedidos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [nchar](10) NULL,
	[nomecliente] [nvarchar](50) NULL,
	[tamanho_pizza] [nchar](10) NULL,
	[sabor_pizza] [nvarchar](50) NULL,
	[extras_pizza] [nvarchar](50) NULL,
	[valor_total] [nchar](10) NULL,
	[tempo_prep] [nchar](10) NULL,
	[status] [nchar](10) NULL,
	[val_extras] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


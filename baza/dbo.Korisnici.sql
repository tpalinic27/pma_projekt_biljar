CREATE TABLE [dbo].[Korisnici] (
    [Id_korisnici] INT          IDENTITY (1, 1) NOT NULL,
    [Email]        VARCHAR (50) NOT NULL,
    [Password]     VARCHAR (50) NOT NULL,
    [Liga]         VARCHAR (50) NULL,
    [Id_klubovi]   INT          NULL,
    [Username] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED ([Id_korisnici] ASC),
    CONSTRAINT [FK_Korisnici_Klubovi] FOREIGN KEY ([Id_klubovi]) REFERENCES [dbo].[Klubovi] ([Id_klubovi])
);


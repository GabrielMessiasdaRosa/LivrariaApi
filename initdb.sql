-- Cria o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LivrariaDb')
BEGIN
    CREATE DATABASE [LivrariaDb];
END
GO

ALTER LOGIN sa WITH PASSWORD = '$wtBv4z!Q*AJhA5u';
ALTER LOGIN sa ENABLE;
GO

-- Usa o banco de dados (necessário usar 'EXEC' para alternar corretamente)
EXEC('USE LivrariaDb');

-- Cria a tabela Livros se não existir
IF NOT EXISTS (SELECT * FROM LivrariaDb.sys.objects WHERE name = 'Livros' AND type = 'U')
BEGIN
    CREATE TABLE LivrariaDb.dbo.Livros (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Titulo NVARCHAR(255) NULL,
        Autor NVARCHAR(255) NULL,
        AnoPublicacao INT NULL
    );
END
GO

-- Insere dados iniciais apenas se ainda não existirem dados
IF NOT EXISTS (SELECT * FROM LivrariaDb.dbo.Livros)
BEGIN
    INSERT INTO LivrariaDb.dbo.Livros (Titulo, Autor, AnoPublicacao) 
    VALUES 
        ('Jogador Nº 1', 'Ernest Cline', 2011),
        ('O Senhor dos Anéis: A Sociedade do Anel', 'J.R.R. Tolkien', 1954),
        ('Neuromancer', 'William Gibson', 1984),
        ('Duna', 'Frank Herbert', 1965),
        ('Fundação', 'Isaac Asimov', 1951),
        ('1984', 'George Orwell', 1949),
        ('O Guia do Mochileiro das Galáxias', 'Douglas Adams', 1979),
        ('Snow Crash', 'Neal Stephenson', 1992),
        ('O Jogo do Exterminador', 'Orson Scott Card', 1985),
        ('A Máquina do Tempo', 'H.G. Wells', 1895);
END
GO
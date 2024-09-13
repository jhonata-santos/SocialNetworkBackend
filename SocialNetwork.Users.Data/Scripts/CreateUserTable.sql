CREATE TABLE Users (
    ID INT NOT NULL AUTO_INCREMENT,            -- Chave primária com auto incremento
    NAME VARCHAR(50) NOT NULL,                 -- Nome do usuário
    DATE_OF_BIRTH DATE,                        -- Data de nascimento
    CPF VARCHAR(11) NOT NULL,                  -- Nome do usuário
    EMAIL VARCHAR(50) NOT NULL UNIQUE,         -- Email único
    PASSWORD VARCHAR(50) NOT NULL,             -- Hash da senha
    CREATE_AT DATETIME,                        -- Data de criação do registro
    UPDATE_AT DATETIME,                        -- Data de atualização
    PRIMARY KEY (ID)                           -- Definindo chave primária
);
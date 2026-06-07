USE ingressoja;

-- Cria a tabela se ela não existir
CREATE TABLE IF NOT EXISTS Users (
    Id VARCHAR(36) NOT NULL PRIMARY KEY,
    `Role` INT NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    Cpf VARCHAR(11) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    photo_profile TEXT,
    password_hash VARCHAR(255) NOT NULL,
    date_birth DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NULL DEFAULT NULL
);

-- Agora o INSERT funciona porque a tabela existe!
INSERT INTO Users (Id, `Role`, first_name, last_name, Cpf, Email, photo_profile, password_hash, date_birth, created_at, updated_at)
VALUES (
    '85ad8a68-a2d7-4be3-aa89-3081785e7fae',
    2,
    'Jalin',
    'Rabei',
    '66677788899',
    'jalin@rabei.com',
    'https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%2Fid%2FOIP.tlmH12J-KdS7vw_bon4L8gHaIo%3Fpid%3DApi&f=1&ipt=6c67c401b347e060d3880e6042f8b1f84a14ccae174c1ffe661c338f5e99c101&ipo=images',
    '$2a$11$ePma/r3xiSKYNjEyeMYRb.D9ABE6.nIZ1GLynMBhvRbpy8VQes/Ey',
    '2000-03-20',
    '2026-06-07 03:47:00.837731',
    NULL
);
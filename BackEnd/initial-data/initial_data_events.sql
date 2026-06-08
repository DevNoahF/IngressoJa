USE ingressoja;


CREATE TABLE IF NOT EXISTS Events (
                                      Id VARCHAR(36) NOT NULL PRIMARY KEY,
    `Name` VARCHAR(55) NOT NULL,
    `Description` VARCHAR(255) NOT NULL,
    street_name VARCHAR(55) NOT NULL,
    Neighborhood VARCHAR(55) NOT NULL,
    City VARCHAR(55) NOT NULL,
    Number INT NOT NULL,
    State INT NOT NULL,
    Date DATE NOT NULL,
    Hour TIME NOT NULL,
    ticket_value DECIMAL(18,2) NOT NULL,
    total_ticket_quantity INT NOT NULL,
    user_id VARCHAR(36) NOT NULL,
    Status INT NOT NULL,
    banner_image VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NULL DEFAULT NULL
    );


INSERT INTO Events (Id, `Name`, `Description`, street_name, Neighborhood, City, Number, State, Date, Hour, ticket_value, total_ticket_quantity, user_id, Status, banner_image, created_at, updated_at)
VALUES (
           'a1b2c3d4-e5f6-7890-abcd-ef1234567890',
           'Show da SeManca e SeMata',
           'Uma noite inesquecível onde a SeManca toca trompete com os pés e a SeMata ensina passos de dança proibidos até pela física. Riso garantido ou SeMata direto para sua casa.',
           'Rua dos Tropeços',
           'Vila do Riso',
           'Cidade Imaginária',
           13,
           1,
           '2026-12-31',
           '23:59:00',
           99.90,
           420,
           '85ad8a68-a2d7-4be3-aa89-3081785e7fae',
           1,
           'https://i.pinimg.com/736x/d9/7a/bc/d97abc082d68c33519d3188e32fc32a9.jpg',
           '2026-06-07 04:00:00',
           NULL
       );
INSERT INTO Events (Id,Name,Description,street_name,Neighborhood,City,`Number`,State,`Date`,`Hour`,ticket_value,total_ticket_quantity,user_id,Status,banner_image,created_at,updated_at) 
VALUES(
    '523396aa-aa2f-44ba-a75d-18668c5ebe38','Encontro de perolas','Venha conhecer nossa perolas nesse belo evento','sim','sim','sim',67,25,'2026-06-16','15:08:00',67.00,67,'85ad8a68-a2d7-4be3-aa89-3081785e7fae',1,'https://i.pinimg.com/736x/e0/0d/fd/e00dfdbc046db93b4477a30580f23627.jpg','2026-06-08 00:02:16.834897',NULL),
	 ('8217fe84-2d16-4776-873d-6cc3adbed63d','Palestra de como cuidar bem de seus maridos','Venha conhecer nossa pastora abençoada que irá ensinar como cuidar bem de seu marido <3','ceu','ceu','Ceu ou Inferno',1,25,'2026-06-24','00:12:00',1.99,50,'85ad8a68-a2d7-4be3-aa89-3081785e7fae',1,'https://i.pinimg.com/736x/5e/27/e5/5e27e5cddd8aaf30b9df1942a2483c24.jpg','2026-06-08 00:06:21.478620',NULL),
	 ('9b65f1b7-d0c4-478e-a0be-5784821caab5','Festa com dj cat','Muita festa e alucinógenos.','casa da esquina','bairro da casa da esquina','Marilia',67,25,'2026-06-08','20:30:00',200.00,1000,'85ad8a68-a2d7-4be3-aa89-3081785e7fae',1,'https://i.pinimg.com/736x/52/85/f2/5285f24557a4283a5470d615def55380.jpg','2026-06-07 23:59:10.917513',NULL),
	 ('f3275328-702e-453e-8fb7-c2917d69bfd6','Curso de agiotagem - Senai','aprenda a fazer a melhor agiotagem do brasil com o senai, venha!
Vagas limitadas!','sim','tenho','Marilia',666,25,'2027-03-03','03:03:00',200.00,30,'85ad8a68-a2d7-4be3-aa89-3081785e7fae',1,'https://i.pinimg.com/736x/0f/b8/d4/0fb8d43697f927d189aeb1255cbcc858.jpg','2026-06-07 23:51:48.648863',NULL);

USE ingressoja;


CREATE TABLE IF NOT EXISTS `Events` (
                                        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Name` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `street_name` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `Neighborhood` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `City` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `State` int NOT NULL,
    `Date` date NOT NULL,
    `Hour` time NOT NULL,
    `ticket_value` decimal(18,2) NOT NULL,
    `total_ticket_quantity` int NOT NULL,
    `user_id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Status` int NOT NULL,
    `banner_image` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `created_at` datetime(6) NOT NULL,
    `updated_at` datetime(6) NULL,
    CONSTRAINT `PK_Events` PRIMARY KEY (`Id`)
    )

INSERT INTO `Events` (
    `Id`, `Name`, `Description`, `street_name`, `Neighborhood`,
    `City`, `Number`, `State`, `Date`, `Hour`,
    `ticket_value`, `total_ticket_quantity`, `user_id`,
    `Status`, `banner_image`, `created_at`, `updated_at`
)
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
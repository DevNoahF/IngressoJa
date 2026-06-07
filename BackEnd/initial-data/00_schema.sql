CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Events` (
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
) CHARACTER SET=utf8mb4;

CREATE TABLE `Sales` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `user_id` char(36) COLLATE ascii_general_ci NOT NULL,
    `event_id` char(36) COLLATE ascii_general_ci NOT NULL,
    `TicketId` char(36) COLLATE ascii_general_ci NULL,
    `selected_tickets_user` int NOT NULL,
    `total_price` double precision NOT NULL,
    `created_at` datetime(6) NOT NULL,
    `sale_status` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Sales` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Tickets` (
    `Code` char(36) COLLATE ascii_general_ci NOT NULL,
    `user_id` char(36) COLLATE ascii_general_ci NOT NULL,
    CONSTRAINT `PK_Tickets` PRIMARY KEY (`Code`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Role` int NOT NULL,
    `first_name` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `last_name` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `Cpf` varchar(11) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `photo_profile` varchar(255) CHARACTER SET utf8mb4 NULL,
    `password_hash` varchar(12) CHARACTER SET utf8mb4 NOT NULL,
    `Token` varchar(255) CHARACTER SET utf8mb4 NULL,
    `date_birth` date NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260526015220_InitialMigrations', '9.0.0');

ALTER TABLE `Users` MODIFY COLUMN `password_hash` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

CREATE UNIQUE INDEX `IX_Users_Cpf` ON `Users` (`Cpf`);

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260529214756_FixUserPasswordLengthAndUniqueConstraints', '9.0.0');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260529215747_FixPasswordHashMappingAgain', '9.0.0');

ALTER TABLE `Users` DROP COLUMN `Token`;

ALTER TABLE `Users` ADD `created_at` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Users` ADD `updated_at` datetime(6) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260530052121_RemoveTokenAddTimestamps', '9.0.0');

COMMIT;


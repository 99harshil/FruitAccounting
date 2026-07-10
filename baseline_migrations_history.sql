-- Creates EF Core's migrations tracking table and marks InitialCreate as already
-- applied (since it captures the schema as it already exists, not as something
-- to run). From here on, `dotnet ef migrations add <Name>` + `database update`
-- is the way to change the schema instead of hand-written SQL.

CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260709173216_InitialCreate', '8.0.28');

-- Verify
SELECT * FROM "__EFMigrationsHistory";

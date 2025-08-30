using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formneo.repository.Migrations
{
    /// <inheritdoc />
    public partial class FixAspNetUsersColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DO $$
            BEGIN
                -- Metin alanları
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='FirstName') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""FirstName"" text NOT NULL DEFAULT '';
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='LastName') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""LastName"" text NOT NULL DEFAULT '';
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='LastLoginIp') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""LastLoginIp"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='manager1') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""manager1"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='manager2') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""manager2"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='profileInfo') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""profileInfo"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='Title') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""Title"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='Location') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""Location"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='FacebookUrl') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""FacebookUrl"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='InstagramUrl') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""InstagramUrl"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='TwitterUrl') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""TwitterUrl"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='LinkedinUrl') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""LinkedinUrl"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='photo') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""photo"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='Department') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""Department"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='SAPDepartmentText') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""SAPDepartmentText"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='SAPPositionText') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""SAPPositionText"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='PCname') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""PCname"" text NULL;
                END IF;

                -- Tarih/saat alanları
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='LastLoginDate') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""LastLoginDate"" timestamp without time zone NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='ResetCodeExpiry') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""ResetCodeExpiry"" timestamp without time zone NULL;
                END IF;

                -- UUID alanları (FK'ler daha sonra eklenebilir)
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='TicketDepartmentId') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""TicketDepartmentId"" uuid NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='WorkCompanyId') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""WorkCompanyId"" uuid NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='PositionId') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""PositionId"" uuid NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='mainManagerUserAppId') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""mainManagerUserAppId"" uuid NULL;
                END IF;

                -- Boolean alanlar
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='isSystemAdmin') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""isSystemAdmin"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='canSsoLogin') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""canSsoLogin"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='isBlocked') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""isBlocked"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='isTestData') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""isTestData"" boolean NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='vacationMode') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""vacationMode"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='HasTicketPermission') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""HasTicketPermission"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='HasDepartmentPermission') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""HasDepartmentPermission"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='HasOtherCompanyPermission') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""HasOtherCompanyPermission"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='HasOtherDeptCalendarPerm') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""HasOtherDeptCalendarPerm"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='canEditTicket') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""canEditTicket"" boolean NOT NULL DEFAULT false;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='DontApplyDefaultFilters') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""DontApplyDefaultFilters"" boolean NOT NULL DEFAULT false;
                END IF;

                -- Diğer alanlar
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='ResetPasswordCode') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""ResetPasswordCode"" text NULL;
                END IF;
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='AspNetUsers' AND column_name='UserLevel') THEN
                    ALTER TABLE ""AspNetUsers"" ADD COLUMN ""UserLevel"" integer NOT NULL DEFAULT 0;
                END IF;
            END
            $$;");
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(8041));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 11, 2, 17, 727, DateTimeKind.Utc).AddTicks(8180));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DO $$
            BEGIN
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""UserLevel"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""ResetPasswordCode"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""DontApplyDefaultFilters"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""canEditTicket"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""HasOtherDeptCalendarPerm"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""HasOtherCompanyPermission"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""HasDepartmentPermission"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""HasTicketPermission"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""vacationMode"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""isTestData"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""isBlocked"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""canSsoLogin"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""isSystemAdmin"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""mainManagerUserAppId"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""PositionId"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""WorkCompanyId"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""TicketDepartmentId"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""ResetCodeExpiry"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""LastLoginDate"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""PCname"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""SAPPositionText"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""SAPDepartmentText"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""Department"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""photo"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""LinkedinUrl"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""TwitterUrl"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""InstagramUrl"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""FacebookUrl"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""Location"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""Title"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""profileInfo"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""manager2"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""manager1"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""LastLoginIp"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""LastName"";
                ALTER TABLE ""AspNetUsers"" DROP COLUMN IF EXISTS ""FirstName"";
            END
            $$;");
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("77df6fbd-4160-4cea-8f24-96564b54e5ac"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 894, DateTimeKind.Utc).AddTicks(5724));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("1bf2fc2e-0e25-46a8-aa96-8f1480331b5b"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 893, DateTimeKind.Utc).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "Plant",
                keyColumn: "Id",
                keyValue: new Guid("0779dd43-6047-400d-968d-e6f1b0c3b286"),
                column: "CreatedDate",
                value: new DateTime(2025, 8, 24, 9, 43, 21, 894, DateTimeKind.Utc).AddTicks(5964));
        }
    }
}

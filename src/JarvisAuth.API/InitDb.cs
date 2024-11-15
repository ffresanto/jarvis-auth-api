using Microsoft.Data.Sqlite;

namespace JarvisAuth.API
{
    public static class InitDb
    {
        private const string SCRIPT_SQL = @"
                    CREATE TABLE IF NOT EXISTS [users] (
                      [id] text NOT NULL
                    , [name] text NOT NULL
                    , [password] text NOT NULL
                    , [email] text NOT NULL
                    , [created_at] text NOT NULL
                    , [updated_at] text NOT NULL
                    , [is_admin] bigint DEFAULT (0) NOT NULL
                    , [enabled] bigint DEFAULT (1) NOT NULL
                    , CONSTRAINT [sqlite_autoindex_users_1] PRIMARY KEY ([id])
                    );

                    CREATE TABLE IF NOT EXISTS [applications] (
                      [id] text NOT NULL
                    , [name] text NOT NULL
                    , [enabled] bigint DEFAULT (1) NOT NULL
                    , [created_at] text NOT NULL
                    , [updated_at] text NOT NULL
                    , CONSTRAINT [sqlite_autoindex_applications_2] PRIMARY KEY ([id])
                    );

                    CREATE TABLE IF NOT EXISTS [users_associate_applications] (
                      [user_id] text NOT NULL
                    , [application_id] text NOT NULL
                    , CONSTRAINT [FK_users_associate_applications_0_0] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    , CONSTRAINT [FK_users_associate_applications_1_0] FOREIGN KEY ([application_id]) REFERENCES [applications] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

                    CREATE TABLE IF NOT EXISTS [applications_permissions] (
                      [id] text NOT NULL
                    , [application_id] text NOT NULL
                    , [name] text NOT NULL
                    , CONSTRAINT [sqlite_autoindex_applications_permissions_1] PRIMARY KEY ([id])
                    , CONSTRAINT [FK_applications_permissions_0_0] FOREIGN KEY ([application_id]) REFERENCES [applications] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

                    CREATE TABLE IF NOT EXISTS [users_permissions] (
                      [user_id] text NOT NULL
                    , [application_permission_id] text NOT NULL
                    , CONSTRAINT [sqlite_autoindex_users_permissions_1] PRIMARY KEY ([user_id],[application_permission_id])
                    , CONSTRAINT [FK_users_permissions_0_0] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    , CONSTRAINT [FK_users_permissions_1_0] FOREIGN KEY ([application_permission_id]) REFERENCES [applications_permissions] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

                    CREATE UNIQUE INDEX IF NOT EXISTS [users_sqlite_autoindex_users_2] ON [users] ([email] ASC);
                    CREATE UNIQUE INDEX IF NOT EXISTS [applications_sqlite_autoindex_applications_1] ON [applications] ([name] ASC);
                    ";
        public static void InitializeDatabase(string connectionStringSqlite)
        {
            using (var connection = new SqliteConnection(connectionStringSqlite))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new SqliteCommand(SCRIPT_SQL, connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }
    }
}

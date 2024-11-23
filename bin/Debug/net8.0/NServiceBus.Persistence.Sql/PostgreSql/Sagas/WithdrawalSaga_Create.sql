
/* TableNameVariable */

/* Initialize */

/* CreateTable */

create or replace function pg_temp.create_saga_table_WithdrawalSaga(tablePrefix varchar, schema varchar)
    returns integer as
    $body$
    declare
        tableNameNonQuoted varchar;
        script text;
        count int;
        columnType varchar;
        columnToDelete text;
    begin
        tableNameNonQuoted := tablePrefix || 'WithdrawalSaga';
        script = 'create table if not exists "' || schema || '"."' || tableNameNonQuoted || '"
(
    "Id" uuid not null,
    "Metadata" text not null,
    "Data" jsonb not null,
    "PersistenceVersion" character varying(23),
    "SagaTypeVersion" character varying(23),
    "Concurrency" int not null,
    primary key("Id")
);';
        execute script;

/* AddProperty TransactionId */

        script = 'alter table "' || schema || '"."' || tableNameNonQuoted || '" add column if not exists "Correlation_TransactionId" integer';
        execute script;

/* VerifyColumnType Int */

        columnType := (
            select data_type
            from information_schema.columns
            where
            table_schema = schema and
            table_name = tableNameNonQuoted and
            column_name = 'Correlation_TransactionId'
        );
        if columnType <> 'integer' then
            raise exception 'Incorrect data type for Correlation_TransactionId. Expected "integer" got "%"', columnType;
        end if;

/* WriteCreateIndex TransactionId */

        script = 'create unique index if not exists "' || tablePrefix || '_i_6523F2384084C7B999BD456C0D8E31E5AE7E7FBD" on "' || schema || '"."' || tableNameNonQuoted || '" using btree ("Correlation_TransactionId" asc);';
        execute script;
/* PurgeObsoleteIndex */

/* PurgeObsoleteProperties */

for columnToDelete in
(
    select column_name
    from information_schema.columns
    where
        table_name = tableNameNonQuoted and
        column_name LIKE 'Correlation_%' and
        column_name <> 'Correlation_TransactionId'
)
loop
	script = '
alter table "' || schema || '"."' || tableNameNonQuoted || '"
drop column "' || columnToDelete || '"';
    execute script;
end loop;

/* CompleteSagaScript */

        return 0;
    end;
    $body$
language 'plpgsql';

select pg_temp.create_saga_table_WithdrawalSaga(@tablePrefix, @schema);


/* TableNameVariable */

/* DropTable */
create or replace function pg_temp.drop_saga_table_WithdrawalSaga(tablePrefix varchar, schema varchar)
    returns integer as
    $body$
    declare
        tableNameNonQuoted varchar;
        dropTable text;
    begin
        tableNameNonQuoted := tablePrefix || 'WithdrawalSaga';
        dropTable = 'drop table if exists "' || schema || '"."' || tableNameNonQuoted || '";';
        execute dropTable;
        return 0;
    end;
    $body$
    language 'plpgsql';

select pg_temp.drop_saga_table_WithdrawalSaga(@tablePrefix, @schema);


/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'TransferSaga`');
set @tableNameNonQuoted = concat(@tablePrefix, 'TransferSaga');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;


/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'DepositSaga`');
set @tableNameNonQuoted = concat(@tablePrefix, 'DepositSaga');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;

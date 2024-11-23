
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'WithdrawalSaga`');
set @tableNameNonQuoted = concat(@tablePrefix, 'WithdrawalSaga');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;

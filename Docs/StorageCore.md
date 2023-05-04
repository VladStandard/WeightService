# SQL Storage Core

## Code structure for table
```
WsSqlPlu1CFkController  # Контроллер таблицы SCHEMA.TABLE.
WsSqlPlu1CFkMap         # Маппинг полей таблицы SCHEMA.TABLE.
WsSqlPlu1CFkModel       # Доменная модель таблицы SCHEMA.TABLE.
WsSqlPlu1CFkValidator   # Валидатор таблицы REF.PLUS_1C_FK.
```
Code example: `namespace WsStorageCore.TableRefFkModels.Plus1CFk;`

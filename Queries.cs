namespace SqlServerTeardown
{
    public static class Queries
    {
        public static string SchemaInfoQuery => @"
        SELECT 
        s.SCHEMA_OWNER, s.SCHEMA_NAME 

        FROM INFORMATION_SCHEMA.SCHEMATA s
        ;";

        public static string TablesInfoQuery => @"
        SELECT 
        t.TABLE_SCHEMA, t.TABLE_NAME, t.TABLE_TYPE 

        FROM INFORMATION_SCHEMA.TABLES t
        ;";

        public static string TablesColumnsQuery => @"
SELECT 
s.SCHEMA_NAME, t.TABLE_NAME, 
c.COLUMN_NAME, c.DATA_TYPE, 
c.IS_NULLABLE, c.COLUMN_DEFAULT, 
sc.is_identity, c.CHARACTER_MAXIMUM_LENGTH, 
sc.precision, sc.scale,
sc.is_xml_document, sc.is_computed, 
sc.is_non_sql_subscribed, sc.is_rowguidcol,

cc.CHECK_CLAUSE,
scc.definition as Computed_Definition,

sc.is_ansi_padded, sc.is_filestream, sc.is_sparse

FROM INFORMATION_SCHEMA.SCHEMATA s
left join sys.schemas ss on s.SCHEMA_NAME = ss.name
left join INFORMATION_SCHEMA.TABLES t on s.SCHEMA_NAME = t.TABLE_SCHEMA
left join sys.tables st on st.name = t.TABLE_NAME and st.schema_id = schema_id(t.TABLE_SCHEMA)
left join INFORMATION_SCHEMA.COLUMNS c on c.TABLE_NAME = st.name and c.TABLE_SCHEMA = s.SCHEMA_NAME
left join sys.all_columns sc on sc.name = c.COLUMN_NAME and sc.object_id = st.object_id
left join 
	( 
		select * from sys.key_constraints sk
		join INFORMATION_SCHEMA.KEY_COLUMN_USAGE  k
		on schema_id(k.TABLE_SCHEMA) = sk.schema_id 
		and OBJECT_ID(k.TABLE_SCHEMA+'.'+ k.TABLE_NAME) = sk.parent_object_id 
		and k.CONSTRAINT_NAME = sk.name
	) k 
	on k.COLUMN_NAME = c.COLUMN_NAME 
	and k.TABLE_NAME = c.TABLE_NAME 
	and k.TABLE_SCHEMA = c.TABLE_SCHEMA
left join 
	(
		select * from INFORMATION_SCHEMA.CHECK_CONSTRAINTS c 
		join sys.check_constraints cc 
		on c.CONSTRAINT_NAME = cc.name 
	) cc 
	on cc.parent_object_id = sc.object_id and sc.name = COL_NAME(cc.parent_object_id, cc.parent_column_id)
left join sys.computed_columns scc on sc.column_id = scc.column_id and sc.object_id = scc.object_id
;";

        public static string ViewsColumnsQuery => @"
SELECT 
s.SCHEMA_NAME, t.TABLE_NAME, 
c.COLUMN_NAME, c.DATA_TYPE, 
c.IS_NULLABLE, c.COLUMN_DEFAULT, 
sc.is_identity, c.CHARACTER_MAXIMUM_LENGTH, 
sc.precision, sc.scale,
sc.is_xml_document, sc.is_computed, 
sc.is_non_sql_subscribed, sc.is_rowguidcol,

cc.CHECK_CLAUSE,
scc.definition as Computed_Definition,

sc.is_ansi_padded, sc.is_filestream, sc.is_sparse

FROM INFORMATION_SCHEMA.SCHEMATA s
left join sys.schemas ss on s.SCHEMA_NAME = ss.name
left join INFORMATION_SCHEMA.VIEWS t on s.SCHEMA_NAME = t.TABLE_SCHEMA
left join sys.views st on st.name = t.TABLE_NAME and st.schema_id = schema_id(t.TABLE_SCHEMA)
left join INFORMATION_SCHEMA.COLUMNS c on c.TABLE_NAME = st.name and c.TABLE_SCHEMA = s.SCHEMA_NAME
left join sys.all_columns sc on sc.name = c.COLUMN_NAME and sc.object_id = st.object_id
left join 
	( 
		select * from sys.key_constraints sk
		join INFORMATION_SCHEMA.KEY_COLUMN_USAGE  k
		on schema_id(k.TABLE_SCHEMA) = sk.schema_id 
		and OBJECT_ID(k.TABLE_SCHEMA+'.'+ k.TABLE_NAME) = sk.parent_object_id 
		and k.CONSTRAINT_NAME = sk.name
	) k 
	on k.COLUMN_NAME = c.COLUMN_NAME 
	and k.TABLE_NAME = c.TABLE_NAME 
	and k.TABLE_SCHEMA = c.TABLE_SCHEMA
left join 
	(
		select * from INFORMATION_SCHEMA.CHECK_CONSTRAINTS c 
		join sys.check_constraints cc 
		on c.CONSTRAINT_NAME = cc.name 
	) cc 
	on cc.parent_object_id = sc.object_id and sc.name = COL_NAME(cc.parent_object_id, cc.parent_column_id)
left join sys.computed_columns scc on sc.column_id = scc.column_id and sc.object_id = scc.object_id
;";

        public static string RelationsQuery => @"
        SELECT
        fk.name as fk_constraint_name,

        schema_name(fk_tab.schema_id) as fk_schema, fk_tab.name as fk_table,
            '>-' as rel,
        schema_name(pk_tab.schema_id) as pk_schema, pk_tab.name as pk_table,
        'columns:' as next, 
        fk_cols.constraint_column_id as no, fk_col.name as fk_column_name,
        ' = ' as [join],
        pk_col.name as pk_column_name

        FROM sys.foreign_keys fk
        INNER JOIN sys.tables fk_tab
            ON fk_tab.object_id = fk.parent_object_id
        INNER JOIN sys.tables pk_tab
            ON pk_tab.object_id = fk.referenced_object_id
        INNER JOIN sys.foreign_key_columns fk_cols
            ON fk_cols.constraint_object_id = fk.object_id
        INNER JOIN sys.columns fk_col
            ON fk_col.column_id = fk_cols.parent_column_id
            AND fk_col.object_id = fk_tab.object_id
        INNER JOIN sys.columns pk_col
            ON pk_col.column_id = fk_cols.referenced_column_id
            AND pk_col.object_id = pk_tab.object_id

        ORDER BY 
        schema_name(fk_tab.schema_id) + '.' + fk_tab.name,
        schema_name(pk_tab.schema_id) + '.' + pk_tab.name
        ;";
    }
}

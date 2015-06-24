SELECT 
	cacheobjtype, objtype, COUNT(*) AS 'count', SUM(size_in_bytes) AS 'sizeInBytes'
FROM sys.dm_exec_cached_plans
GROUP BY cacheobjtype, objtype
ORDER BY cacheobjtype, objtype
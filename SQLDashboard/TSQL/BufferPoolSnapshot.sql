SELECT 
	COUNT(*), page_type 
FROM sys.dm_os_buffer_descriptors
GROUP BY page_type
ORDER BY page_type;
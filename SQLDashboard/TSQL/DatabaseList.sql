SELECT 
	D.name AS 'DatabaseName',
	database_id,
	owner_sid,
	SP.name AS 'OwnerName',
	D.create_date,
	compatibility_level,
	collation_name,
	user_access_desc,
	is_read_only,
	is_auto_close_on,
	is_auto_shrink_on,
	state_desc,
	is_in_standby,
	is_cleanly_shutdown,
	snapshot_isolation_state_desc,
	is_read_committed_snapshot_on,
	recovery_model_desc,
	page_verify_option_desc,
	is_auto_create_stats_on,
	is_auto_update_stats_on,
	is_auto_update_stats_async_on,
	is_fulltext_enabled,
	is_trustworthy_on,
	is_db_chaining_on,
	is_parameterization_forced,
	is_published,
	is_subscribed,
	is_merge_published,
	is_distributor,
	is_broker_enabled,
	log_reuse_wait_desc
	--,is_cdc_enabled
	--,containment_desc	
	--,* 
FROM sys.databases D 
LEFT OUTER JOIN sys.server_principals SP 
ON 
	D.owner_sid = SP.sid
ORDER BY
	D.database_id;
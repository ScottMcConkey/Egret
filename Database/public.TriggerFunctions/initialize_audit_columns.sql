-- FUNCTION: public.initialize_audit_columns()

-- DROP FUNCTION public.initialize_audit_columns();

CREATE FUNCTION public.initialize_audit_columns()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF 
    ROWS 0
AS $BODY$

begin
  new.useraddedby = session_user;
  new.dateadded = current_timestamp;
  new.userlastupdatedby = session_user;
  new.dateupdated = current_timestamp;
  return new;
end;

$BODY$;

ALTER FUNCTION public.initialize_audit_columns()
    OWNER TO postgres;
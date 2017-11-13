-- FUNCTION: public.update_audit_columns()

-- DROP FUNCTION public.update_audit_columns();

CREATE FUNCTION public.update_audit_columns()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE NOT LEAKPROOF 
    ROWS 0
AS $BODY$

BEGIN
   NEW.dateupdated = now();
   NEW.userupdatedby = session_user;
   RETURN NEW;
END;

$BODY$;

ALTER FUNCTION public.update_audit_columns()
    OWNER TO postgres;


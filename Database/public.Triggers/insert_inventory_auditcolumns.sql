-- Trigger: insert_inventory_auditcolumns

-- DROP TRIGGER insert_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER insert_inventory_auditcolumns
    BEFORE INSERT
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE public.initialize_audit_columns();